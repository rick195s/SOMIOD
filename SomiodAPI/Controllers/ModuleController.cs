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


        // POST: api/somiod/Lamp
        [Route("{applicationName}")]
        public IHttpActionResult PostModule([FromBody]Module value, string applicationName)
        {
            SqlConnection conn = null;
            
            return Ok();


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

     
    }
}
