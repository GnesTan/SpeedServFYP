using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem
{
    public partial class BackendHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                try
                {
                    String strRetrieveName = "";

                    if (Session["strRetrieveName"] != null)
                    {
                        strRetrieveName = Session["strRetrieveName"].ToString();
                    }

                    welcomeName.Text = "Welcome back, " + strRetrieveName;

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

        protected void ibProfile_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Servicer/ServicerViewProfile.aspx");
        }
    }
}