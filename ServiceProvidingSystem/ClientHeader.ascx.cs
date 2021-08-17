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
    }
}