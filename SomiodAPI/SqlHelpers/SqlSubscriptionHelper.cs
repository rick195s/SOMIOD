﻿using System;
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

<<<<<<< HEAD
            int parentId = GetParent(applicationName, moduleName);
=======
            int parentId = SqlDataHelper.GetDataParent(applicationName, moduleName);
>>>>>>> 1637257cbe06a94af7f5c2c4d3d48e49802ff07e

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

<<<<<<< HEAD
        public static int DeleteSubscription(int id)
=======

        public static Subscription DeleteSubscription(int id)
>>>>>>> 1637257cbe06a94af7f5c2c4d3d48e49802ff07e
        {
            SqlConnection sqlConnection = null;

            try
            {
                sqlConnection = new SqlConnection(connectionString);

<<<<<<< HEAD
                string sql = "DELETE FROM Subscription WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
=======
                SqlCommand cmd = new SqlCommand();

                Subscription subscription = GetSubscription(id);

                cmd.CommandText = "DELETE FROM Application WHERE Id=@Id";
>>>>>>> 1637257cbe06a94af7f5c2c4d3d48e49802ff07e
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


<<<<<<< HEAD
        //TODO - apagar (?)
        private static Data LoadData(SqlDataReader reader)
=======
        public static Subscription GetSubscription(int id)
>>>>>>> 1637257cbe06a94af7f5c2c4d3d48e49802ff07e
        {
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(connectionString);
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

<<<<<<< HEAD
            data.Id = reader.GetSqlInt32(reader.GetOrdinal("Id")).Value;
            data.Content = reader.GetString(reader.GetOrdinal("Content"));
            data.Creation_dt = reader.GetString(reader.GetOrdinal("Creation_dt"));
            data.Parent = reader.GetSqlInt32(reader.GetOrdinal("Parent")).Value;
=======
                // isto DEVE que ser alterado .... para usar SQLParameters
                cmd.CommandText = "SELECT * FROM Subscription where id = @id";
                cmd.Parameters.AddWithValue("id", id);
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();
>>>>>>> 1637257cbe06a94af7f5c2c4d3d48e49802ff07e

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