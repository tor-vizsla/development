using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Collections.Specialized;
using System.Text;

namespace development
{
    public partial class testSend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


    [System.Web.Services.WebMethod]
    public static string TestSend(String Mobile)
    {
        StringBuilder sb = new StringBuilder();
        //POST Example = send pin
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("appId", "D11E5CFD-4528-4547-A13E-F9E62772142D");

            SendData data = new SendData();
            data.mobile = "07890664662";

            HttpResponseMessage response = client.PostAsJsonAsync("http://127.0.0.1/api/v1/send", data).Result;
            string responseBody = response.Content.ReadAsAsync<string>().Result;
            JObject results = JObject.Parse(responseBody);
            if (response.IsSuccessStatusCode)
            {
                sb.Append( (string)results["Message"]);
                sb.Append("<br/>");
                sb.Append((string)results["Links"]);
                }
            else
            {
                // send failed with content
                sb.Append( (string)results["Message"]);
            }
           
        }
        return sb.ToString();
    }
    private class SendData
    {
        public string mobile { get; set; }
    }
    private class SendResponse
    {
        public string Message { get; set; }
    }        
    }


}