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


        public static Subscription CreateSubscription(Subscription subscription, string applicationName, string moduleName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            int parentId = GetParent(applicationName, moduleName);

            if (parentId == 0 || (!subscription.Event.ToLower().Contains("creation") && !subscription.Event.ToLower().Contains("deletion")))
            {
                return null;
            }

            if (VerifySubscriptionName(subscription.Name, parentId))
            {
                return null;
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

                if (numRows > 0)
                {
                    return GetSubscription(subscription.Name);
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

        public static bool VerifySubscriptionName(string name, int parentId)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Subscription where name = @Name AND parent = @ParentId";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@ParentId", parentId);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (!reader.Read())
                {
                    return false;
                }

                return true;
            }
            catch
            {
                return true;
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
            int applicationId = SqlModuleHelper.GetModuleParent(applicationName);

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

                cmd.CommandText = "DELETE FROM Subscription WHERE Id=@Id";
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


        public static Subscription GetSubscription(string name)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Subscription where name = @Name";
                cmd.Parameters.AddWithValue("@Name", name);
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
            subscription.Event = reader.GetString(reader.GetOrdinal("Event"));
            subscription.Endpoint = reader.GetString(reader.GetOrdinal("EndPoint"));

            return subscription;
        }
        public static List<Subscription> GetSubscriptions(int parent,string event_data)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Subscription where Parent=@parent and Event=@event_data";
                cmd.Parameters.AddWithValue("@parent", parent);
                cmd.Parameters.AddWithValue("@event_data", event_data);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                List<Subscription> subscriptions = new List<Subscription>();
                while (reader.Read())
                {
                    Subscription subscription = LoadSubscription(reader);
                    subscriptions.Add(subscription);
                }

                return subscriptions;
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

        }
}