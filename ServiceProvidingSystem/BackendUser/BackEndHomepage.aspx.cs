using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class BackEndHomepage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
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
    }
}