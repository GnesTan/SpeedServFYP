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

        protected void btnHome_Click(object sender, EventArgs e)
        {
            //to verify backend user login credential
            String userType = "";

            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }


            if (userType.Equals("Backend"))
            {
                Response.Redirect("~/BackendUser/BackEndHomepage.aspx");
            }
            else if (userType.Equals("Servicer"))
            {
                Response.Redirect("~/Servicer/RequestList.aspx");
            }
            else
            {
                Response.Redirect("~/Client/ClientHomePage.aspx");
            }

        }

    }



}