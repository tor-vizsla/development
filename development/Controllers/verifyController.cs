using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace development.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "get,post")]
    public class verifyController : ApiController
    {
        // POST:
        [Route("api/verify")]
        [Route("api/v1/verify")]
        public IHttpActionResult Post([FromBody]VerificationData postData)
        {
            VerificationResponse obj = new VerificationResponse();
            var appid = "";

            #region 1) check the appid header
            try
            {
                // get the authentication/appid header
                IEnumerable<string> headerValues = Request.Headers.GetValues("appid");
                appid = headerValues.FirstOrDefault();

                // 2) validate the format of appid
                if (!IsValidGuid(appid))
                {
                    // appid is not formatted correctly or null or empty
                    return BadRequest("The value provided for one of the HTTP headers was not in the correct format");
                }
            }
            catch (Exception)
            {
                // no appid header
                return BadRequest("a required HTTP header was not supplied");
            }
            #endregion

            #region 2) check the mobile parameter
            if (!IsDigitsOnly(postData.mobile))
            {
                return BadRequest("An invalid value was specified for one of the query parameters in the request URI");
            }
            #endregion

            #region 3) check the pin parameter
            if (!IsDigitsOnly(postData.pin))
            {
                return BadRequest("An invalid value was specified for one of the query parameters in the request URI");
            }
            else
            {
                // check the pin is 5 digits long
                if (postData.pin.Length != 5)
                {
                    return BadRequest("An invalid value was specified for one of the query parameters in the request URI");
                }
            }
            #endregion


            #region 4) call verification stored proc
            string dbRetVal = "";
            string dbErrorMessage = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connMobVerify"].ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("app.verify", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("appid", appid);
                        cmd.Parameters.AddWithValue("mobile", postData.mobile);
                        cmd.Parameters.AddWithValue("pin", postData.pin);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    dbRetVal = reader["retVal"].ToString();
                                }
                            }
                        }

                    }
                }
            }
            catch (SqlException dbex)
            {
                // database error
                dbRetVal = "dbError";
                dbErrorMessage = dbex.ToString();
            }
            #endregion

            string json = "";

            switch (dbRetVal)
            {
                case "success":
                    obj.Message = "success";
                    json = JsonConvert.SerializeObject(obj);
                    return Ok(json);
                    //break;
                case "failed":
                    obj.Message = "failed";
                    json = JsonConvert.SerializeObject(obj);
                    return Ok(json);
                    //break;
                case "unauthorised":
                    return BadRequest("Unauthorised");
                    //break;
                case "dbError":
                    obj.Message = dbErrorMessage;
                    json = JsonConvert.SerializeObject(obj);
                    return Ok(json);
                    //break;
                case "":
                    obj.Message = "database returned nothing";
                    json = JsonConvert.SerializeObject(obj);
                    return Ok(json);
                    //break;
                default:
                    obj.Message = "unknown error";
                    json = JsonConvert.SerializeObject(obj);
                    return Ok(json);
                    //break;
            }

        }

        public class VerificationData
        {
            public string mobile { get; set; }
            public string pin { get; set; }
        }
        public class VerificationResponse
        {
            public string Message { get; set; }
        }

        private static bool IsValidGuid(string str)
        {
            Guid guid;
            return Guid.TryParse(str, out guid);
        }

        private bool IsDigitsOnly(string str)
        {
            if (str.Length == 0)
            {
                return false;
            }
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
