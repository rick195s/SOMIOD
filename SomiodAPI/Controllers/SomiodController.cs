﻿using SomiodAPI.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web.Http;
using static System.Net.Mime.MediaTypeNames;

namespace SomiodAPI.Controllers
{
    [RoutePrefix("api/somiod")]
    public class SomiodController : ApiController
    {
        string connectionString = SomiodAPI.Properties.Settings.Default.connStr;

        #region GET
        // GET: api/somiod/application
        [Route("applications")]
        public IHttpActionResult GetAllApplications()
        {
            List<Application> applications  = SqlApplicationHelper.GetApplications();

            if (applications == null)
            {
                return NotFound();
            }
            return Ok(applications);
        }

        // GET: api/somiod/application/5
        [Route("applications/{id:int}")]
        public IHttpActionResult GetApplicationById(int id)
        {
            Application application = SqlApplicationHelper.GetApplication(id); ;

            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }

        // GET: api/somiod/module
        [Route("modules")]
        public IHttpActionResult GetAllModules()
        {
            List<Module> modules = SqlModuleHelper.GetModules();

            if (modules == null)
            {
                return NotFound();
            }
            return Ok(modules);
        }

        // GET: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult GetModule(int id)
        {
            Module module = SqlModuleHelper.GetModule(id);

            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }
        #endregion

        #region POST
        // POST: api/somiod
        [Route("")]
        public IHttpActionResult PostApplication([FromBody] Application application)
        {
            try
            {
                Application applicationCreated = SqlApplicationHelper.CreateApplication(application);

                if (applicationCreated == null)
                {
                    return NotFound();
                }

                return Ok(applicationCreated);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE"))
                {
                    return BadRequest("Unique Name Constraint");
                }

                return InternalServerError();
            }
            
        }

        // POST: api/somiod/Lamp
        [Route("{applicationName}")]
        public IHttpActionResult PostModule([FromBody] Module value, string applicationName)
        {

            int success = SqlModuleHelper.CreateModule(value, applicationName);

            if (success == 0)
            {
                return InternalServerError();
            }
            return Ok();
            
        }

        // POST: api/somiod/Lamp/Light
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PostSubscription_Data([FromBody] Subscription value, string applicationName, string moduleName)
        {
            SqlConnection conn = null;
            value.Parent = 0;

            int parentId = SqlDataHelper.getDataParent(applicationName, moduleName);

            try
            {
                conn = new SqlConnection(connectionString);

                int numRows = 0;
                if (value.Res_type.ToLower() == "subscription") { 
                    string sql = "INSERT INTO Subscription VALUES(@Name, @Creation, @Parent, @Event, @Endpoint)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                    cmd.Parameters.AddWithValue("@Parent", parentId);
                    cmd.Parameters.AddWithValue("@Event", value.Event);
                    cmd.Parameters.AddWithValue("@Endpoint", value.Endpoint);

                    numRows = cmd.ExecuteNonQuery();
                } else if (value.Res_type.ToLower() == "data")
                {
                    string sql = "INSERT INTO Data VALUES(@Name, @Creation, @Parent)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@Name", value.Name);
                    cmd.Parameters.AddWithValue("@Creation", value.Creation_dt);
                    cmd.Parameters.AddWithValue("@Parent", parentId);

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
        public IHttpActionResult PutApplication(int id, [FromBody] Application application)
        {
            try
            {
                Application applicationUpdated = SqlApplicationHelper.UpdateApplication(id, application);

                if (applicationUpdated == null)
                {
                    return NotFound();
                }

                return Ok(applicationUpdated);
            }
            catch (Exception)
            {
                return InternalServerError();

            }

        }

        // PUT: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult PutModule(int id, [FromBody] Module value)
        {
            int success = SqlModuleHelper.updateModule(value, id);

            if (success == 0)
            {
                return InternalServerError();
            }
            return Ok();
        }
        #endregion

        #region DELETE
        // DELETE: api/somiod/application/5
        [Route("applications/{id}")]
        public IHttpActionResult DeleteApplication(int id)
        {
            try
            {
                Application application = SqlApplicationHelper.DeleteApplication(id); ;

                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);

            }
            catch (Exception)
            {
                return InternalServerError();
            }

            
        }

        // DELETE: api/somiod/module/5
        [Route("modules/{id}")]
        public IHttpActionResult DeleteModule(int id)
        {
            int success = SqlModuleHelper.deleteModule(id);

            if (success == 0)
            {
                return InternalServerError();
            }
            return Ok();
        }

        // DELETE: api/somiod/datas/5
        [Route("datas/{id}")]
        public IHttpActionResult DeleteData(int id)
        {
            int success = SqlDataHelper.deleteData(id);

            if (success == 0)
            {
                return InternalServerError();
            }
            return Ok();
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
