using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace development
{
    public partial class show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string token = Request.Form["token"].ToString();
            Label1.Text = "token: " + token;
        }
    }
}