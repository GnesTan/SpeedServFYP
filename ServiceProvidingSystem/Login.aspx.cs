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
            Response.Redirect("~/StaffLogin.aspx");
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

                    Response.Redirect("Servicer/RequestList.aspx");
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