using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SomiodAPI.Models;
using static System.Net.Mime.MediaTypeNames;

namespace SomiodAPI.SqlHelpers
{
    public class SqlDataHelper
    {
        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static int CreateData(Subscription_Data data, string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            int parentId = SqlSubscriptionHelper.GetParent(applicationName, moduleName);

            if (parentId == 0)
            {
                return 0;
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

                return numRows;
            }
            catch
            {
                return 0;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }


        public static int DeleteData(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "DELETE FROM Data WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                return numRows;

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return 0;
            }
        }


        //TODO - apagar (?)
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