using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using System.Web.UI.WebControls.WebParts;
using System.Diagnostics;

namespace SomiodAPI.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ModuleController : ApiController
    {
        
        string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        // GET: api/somiod/module
        [Route("module")]
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
        [Route("module/{id}")]
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

        // POST: api/somiod/Lamp
        [Route("{applicationName}")]
        public IHttpActionResult PostModule([FromBody]Module value, string applicationName)
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

        // PUT: api/somiod/module/5
        [Route("module/{id}")]
        public IHttpActionResult PutModule(int id, [FromBody]Module value)
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

        // DELETE: api/somiod/module/5
        [Route("module/{id}")]
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
    }
}
