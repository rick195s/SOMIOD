using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SomiodAPI.Models;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;
using System.Net.Configuration;

namespace SomiodAPI.SqlHelpers
{
    public class SqlDataHelper
    {
        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static Data CreateData(Data data, string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            int parentId = SqlSubscriptionHelper.GetParent(applicationName, moduleName);

            if (parentId == 0)
            {
                return null;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO Data VALUES(@Content, @Creation, @Parent)";
                cmd.Parameters.AddWithValue("@Content", data.Content);
                cmd.Parameters.AddWithValue("@Creation", data.Creation_dt);
                cmd.Parameters.AddWithValue("@Parent", parentId);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();
                Debug.WriteLine("num rows: " + numRows);

                if (numRows > 0)
                {
                    return GetData(data.Content);
                }
                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static Data GetData(string content)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Data where content = @Content";
                cmd.Parameters.AddWithValue("@Content", content);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Data data = LoadData(reader);
                    return data;
                }

                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static List<Data> GetDatas(int parent)
        {
            List<Data> datas = new List<Data>();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Data WHERE parent = @Parent";
                cmd.Parameters.AddWithValue("@Parent", parent);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Data data = LoadData(reader);
                    datas.Add(data);
                }
                return datas;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static Data GetData(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Data where id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Data data = LoadData(reader);
                    return data;
                }

                return null;
            }
            catch
            {
                return null;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static Data DeleteData(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                Data data = GetData(id);

                string sql = "DELETE FROM Data WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return data;
                }
                return null;

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;
            }
        }


        private static Data LoadData(SqlDataReader reader)
        {
            Data data = new Data();

            data.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            data.Content = reader.GetString(reader.GetOrdinal("Content"));
            data.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            data.Parent = reader.GetSqlInt32(reader.GetOrdinal("Parent")).Value;

            return data;
        }

    }
}