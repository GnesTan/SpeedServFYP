using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ServiceProvidingSystem
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Login1_LoggedIn1(object sender, EventArgs e)
        {
            //if (Roles.IsUserInRole(Login1.UserName, "Artist"))
            //{
            //    Session["USERNAME"] = Login1.UserName;

            //    //Get customer id when customer login.
            //    String str = ConfigurationManager.ConnectionStrings["ServiceProvidingSystemDB"].ConnectionString;
            //    String strSel = "SELECT ARTIST_ID FROM Artist WHERE USERNAME = @USERNAME";

            //    using (SqlConnection con = new SqlConnection(str))
            //    {
            //        SqlCommand cmd = new SqlCommand(strSel, con);
            //        SqlParameter paraArtist_ID = new SqlParameter()
            //        {
            //            ParameterName = "@USERNAME",
            //            Value = Session["USERNAME"].ToString()
            //        };
            //        cmd.Parameters.Add(paraArtist_ID);

            //        con.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            Session["ARTIST_ID"] = reader.GetInt32(0);
            //        }
            //        con.Close();
            //    }

            //    Response.Redirect("~/Homepage.aspx");
            //}
            //else if (Roles.IsUserInRole(Login1.UserName, "Customer"))
            //{
            //    Session["USERNAME"] = Login1.UserName;

            //    //Get customer id when customer login.
            //    int customerid = 0;
            //    String str = ConfigurationManager.ConnectionStrings["ServiceProvidingSystemDB"].ConnectionString;
            //    String strSel = "SELECT CUSTOMER_ID FROM CUSTOMER WHERE USERNAME =@USERNAME";

            //    using (SqlConnection con = new SqlConnection(str))
            //    {
            //        SqlCommand cmd = new SqlCommand(strSel, con);
            //        SqlParameter paraCust_ID = new SqlParameter()
            //        {
            //            ParameterName = "@USERNAME",
            //            Value = Session["USERNAME"].ToString()
            //        };
            //        cmd.Parameters.Add(paraCust_ID);

            //        con.Open();
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
            //            customerid = reader.GetInt32(0);
            //            Session["CUST_ID"] = customerid;
            //        }
            //        con.Close();
            //    }

            //    Response.Redirect("~/Homepage.aspx");
            //}
        }
    }
}