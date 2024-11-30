using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntertainmentApp
{
    public partial class sessionExpired : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string source = Request.QueryString["source"];
            if (!string.IsNullOrEmpty(source))
            {
                // Pass the source to JavaScript
                ClientScript.RegisterStartupScript(this.GetType(), "SetSource",
                    $"var source = '{source}';", true);
            }
            else
            {
                // Default redirection if no source is provided
                ClientScript.RegisterStartupScript(this.GetType(), "SetSource",
                    "var source = 'default';", true);
            }
        }
    }
}