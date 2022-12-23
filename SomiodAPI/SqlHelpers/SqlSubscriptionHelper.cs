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

            int parentId = SqlDataHelper.GetDataParent(applicationName, moduleName);

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


        public static Subscription DeleteSubscription(int id)
        {
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

                SqlCommand cmd = new SqlCommand();

                Subscription subscription = GetSubscription(id);

                cmd.CommandText = "DELETE FROM Application WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();

                if (numRows > 0)
                {
                    return subscription;
                }
                return null;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }

    
        }


        public static Subscription GetSubscription(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                // isto DEVE que ser alterado .... para usar SQLParameters
                cmd.CommandText = "SELECT * FROM Subscription where id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Subscription subscription = LoadSubscription(reader);
                    return subscription;
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

        private static Subscription LoadSubscription(SqlDataReader reader)
        {
            Subscription subscription = new Subscription();

            subscription.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            subscription.Name = reader.GetString(reader.GetOrdinal("Name"));
            subscription.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            subscription.Parent = reader.GetSqlInt32(reader.GetOrdinal("Parent")).Value;

            return subscription;
        }

    }
}