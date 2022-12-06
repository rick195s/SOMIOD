using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SomiodAPI.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {
        string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        #region GET
        // GET: api/somiod/application
        [Route("applications")]
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

        // GET: api/somiod/application/5
        [Route("applications/{id:int}")]
        public IHttpActionResult GetApplicationById(int id)
        {
            Application app = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "SELECT * FROM Application WHERE Id=@idApp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idApp", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    app = new Application
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = (string)reader["Creation_dt"]
                    };
                }
                reader.Close();
                conn.Close();
                if (app == null)
                {
                    return NotFound();
                }
                return Ok(app);

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }

        // GET: api/somiod/module
        [Route("modules")]
        public IEnumerable<Module> GetAllModules()
        {
            List<Module> modules = new List<Module>();
            SqlConnection conn = null;
            string sql = "SELECT * FROM Module ORDER BY Id";

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);


                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Module module = new Module
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = (string)reader["Creation_dt"],
                        Parent = (int)reader["Parent"]
                    };

                    modules.Add(module);
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
            return modules;
        }

        // GET: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult GetModule(int id)
        {
            Module module = null;
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "SELECT * FROM Module WHERE Id=@idApp";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@idApp", id);

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    module = new Module
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Creation_dt = (string)reader["Creation_dt"],
                        Parent = (int)reader["Parent"]
                    };
                }
                reader.Close();
                conn.Close();
                if (module == null)
                {
                    return NotFound();
                }
                return Ok(module);

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return NotFound();
            }
        }
        #endregion

        #region POST
        // POST: api/somiod
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] Application value)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "INSERT INTO Application VALUES(@Name, @Creation)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // POST: api/somiod/Lamp
        [Route("{applicationName}")]
        public IHttpActionResult PostModule([FromBody] Module value, string applicationName)
        {
            SqlConnection conn = null;
            value.Parent = 0;


            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql2 = "SELECT * FROM Application WHERE Name=@ApplicationName";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@ApplicationName", applicationName);
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    value.Parent = (int)reader["Id"];
                }

                Debug.WriteLine("id aplicação: " + value.Parent);
                reader.Close();

                if (value.Parent == 0)
                {
                    return InternalServerError();
                }

                string sql = "INSERT INTO Module VALUES(@Name, @Creation, @Parent)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                cmd.Parameters.AddWithValue("@Parent", value.Parent);

                int numRows = cmd.ExecuteNonQuery();

                conn.Close();
                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // POST: api/somiod/Lamp/Light
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PostSubscription_Data([FromBody] Subscription value, string applicationName, string moduleName)
        {
            SqlConnection conn = null;
            int applicationId = 0;
            value.Parent = 0;
            
            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql2 = "SELECT * FROM Application WHERE Name=@ApplicationName";
                SqlCommand cmd2 = new SqlCommand(sql2, conn);
                cmd2.Parameters.AddWithValue("@ApplicationName", applicationName);
                SqlDataReader reader = cmd2.ExecuteReader();
                while (reader.Read())
                {
                    applicationId = (int)reader["Id"];
                }

                Debug.WriteLine("id aplicação: " + applicationId);
                reader.Close();

                if (applicationId == 0)
                {
                    return InternalServerError();
                }

                string sql3 = "SELECT * FROM Module WHERE Name=@ModuleName";
                SqlCommand cmd3 = new SqlCommand(sql3, conn);
                cmd3.Parameters.AddWithValue("@ModuleName", moduleName);
                SqlDataReader reader2 = cmd3.ExecuteReader();
                while (reader2.Read())
                {
                    value.Parent = (int)reader2["Id"];
                }

                Debug.WriteLine("id modulo: " + value.Parent);
                reader2.Close();

                if (value.Parent == 0)
                {
                    return InternalServerError();
                }

                int numRows = 0;
                if (value.Res_type.ToLower() == "subscription") { 
                    string sql = "INSERT INTO Subscription VALUES(@Name, @Creation, @Parent, @Event, @Endpoint)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                    cmd.Parameters.AddWithValue("@Parent", value.Parent);
                    cmd.Parameters.AddWithValue("@Event", value.Event);
                    cmd.Parameters.AddWithValue("@Endpoint", value.Endpoint);

                    numRows = cmd.ExecuteNonQuery();
                } else if (value.Res_type.ToLower() == "data")
                {
                    string sql = "INSERT INTO Data VALUES(@Name, @Creation, @Parent)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                    cmd.Parameters.AddWithValue("@Parent", value.Parent);

                    numRows = cmd.ExecuteNonQuery();
                }


                conn.Close();
                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }
        #endregion

        #region PUT
        // PUT: api/somiod/application/5
        [Route("applications/{id}")]
        public IHttpActionResult PutApplication(int id, [FromBody] Application value)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "UPDATE Application SET Name=@Name, Creation_dt=@Creation WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // PUT: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult PutModule(int id, [FromBody] Module value)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "UPDATE Module SET Name=@Name, Creation_dt=@Creation WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", value.Name);
                cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }
        #endregion

        #region DELETE
        // DELETE: api/somiod/application/5
        [Route("applications/{id}")]
        public IHttpActionResult DeleteApplication(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "DELETE FROM Application WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // DELETE: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult DeleteModule(int id)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(connectionString);
                conn.Open();

                string sql = "DELETE FROM Module WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                int numRows = cmd.ExecuteNonQuery();
                conn.Close();

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // DELETE: api/somiod/datas/5
        [Route("datas/{id}")]
        public IHttpActionResult DeleteData(int id)
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

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }

        // DELETE: api/somiod/subscriptions/5
        [Route("subscriptions/{id}")]
        public IHttpActionResult DeleteSubscriptions(int id)
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

                if (numRows > 0)
                {
                    return Ok();
                }
                return InternalServerError();

            }
            catch (Exception)
            {
                //fechar a ligação à BD
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
                return InternalServerError();
            }
        }
        #endregion
    }
}
