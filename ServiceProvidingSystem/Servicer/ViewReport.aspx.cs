using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.Servicer
{
    public partial class ViewReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //to verify servicer login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Servicer"))
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void ib1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Servicer/ServiceDetailsReportSetting.aspx");
        }

        protected void ib2_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Servicer/AnnualServiceAnalysisReportSetting.aspx");
        }

        protected void ib3_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('CustomerReviewReport.aspx');", true);
        }

        protected void ib4_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('WalletTransactionReport.aspx');", true);
        }
    }
}