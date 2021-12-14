using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class BackendReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //to verify backend user login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Backend"))
                {
                    Response.Redirect("~/StaffLogin.aspx");
                }
            }
        }

        protected void ib1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/BackendUser/SubscriptionVolumeReportSetting.aspx");
        }

        protected void ib2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/BackendUser/SalesTransactionReportSetting.aspx");
        }

        protected void ib3_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('UserTypeRatioAnalysisReport.aspx');", true);
        }
    }
}