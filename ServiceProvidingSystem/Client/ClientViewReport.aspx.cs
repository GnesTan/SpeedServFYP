using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.Client
{
    public partial class ClientViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ib1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Client/ServiceRequestReportSetting.aspx");
        }

        protected void ib2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Client/TransactionReportSetting.aspx");
        }
    }
}