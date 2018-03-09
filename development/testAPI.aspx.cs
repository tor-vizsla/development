using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace development
{
    public partial class testAPI : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //POST Example = verify pin
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("appId", "D11E5CFD-4528-4547-A13E-F9E62772142D");

                VerificationData data = new VerificationData();
                data.mobile = "07890664662";
                data.pin = "12345";

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

                HttpResponseMessage response = client.PostAsJsonAsync("http://127.0.0.1/api/verify",data).Result;

                //var reply = Newtonsoft.Json.JsonConvert.DeserializeObject<VerificationResponse>(response.ToString());

                if (response.IsSuccessStatusCode)
                {
                    // we have validated this pin and mobile
                    //Label1.Text = "StatusCode:" + response.StatusCode.ToString() + "<br/>Message:" + reply.Message;
                    Label1.Text = response.ToString();
                }
                else {
                    // failed validation with response phrase:
                    // 
                    //Label1.Text = "StatusCode:" + response.StatusCode.ToString() + "<br/>Message:" + reply.Message;
                    Label1.Text = response.ToString();
                }
                
            }



        }
        class VerificationData
        {
            public string mobile { get; set; }
            public string pin { get; set; }
        }
        public class VerificationResponse
        {
            public string Message { get; set; }
        }

    }
}