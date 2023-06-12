using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Routing;

namespace MVC_App_WebAPI.Controllers
{

    
    public class APIController : ApiController
    {
        static int i = 1;
        [Route("api/get/{id}")]
        public IHttpActionResult Get([FromUri]int id) //mentioning that will be getting attribute from url only
        {
            i++;
            return Ok(i);
        }
    }
}
