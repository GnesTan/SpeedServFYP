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

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

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


        protected void hlServicer_Click(object sender, EventArgs e)
        {
            //go to Register page for Servicer
            Session["RegisterUser"] = "Servicer";

            Response.Redirect("Register.aspx");
        }

        protected void hlClient_Click(object sender, EventArgs e)
        {
            //go to Register page for Client
            Session["RegisterUser"] = "Client";

            Response.Redirect("Register.aspx");
        }

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //get text from controls
            String strEmail = txtEmail.Text;
            String strPassword = txtPassword.Text;

            //declare all needed information
            String strId = "";
            String strRetrieveName = "";
            String strRetrievePassword = "";
            int recordFound = 0;
            String userType = "Servicer";


            //check from servicer database
            con.Open();

            //retrieve Servicer data
            String strAdd = "SELECT * FROM Servicer WHERE email_address = @email_address;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@email_address", strEmail);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strId = dataReader["servicer_id"].ToString();
                strRetrieveName = dataReader["full_name"].ToString();
                strRetrievePassword = dataReader["password"].ToString();
                recordFound++;
                userType = "Servicer";
            }

            con.Close();

            //check from client database
            con.Open();

            //retrieve data
            strAdd = "SELECT * FROM Client WHERE email_address = @email_address;";

            cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@email_address", strEmail);
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strId = dataReader["client_id"].ToString();
                strRetrieveName = dataReader["full_name"].ToString();
                strRetrievePassword = dataReader["password"].ToString();
                recordFound++;
                userType = "Client";
            }

            con.Close();


            //check is the password matched
            if (recordFound > 0 && strPassword.Equals(strRetrievePassword))
            {
                //save id and name to session
                Session["strId"] = strId;
                Session["strRetrieveName"] = strRetrieveName;
                Session["userType"] = userType;
                //redirect user to homepage according the type of user they are
                if (userType.Equals("Servicer"))
                {

                    Response.Redirect("Servicer/ServicerHomepage.aspx");
                }
                else
                {

                    Response.Redirect("Client/ClientHomepage.aspx");
                }
            }
            else
            {
                //display error if record not found or password is invalid
                lblWrong.Text = "The email address or password is invalid.";
            }



        }
    }
}