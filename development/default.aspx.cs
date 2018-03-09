using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Http;
using System.Net.Http.Headers;

namespace development
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static string Test(string var1)
        {
            string token = "8a9c2d66-b00d-4337-8e1e-b199d67bf1fe";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("appId", "eff38a37-8f87-401a-bde8-ec9e32417a8b");
                client.DefaultRequestHeaders.Add("appSecret", "b7a94f39-c5a4-4ae8-8aa9-5a96d450034");

                //VerificationData data = new VerificationData();
                //data.mobile = "07890664662";
                //data.pin = "12345";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(token);

                HttpResponseMessage response = client.PostAsJsonAsync("http://192.168.0.17/api/auth", token).Result;

                var reply = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationResponse>(response.ToString());

                if (response.IsSuccessStatusCode)
                {
                    // we have validated this pin and mobile
                    //Label1.Text = "StatusCode:" + response.StatusCode.ToString() + "<br/>Message:" + reply.Message;
                    //return response.ToString();
                    return reply.Message;
                }
                else
                {
                    // failed validation with response phrase:
                    // 
                    //Label1.Text = "StatusCode:" + response.StatusCode.ToString() + "<br/>Message:" + reply.Message;
                    return reply.Message;
                }

            }

            
        }

        public class VerificationResponse
        {
            public string Message { get; set; }
        }
    }
}