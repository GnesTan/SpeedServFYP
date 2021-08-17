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
    public partial class Register2 : System.Web.UI.Page
    {
        String table = "Servicer";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterUser"] != null)
            {
                lblUser.Text = Session["RegisterUser"].ToString();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string emailAddress = "";
            string password = "";
            string conPassword = "";

            string strExist = "";
            string strNotMatch = "";

            int errorCount = 0;

            emailAddress = txtEmail.Text;
            password = txtPassword.Text;
            conPassword = txtConfirmPass.Text;

            int emailExist = 0;

            con.Open();

            //retrieve data


            SqlCommand check_servicer_email = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (email_address = @email)", con);
            check_servicer_email.Parameters.AddWithValue("@email", emailAddress);
            emailExist += (int)check_servicer_email.ExecuteScalar();

            con.Close();

            con.Open();

            //retrieve data


            SqlCommand check_client_email = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (email_address = @email)", con);
            check_client_email.Parameters.AddWithValue("@email", emailAddress);
            emailExist += (int)check_client_email.ExecuteScalar();

            con.Close();


            if (emailExist > 0)
            {
                strExist = "Email entered already registered by another user, please try another email.";
                lblEmailExist.Text = strExist;
                errorCount += 1;
            }

            if (!password.Equals(conPassword))
            {
                strNotMatch = "Password and Confirm Password are not matched, please try again.";
                lblNotMatch.Text = strNotMatch;
                errorCount += 1;
            }


            if (errorCount == 0)
            {
                Session["emailAddress"] = emailAddress;
                Session["password"] = password;

                Response.Redirect("Register3.aspx");
            }





        }
    }
}