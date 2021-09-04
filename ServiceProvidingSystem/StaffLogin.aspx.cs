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
    public partial class StaffLogin : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnCustomerLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //get text from controls
            String strUsername = txtUsername.Text;
            String strPassword = txtPassword.Text;

            //declare all needed information
            String strRetrieveName = "";
            String strRetrievePassword = "";
            int recordFound = 0;


            //check from admin database
            con.Open();

            //retrieve data
            String strAdd = "SELECT * FROM Back_end_User WHERE USERNAME = @USERNAME;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@USERNAME", strUsername);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strRetrievePassword = dataReader["PASSWORD"].ToString();
                recordFound++;
            }

            con.Close();


            //check is the password matched
            if (recordFound > 0 && strPassword.Equals(strRetrievePassword))
            {
                //save id and name to session
                Session["strUsername"] = strUsername;
                Session["strRetrieveName"] = strRetrieveName;
                Session["userType"] = "Backend";
                //redirect user to homepage according the type of user they are


                Response.Redirect("~/BackendUser/BackEndHomepage.aspx");
            }
            else
            {
                //display error if record not found or password is invalid
                lblWrong.Text = "The email address or password is invalid.";
            }



        }
    }
}