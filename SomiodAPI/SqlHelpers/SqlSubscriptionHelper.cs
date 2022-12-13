using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SomiodAPI.SqlHelpers
{
    public class SqlSubscriptionHelper
    {
        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static int CreateData(Data data, string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            int parentId = getDataParent(applicationName, moduleName);

            if (parentId == 0)
            {
                return 0;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Data VALUES(@Name, @Creation, @Parent)";
                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Creation", data.Creation_dt);
                cmd.Parameters.AddWithValue("@Parent", parentId);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();

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

        public static int GetDataParent(string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            int moduleId = 0;
            int applicationId = SqlModuleHelper.getModuleParent(applicationName);

            if (applicationId == 0)
            {
                return 0;
            }
            

            try
            {
                sqlConnection.Open();

                string sql = "SELECT * FROM Module WHERE Name=@ModuleName";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.AddWithValue("@ModuleName", moduleName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    moduleId = (int)reader["Id"];
                }

                Debug.WriteLine("id do modulo: " + moduleId);
                reader.Close();

                if (moduleId == 0)
                {
                    return 0;
                }

                return moduleId;

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
                return 0;
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


        private static Data LoadData(SqlDataReader reader)
        {
            Data data = new Data();

            data.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            data.Name = reader.GetString(reader.GetOrdinal("Name"));
            data.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            data.Parent = reader.GetSqlInt32(reader.GetOrdinal("Parent")).Value;

            return data;
        }

    }
}