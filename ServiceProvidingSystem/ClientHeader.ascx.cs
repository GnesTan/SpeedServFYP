using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace ServiceProvidingSystem
{
    public partial class ClientHeader : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strId"] != null)
            {
                liLog.InnerHtml = "SignOut";
                displayName.InnerText = "Hi, " + Session["strRetrieveName"].ToString();
            }
            else
            {
                liLog.InnerText = "Login";
            }
        }

        protected void liFav_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login before proceed to next step'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("Favourite.aspx");
            }
        }

        protected void liView_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login before proceed to next step'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("ViewProfile.aspx");
            }
        }

        protected void liRequest_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login before request service'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("PostServiceRequest.aspx");
            }
        }

        protected void liRequestStatus_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login to proceed'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("RequestStatusList.aspx");
            }
        }
        protected void liLog_Click(object sender, EventArgs e)
        {
            if (Session["strId"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (liLog.InnerText == "SignOut")
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void liPay_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login to proceed'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("PaymentList.aspx");
            }
        }

        protected void liTransact_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login to proceed'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("RecentTransaction.aspx");
            }
        }

        protected void liService_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ClientHomePage.aspx#services");
        }

        protected void liReport_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login to proceed'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("ClientViewReport.aspx");
            }
        }
    }
}