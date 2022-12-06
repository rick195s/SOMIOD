using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//TODO - VERIFICAR SE O "GetApplicationById" É PRECISO

namespace SomiodAPI.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {

        string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        // GET: api/somiod/application
        [Route("application")]
        public IEnumerable<Application> GetAllApplications()
        {
            List<Application> applications = new List<Application>();
            SqlConnection conn = null;
            string sql = "SELECT * FROM Application ORDER BY Id";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Application application = new Application
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = (string)reader["Creation_dt"]
                    };

                    applications.Add(application);
                }
                reader.Close();
                conn.Close();
            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            return applications;
        }

      
    }
}
