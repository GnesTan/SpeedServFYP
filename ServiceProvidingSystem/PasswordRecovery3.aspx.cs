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
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            String newPassword = txtNewPassword.Text;

            String dbTable = Session["recoveryUserType"].ToString();
            String emailAddress = Session["emailAddress"].ToString();

            con.Open();
            try
            {

                //update servicer password
                String strUpdate = "UPDATE " + dbTable + " SET password = @newPassword WHERE email_address = @emailAddress;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@newPassword", newPassword);
                cmdUpdate.Parameters.AddWithValue("@emailAddress", emailAddress);

                cmdUpdate.ExecuteNonQuery();

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