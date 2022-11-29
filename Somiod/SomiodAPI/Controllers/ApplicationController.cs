using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SomiodAPI.Controllers
{
    [RoutePrefix("api/somiod")]
    public class ApplicationController : ApiController
    {

        List<Application> applications = new List<Application>
        {
            new Application {Id = 1, Name = "Lamp", Creation_dt = "2002-09-28 12:34:23"}
        };

        // GET: api/Application
        [Route("applications")]
        public IEnumerable<Application> GetAllApplications()
        {
            return applications;
        }

        // GET: api/Application/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Application
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Application/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Application/5
        public void Delete(int id)
        {
        }
    }
}
