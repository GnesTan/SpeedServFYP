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
    public partial class PasswordRecovery3 : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            String newPassword = txtNewPassword.Text;

            String dbTable = Session["recoveryUserType"].ToString();
            String emailAddress = Session["emailAddress"].ToString();


            try
            {
                con.Open();

            //update servicer password
                String strAdd = "UPDATE " + dbTable + " SET password = @newPassword WHERE email_address = @emailAddress;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                cmdAdd.Parameters.AddWithValue("@newPassword", newPassword);
                cmdAdd.Parameters.AddWithValue("@emailAddress", emailAddress);

                cmdAdd.ExecuteNonQuery();

                con.Close();
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


            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('New password had set successfully.');window.location.href='Login.aspx';", true);




        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

           Response.Redirect("~/Login.aspx");


        }


    }
}