using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Text;
using PayPal.Api;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace development
{
    public partial class testPaypal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static string PurchaseBundle(int BundleAmount)
        {
            // sandbox account: info-facilitator@mobileverify.co.uk
            // sandbox customer info-buyer@mobileverify.co.uk = p1nkfl0yd

            //string apiUri = "https://api.sandbox.paypal.com";
            //string apiUri = "https://api.paypal.com";

            string client_id = "Aena3ntSjQJqG9qreGdIco_Gz8ZvT8ER9mJvqTMGs7yd-101IU7tPym6IpG3HN-MGyKlRYmgWao9CcXN";
            string secret = "ECXV2PwPM-olaswL9VJ0xrBPmfhRM-onnC9nnWAFlnC1bSZegrT29sJlJ41jFYjBUIIyDVdn_TDq9YS4";

            //stage 1) get an access token

            StringBuilder sb = new StringBuilder();

            Dictionary<string, string> sdkConfig = new Dictionary<string, string>();
            sdkConfig.Add("mode", "sandbox");
            string accessToken = new OAuthTokenCredential(client_id, secret, sdkConfig).GetAccessToken();

            APIContext apiContext = new APIContext(accessToken);
            apiContext.Config = sdkConfig;

            Amount bundleAmount = new Amount();
            bundleAmount.currency = "GBP";
            bundleAmount.total = "55.00";
            

            List<Transaction> transactionList = new List<Transaction>();
            Transaction tran = new Transaction();
            tran.description = "SMS Bundle";
            tran.amount = bundleAmount;
            transactionList.Add(tran);

            Payer payr = new Payer();
            payr.payment_method = "paypal";

            RedirectUrls redirUrls = new RedirectUrls();
            redirUrls.cancel_url = "https://devtools-paypal.com/guide/pay_paypal/dotnet?cancel=true";
            redirUrls.return_url = "https://devtools-paypal.com/guide/pay_paypal/dotnet?success=true";

            Payment pymnt = new Payment();
            pymnt.intent = "sale";
            pymnt.payer = payr;
            pymnt.transactions = transactionList;
            pymnt.redirect_urls = redirUrls;

            Payment createdPayment = pymnt.Create(apiContext);

            //debug links

            // Find items where rel contains "approval_url".
            

            sb.Append(createdPayment.links[1].href);
            return sb.ToString();
        }

        private class SendData
        {
            public string grant_type { get; set; }
        }
        private class myLinks
        {
            public string href { get; set; }
            public string rel { get; set; }
            public string method { get; set; }
        }
        private class mobVerifySubscription
        {
            public mobVerifySubscription()
            {
                // Use private setter in the constructor.
                name = "Annual subscription";
                description = "Annual subscription for two factor authentication services";
                type = "fixed";

                payment_definitions.Add("name", "Annual subscription");
                payment_definitions.Add("type", "REGULAR");
                payment_definitions.Add("frequency_interval", "");
                payment_definitions.Add("frequency", "");
                payment_definitions.Add("cycles", "");

                charge_models.Add("type","TAX");


            }
            public string name { get; private set; }
            public string description { get; private set; }
            public string type { get; private set; }

            public Dictionary<string,string> payment_definitions {get; private set;}
            public amount pay_def_amount { get; set; }

            public Dictionary<string, string> charge_models { get; private set; }
            public amount charge_models_amount { get; set; }
        }
       
        private class amount
        {
            public amount()
            {
                currency = "GBP";
            }
            public string currency { get; private set; }
            public double value { get; set; }
        }
    }


}