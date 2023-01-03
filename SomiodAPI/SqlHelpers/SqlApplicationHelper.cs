using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SomiodAPI
{

    /// <summary>
    /// Summary description for SqlServerHelper
    /// </summary>
    public class SqlApplicationHelper
    {

        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static List<Application> GetApplications()
        {
            List<Application> applications = new List<Application>();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Application";
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


        public static Application CreateApplication(Application application)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Application VALUES(@Name, @Creation)";
                cmd.Parameters.AddWithValue("@Name", application.Name);
                cmd.Parameters.AddWithValue("@Creation", application.Creation_dt);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();
                if (numRows > 0)
                {
                    return GetApplication(application.Name);
                }
                return null;
            }
            catch(Exception e)
            {
                throw (e);
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }


        public static Application UpdateApplication(int id, Application application)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "UPDATE Application SET Name=@Name WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Name", application.Name);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();
                if (numRows > 0)
                {
                    return GetApplication(application.Name);
                }
                return null;
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }




        public static Application DeleteApplication(int id)
        {

            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                
                SqlCommand cmd = new SqlCommand();
                
                Application application = GetApplication(id);

                cmd.CommandText = "DELETE FROM Application WHERE Id=@Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
                
                int numRows = cmd.ExecuteNonQuery();
               
                if (numRows > 0)
                {
                    return application;
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




        public static Application GetApplication(string name)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                // isto DEVE que ser alterado .... para usar SQLParameters
                cmd.CommandText = "SELECT * FROM Application where name = @Name";
                cmd.Parameters.AddWithValue("@Name", name);
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


        public static int ApplicationExists(string application)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
             
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "SELECT id FROM Application where name = @name";
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