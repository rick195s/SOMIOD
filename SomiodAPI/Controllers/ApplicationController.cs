using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

//TODO - VERIFICAR SE O "GetApplicationById" É PRECISO

namespace SomiodAPI.Controllers
{
    //[RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {

        //string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        //// GET: api/somiod/application
        ////[Route("applications")]
        ////public IEnumerable<Application> GetAllApplications()
        //{
        //    List<Application> applications = new List<Application>();
        //    SqlConnection conn = null;
        //    string sql = "SELECT * FROM Application ORDER BY Id";

        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);


        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Application application = new Application
        //            {
        //                Id = (int)reader["Id"],
        //                Name = (string)reader["Name"],
        //                Creation_dt = (string)reader["Creation_dt"]
        //            };

        //            applications.Add(application);
        //        }
        //        reader.Close();
        //        conn.Close();
        //    }
        //    catch (Exception)
        //    {
        //        //fechar a ligação à BD
        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //    }
        //    return applications;
        //}

    //    // GET: api/somiod/application/5
    //    [Route("application/{id:int}")]
    //    public IHttpActionResult GetApplicationById(int id)
    //    {
    //        Application app = null;
    //        SqlConnection conn = null;

    //        try
    //        {
    //            conn = new SqlConnection(connectionString);
    //            conn.Open();

    //            string sql = "SELECT * FROM Application WHERE Id=@idApp";
    //            SqlCommand cmd = new SqlCommand(sql, conn);
    //            cmd.Parameters.AddWithValue("@idApp", id);

    //            SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                app = new Application
    //                {
    //                    Id = (int)reader["Id"],
    //                    Name = (string)reader["Name"],
    //                    Creation_dt = (string)reader["Creation_dt"]
    //                };
    //            }
    //            reader.Close();
    //            conn.Close();
    //            if (app == null)
    //            {
    //                return NotFound();
    //            }
    //            return Ok(app);

    //        }
    //        catch (Exception)
    //        {
    //            //fechar a ligação à BD
    //            if (conn.State == System.Data.ConnectionState.Open)
    //            {
    //                conn.Close();
    //            }
    //            return NotFound();
    //        }
    //    }

    //    // POST: api/somiod
    //    [Route("")]
    //    public IHttpActionResult PostApplication([FromBody]Application value)
    //    {
    //        SqlConnection conn = null;

    //        try
    //        {
    //            conn = new SqlConnection(connectionString);
    //            conn.Open();

    //            string sql = "INSERT INTO Application VALUES(@Name, @Creation)";
    //            SqlCommand cmd = new SqlCommand(sql, conn);
    //            cmd.Parameters.AddWithValue("@Name", value.Name);
    //            cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);

    //            int numRows = cmd.ExecuteNonQuery();
    //            conn.Close();

    //            if (numRows > 0)
    //            {
    //                return Ok();
    //            }
    //            return InternalServerError();

    //        }
    //        catch (Exception)
    //        {
    //            //fechar a ligação à BD
    //            if (conn.State == System.Data.ConnectionState.Open)
    //            {
    //                conn.Close();
    //            }
    //            return InternalServerError();
    //        }
    //    }

    //    [Route("{applicationName}")]
    //    public IHttpActionResult PostModule([FromBody] Module value, string applicationName)
    //    {
    //        SqlConnection conn = null;
    //        value.Parent = 0;


    //        try
    //        {
    //            conn = new SqlConnection(connectionString);
    //            conn.Open();

    //            string sql2 = "SELECT * FROM Application WHERE Name=@ApplicationName";
    //            SqlCommand cmd2 = new SqlCommand(sql2, conn);
    //            cmd2.Parameters.AddWithValue("@ApplicationName", applicationName);
    //            SqlDataReader reader = cmd2.ExecuteReader();
    //            while (reader.Read())
    //            {
    //                value.Parent = (int)reader["Id"];
    //            }

    //            Debug.WriteLine("id aplicação: " + value.Parent);
    //            reader.Close();

    //            if (value.Parent == 0)
    //            {
    //                return InternalServerError();
    //            }

    //            string sql = "INSERT INTO Module VALUES(@Name, @Creation, @Parent)";
    //            SqlCommand cmd = new SqlCommand(sql, conn);
    //            cmd.Parameters.AddWithValue("@Name", value.Name);
    //            cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
    //            cmd.Parameters.AddWithValue("@Parent", value.Parent);

    //            int numRows = cmd.ExecuteNonQuery();

    //            conn.Close();
    //            if (numRows > 0)
    //            {
    //                return Ok();
    //            }
    //            return InternalServerError();

    //        }
    //        catch (Exception)
    //        {
    //            //fechar a ligação à BD
    //            if (conn.State == System.Data.ConnectionState.Open)
    //            {
    //                conn.Close();
    //            }
    //            return InternalServerError();
    //        }
    //    }

    //    // PUT: api/somiod/application/5
    //    [Route("application/{id}")]
    //    public IHttpActionResult PutApplication(int id, [FromBody]Application value)
    //    {
    //        SqlConnection conn = null;

    //        try
    //        {
    //            conn = new SqlConnection(connectionString);
    //            conn.Open();

    //            string sql = "UPDATE Application SET Name=@Name, Creation_dt=@Creation WHERE Id=@Id";
    //            SqlCommand cmd = new SqlCommand(sql, conn);
    //            cmd.Parameters.AddWithValue("@Name", value.Name);
    //            cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
    //            cmd.Parameters.AddWithValue("@Id", id);

    //            int numRows = cmd.ExecuteNonQuery();
    //            conn.Close();

    //            if (numRows > 0)
    //            {
    //                return Ok();
    //            }
    //            return InternalServerError();

    //        }
    //        catch (Exception)
    //        {
    //            //fechar a ligação à BD
    //            if (conn.State == System.Data.ConnectionState.Open)
    //            {
    //                conn.Close();
    //            }
    //            return InternalServerError();
    //        }
    //    }

    //    // DELETE: api/somiod/application/5
    //    [Route("application/{id}")]
    //    public IHttpActionResult DeleteApplication(int id)
    //    {
    //        SqlConnection conn = null;

    //        try
    //        {
    //            conn = new SqlConnection(connectionString);
    //            conn.Open();

    //            string sql = "DELETE FROM Application WHERE Id=@Id";
    //            SqlCommand cmd = new SqlCommand(sql, conn);
    //            cmd.Parameters.AddWithValue("@Id", id);

    //            int numRows = cmd.ExecuteNonQuery();
    //            conn.Close();

    //            if (numRows > 0)
    //            {
    //                return Ok();
    //            }
    //            return InternalServerError();

    //        }
    //        catch (Exception)
    //        {
    //            //fechar a ligação à BD
    //            if (conn.State == System.Data.ConnectionState.Open)
    //            {
    //                conn.Close();
    //            }
    //            return InternalServerError();
    //        }
    //    }
    }
}
