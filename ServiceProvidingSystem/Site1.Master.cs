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
            //if (!IsPostBack)
            //{



            //    try
            //    {

            //        bool isLogin = (System.Web.HttpContext.Current.User != null) && System.Web.HttpContext.Current.User.Identity.IsAuthenticated;

            //        if (Session["USERNAME"] == null && isLogin)
            //        {

            //            FormsAuthentication.SignOut();

            //            Response.Redirect(Request.Url.AbsoluteUri, false);

            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        if (ex != null)
            //        {
            //            String exMessage = ex.Message;
            //            Application["ErrorMessage"] = exMessage;
            //        }
            //        Application["ErrorCode"] = " ";
            //        Response.Redirect("~/ErrorPage.aspx");
            //    }
            //}

            //string strFeatured;
            //Control ctlControl;

            //if (Roles.GetRolesForUser().Contains("Artist"))
            //{
            //    strFeatured = "ArtistHeader.ascx";
            //}
            //else
            //{
            //    strFeatured = "CustomerHeader.ascx";
            //}

            //ctlControl = LoadControl(strFeatured);
            //headerControl.Controls.Add(ctlControl);
        }

    }

}
