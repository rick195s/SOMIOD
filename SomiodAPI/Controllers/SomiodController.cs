using SomiodAPI.Helpers;
using SomiodAPI.Models;
using SomiodAPI.SqlHelpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
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
            Module module = SqlModuleHelper.GetModule(id, true);

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
            
            if (value == null)
            {
                return BadRequest("Error deserializing object");
            }

            if (value?.Res_type.ToUpper() == "DATA")
            {
                Data data = SqlDataHelper.CreateData(new Data(value), applicationName, moduleName);
                if (data == null){
                    return InternalServerError();
                }

                MosquittoHelper.PublishData(IPAddress.Parse("127.0.0.1"), "creation", moduleName, data);
                return Ok(data);
            }

            if (value?.Res_type.ToUpper() == "SUBSCRIPTION")
            {
                Subscription subscription = SqlSubscriptionHelper.CreateSubscription(new Subscription(value), applicationName, moduleName);
                if (subscription == null){
                    return InternalServerError();
                }
                return Ok(subscription);
            }

            return BadRequest("Res_type not recognized");
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
                return NotFound();
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
                return NotFound();
            }

            string moduleName = SqlModuleHelper.GetModule(data.Parent).Name;

            MosquittoHelper.PublishData(IPAddress.Parse("127.0.0.1"), "deletion", moduleName, data);
            return Ok(data);
        }

        // DELETE: api/somiod/subscriptions/5
        [Route("subscriptions/{id}")]
        public IHttpActionResult DeleteSubscriptions(int id)
        {
            Subscription subscription = SqlSubscriptionHelper.DeleteSubscription(id);

            if (subscription == null)
            {
                return NotFound();
            }
            return Ok(subscription);
        }
        #endregion
    }
}
