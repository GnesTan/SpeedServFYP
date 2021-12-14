using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace ServiceProvidingSystem
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {



            try
            {

                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }

                String strFeatured;
                Control ctlControl;

                if (userType.Equals("Servicer"))
                {
                    strFeatured = "~/ServicerHeader.ascx";
                }
                else if (userType.Equals("Backend"))
                {
                    strFeatured = "~/BackendHeader.ascx";
                }
                else
                {
                    strFeatured = "~/ClientHeader.ascx";
                }

                ctlControl = LoadControl(strFeatured);
                headerControl.Controls.Add(ctlControl);



            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    String exMessage = ex.Message;
                    Application["ErrorMessage"] = exMessage;
                }
                Application["ErrorCode"] = " ";
                Response.Redirect("~/ErrorPage.aspx");
            }






        }

    }

}
