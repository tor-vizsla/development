using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace development.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "post")]
    public class sendController : ApiController
    {
        // POST: api/send
        [Route("api/send")]
        [Route("api/v1/send")]
        public IHttpActionResult Post([FromBody]SendData postData)
        {
            // send should return 201
            
            SendResponse obj = new SendResponse();
            string json = "";
            obj.Message = "all good";
            obj.Links = "my liunk";

            json = JsonConvert.SerializeObject(obj);
            return Ok(json);
            //return Created("http://127.0.0.1",json);
        }

        public class SendData
        {
            public string mobile { get; set; }
        }
        public class SendResponse
        {
            public string Message { get; set; }
            public string Links { get; set; }
        }
    }
}
