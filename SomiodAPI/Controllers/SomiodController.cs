using SomiodAPI.Models;
using SomiodAPI.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
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

            Module module = SqlModuleHelper.CreateModule(value, applicationName);

            if (module == null)
            {
                return InternalServerError();
            }
            return Ok(module);
            
        }

        // POST: api/somiod/Lamp/Light
        [Route("{applicationName}/{moduleName}")]
        public IHttpActionResult PostSubscription_Data([FromBody] Subscription_Data value, string applicationName, string moduleName)
        {
            Data data = null;
            Subscription subscription = null;
            bool rest_typeData = false;

            if (value.Res_type.ToUpper() == "DATA")
            {
                data = SqlDataHelper.CreateData(value, applicationName, moduleName);
                rest_typeData = true;
            }

            if (value.Res_type.ToUpper() == "SUBSCRIPTION")
            {
                subscription = SqlSubscriptionHelper.CreateSubscription(value, applicationName, moduleName);
            }

            if (data == null && subscription == null)
            {
                return InternalServerError();
            }
            //TODO - arranjar alguma maneira para devolver quando é Data e quando é Subscription
            //return rest_typeData ? Ok(data) : Ok(subscription);
            
            if (rest_typeData)
            {
                return Ok(data);
            }
            return Ok(subscription);

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
            Module module = SqlModuleHelper.UpdateModule(value, id);

            if (module == null)
            {
                return InternalServerError();
            }
            return Ok(module);
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
            Module module = SqlModuleHelper.DeleteModule(id);

            if (module == null)
            {
                return InternalServerError();
            }
            return Ok(module);
        }

        // DELETE: api/somiod/datas/5
        [Route("datas/{id}")]
        public IHttpActionResult DeleteData(int id)
        {
            Data data = SqlDataHelper.DeleteData(id);

            if (data == null)
            {
                return InternalServerError();
            }
            return Ok(data);
        }

        // DELETE: api/somiod/subscriptions/5
        [Route("subscriptions/{id}")]
        public IHttpActionResult DeleteSubscriptions(int id)
        {
            Subscription subscription = SqlSubscriptionHelper.DeleteSubscription(id);

            if (subscription == null)
            {
                return InternalServerError();
            }
            return Ok(subscription);
        }
        #endregion
    }
}
