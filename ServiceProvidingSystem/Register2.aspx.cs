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
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

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

            int errorCount = 0;

            emailAddress = txtEmail.Text;
            password = txtPassword.Text;
            conPassword = txtConfirmPass.Text;

            int emailExist = 0;

            con.Open();
            try
            {
                //retrieve data
                SqlCommand check_servicer_email = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (email_address = @email)", con);
                check_servicer_email.Parameters.AddWithValue("@email", emailAddress);
                emailExist += (int)check_servicer_email.ExecuteScalar();

            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    String message = ex.Message;
                    Application["ErrorMessage"] = message;
                }
                Application["ErrorCode"] = " ";
                Response.Redirect("~/ErrorPage.aspx");
            }
            finally
            {
                con.Close();
            }

            con.Open();
            try
            {
                //retrieve data
                SqlCommand check_client_email = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (email_address = @email)", con);
                check_client_email.Parameters.AddWithValue("@email", emailAddress);
                emailExist += (int)check_client_email.ExecuteScalar();


            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    String message = ex.Message;
                    Application["ErrorMessage"] = message;
                }
                Application["ErrorCode"] = " ";
                Response.Redirect("~/ErrorPage.aspx");
            }
            finally
            {
                con.Close();
            }


            if (emailExist > 0)
            {
                strExist += "*Email entered already registered by another user, please try another email. <br/>";
                errorCount += 1;
            }

            if (!password.Equals(conPassword))
            {
                strExist += "*Password and Confirm Password are not matched, please try again. <br/>";
                errorCount += 1;
            }

            lblError.Text = strExist;

            if (errorCount == 0)
            {
                Session["emailAddress"] = emailAddress;
                Session["password"] = password;

                Response.Redirect("Register3.aspx");
            }





        }
    }
}