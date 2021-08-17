using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ErrorCode.Text = Application["ErrorCode"].ToString();
            ErrorMessage.Text = Application["ErrorMessage"].ToString();
        }
    }
}