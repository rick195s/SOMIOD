using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using SomiodAPI.Models;

namespace SomiodAPI.SqlHelpers
{
    public class SqlSubscriptionHelper
    {
        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static int CreateSubscription(Subscription_Data subscription, string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            int parentId = GetParent(applicationName, moduleName);

            if (parentId == 0)
            {
                return 0;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Subscription VALUES(@Name, @Creation, @Parent, @Event, @Endpoint)";
                cmd.Parameters.AddWithValue("@Name", subscription.Name);
                cmd.Parameters.AddWithValue("@Creation", subscription.Creation_dt);
                cmd.Parameters.AddWithValue("@Parent", parentId);
                cmd.Parameters.AddWithValue("@Event", subscription.Event);
                cmd.Parameters.AddWithValue("@Endpoint", subscription.Endpoint);

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

        public static int GetParent(string applicationName, string moduleName)
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

        public static int DeleteSubscription(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "DELETE FROM Subscription WHERE Id=@Id";
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