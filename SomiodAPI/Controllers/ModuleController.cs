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
        
        //string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        //// GET: api/somiod/module
        //[Route("module")]
        //public IEnumerable<Module> GetAllModules()
        //{
        //    List<Module> modules = new List<Module>();
        //    SqlConnection conn = null;
        //    string sql = "SELECT * FROM Module ORDER BY Id";

        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        conn.Open();
        //        SqlCommand cmd = new SqlCommand(sql, conn);


        //        SqlDataReader reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            Module module = new Module
        //            {
        //                Id = (int)reader["Id"],
        //                Name = (string)reader["Name"],
        //                Creation_dt = (string)reader["Creation_dt"],
        //                Parent = (int)reader["Parent"]
        //            };

        //            modules.Add(module);
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
        //    return modules;
        //}

        //// GET: api/somiod/module/5
        //[Route("module/{id}")]
        //public IHttpActionResult GetModule(int id)
        //{
        //    Module module = null;
        //    SqlConnection conn = null;

        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        conn.Open();

        //        string sql = "SELECT * FROM Module WHERE Id=@idApp";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@idApp", id);

        //        SqlDataReader reader = cmd.ExecuteReader();
        //        if (reader.Read())
        //        {
        //            module = new Module
        //            {
        //                Id = (int)reader["Id"],
        //                Name = (string)reader["Name"],
        //                Creation_dt = (string)reader["Creation_dt"],
        //                Parent = (int)reader["Parent"]
        //            };
        //        }
        //        reader.Close();
        //        conn.Close();
        //        if (module == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(module);

        //    }
        //    catch (Exception)
        //    {
        //        //fechar a ligação à BD
        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        return NotFound();
        //    }
        //}


        //// PUT: api/somiod/module/5
        //[Route("module/{id}")]
        //public IHttpActionResult PutModule(int id, [FromBody]Module value)
        //{
        //    SqlConnection conn = null;

        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        conn.Open();

        //        string sql = "UPDATE Module SET Name=@Name, Creation_dt=@Creation WHERE Id=@Id";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@Name", value.Name);
        //        cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
        //        cmd.Parameters.AddWithValue("@Id", id);

        //        int numRows = cmd.ExecuteNonQuery();
        //        conn.Close();

        //        if (numRows > 0)
        //        {
        //            return Ok();
        //        }
        //        return InternalServerError();

        //    }
        //    catch (Exception)
        //    {
        //        //fechar a ligação à BD
        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        return InternalServerError();
        //    }
        //}

        //// DELETE: api/somiod/module/5
        //[Route("module/{id}")]
        //public IHttpActionResult DeleteModule(int id)
        //{
        //    SqlConnection conn = null;

        //    try
        //    {
        //        conn = new SqlConnection(connectionString);
        //        conn.Open();

        //        string sql = "DELETE FROM Module WHERE Id=@Id";
        //        SqlCommand cmd = new SqlCommand(sql, conn);
        //        cmd.Parameters.AddWithValue("@Id", id);

        //        int numRows = cmd.ExecuteNonQuery();
        //        conn.Close();

        //        if (numRows > 0)
        //        {
        //            return Ok();
        //        }
        //        return InternalServerError();

        //    }
        //    catch (Exception)
        //    {
        //        //fechar a ligação à BD
        //        if (conn.State == System.Data.ConnectionState.Open)
        //        {
        //            conn.Close();
        //        }
        //        return InternalServerError();
        //    }
        //}
    }
}
