using SomiodAPI.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SomiodAPI
{

    /// <summary>
    /// Summary description for SqlServerHelper
    /// </summary>
    public class SqlModuleHelper
    {

        static string connectionString = SomiodAPI.Properties.Settings.Default.connStr;


        public static Module CreateModule(Module module, string applicationName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            int parentId = GetModuleParent(applicationName);

            if (parentId == 0)
            {
                return null;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Module VALUES(@Name, @Creation, @Parent)";
                cmd.Parameters.AddWithValue("@Name", module.Name);
                cmd.Parameters.AddWithValue("@Creation", module.Creation_dt);
                cmd.Parameters.AddWithValue("@Parent", parentId);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                int numRows = cmd.ExecuteNonQuery();
                if (numRows > 0)
                {
                    return GetModule(module.Name);
                }

                return null;
            }
            catch(Exception e )
            {
                throw e;
            }
            finally
            {
                if (sqlConnection != null)
                    sqlConnection.Close();
            }
        }

        public static int GetModuleParent(string applicationName)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            int applicationId = 0;

            try
            {
                sqlConnection.Open();

                string sql = "SELECT * FROM Application WHERE Name=@ApplicationName";
                SqlCommand cmd = new SqlCommand(sql, sqlConnection);
                cmd.Parameters.AddWithValue("@ApplicationName", applicationName);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    applicationId = (int)reader["Id"];
                }

                Debug.WriteLine("id aplicação: " + applicationId);
                reader.Close();

                if (applicationId == 0)
                {
                    return 0;
                }

                return applicationId;

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

        public static Module GetModule(int id, bool loadDatas = false)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Module where id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Module module = LoadModule(reader, loadDatas);
                    return module;
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

        public static Module GetModule(string name, bool loadDatas = false)
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Module where name = @Name";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Module module = LoadModule(reader, loadDatas);
                    return module;
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

        public static List<Module> GetModules()
        {
            List<Module> modules = new List<Module>();
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                cmd.CommandText = "SELECT * FROM Module";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Module module = LoadModule(reader);
                    modules.Add(module);
                }
                return modules;
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

        public static Module UpdateModule(Module module, int id)
        {
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {

                string sql = "UPDATE Module SET Name=@Name WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", module.Name);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                int numRows = cmd.ExecuteNonQuery();

                if (numRows > 0)
                {
                    return GetModule(module.Name);
                }

                return null;

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (conn != null)
                    conn.Close();
            }
        }

        public static Module DeleteModule(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                Module module = GetModule(id);

                string sql = "DELETE FROM Module WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return module;
                }

                return null;

            }
            catch (Exception e)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                throw e;
            }
        }


        private static Module LoadModule(SqlDataReader reader, bool loadDatas = false)
        {
            Module module = new Module();

            module.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            module.Name = reader.GetString(reader.GetOrdinal("Name"));
            module.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            module.Parent = reader.GetSqlInt32(reader.GetOrdinal("Parent")).Value;

            if (loadDatas)
            {
                module.data = SqlDataHelper.GetDatas(module.Id);
            }

            return module;
        }

    }
}