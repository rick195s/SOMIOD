using SomiodAPI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AuthService
{

    /// <summary>
    /// Summary description for SqlServerHelper
    /// </summary>
    public class SqlServerHelper
    {

        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        public static int ApplicationExists(string application)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
             
                SqlCommand cmd = new SqlCommand();

                // attention...
                cmd.CommandText = "SELECT id FROM Applications where name = @name";
                cmd.Parameters.AddWithValue("name", application);
    

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int id = (int)cmd.ExecuteScalar();
                return id;
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


        public static Application GetApplication(int id)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                // isto DEVE que ser alterado .... para usar SQLParameters
                cmd.CommandText = "SELECT * FROM Application where id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Application application = LoadApplication(reader);
                    return application;
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

        public static List<Application> GetApplications()
        {
            List<Application> applications = new List<Application>();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Applications";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Application application = LoadApplication(reader);
                    applications.Add(application);
                }
                return applications;
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
            

        private static Application LoadApplication(SqlDataReader reader)
        {
            Application application = new Application();

            application.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            application.Name = reader.GetString(reader.GetOrdinal("Name"));
            application.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            
            return application;
        }

    }
}