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
    public partial class ResetPassword : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["strId"] == null)
                {
                    lblError.Text = "";

                    Response.Redirect("~/Login.aspx", true);

                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            String currentPassword = txtCurrentPassword.Text;
            String newPassword = txtNewPassword.Text;

            String userType = Session["userType"].ToString();
            String strId = Session["strId"].ToString();
            String dbPassword = "";

            String dbTable = "";
            String idType = "";

            if (userType.Equals("Servicer"))
            {
                dbTable = "Servicer";
                idType = "servicer_id";

            }
            else
            {
                dbTable = "Client";
                idType = "client_id";

            }

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM " + dbTable + " WHERE " + idType + " = @strId;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@idType", idType);
                cmdSelect.Parameters.AddWithValue("@strId", strId);
                SqlDataReader dataReader;
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    dbPassword = dataReader["password"].ToString();
                }

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


            if (currentPassword.Equals(dbPassword))
            {
                try
                {
                    con.Open();

                    //update servicer password
                    String strUpdate = "UPDATE " + dbTable + " SET password = @newPassword WHERE " + idType + " = @strId;";

                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                    cmdUpdate.Parameters.AddWithValue("@newPassword", newPassword);
                    cmdUpdate.Parameters.AddWithValue("@strId", strId);

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


                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Password reset successfully.');window.location.href='/Servicer/ServicerViewProfile.aspx';", true);



            }
            else
            {
                lblError.Text = "The current password is invalid.";

            }






        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["userType"].ToString().Equals("Servicer"))
            {
                Response.Redirect("~/Servicer/ServicerViewProfile.aspx");
            }
            else
            {
                Response.Redirect("~/Client/ViewProfile.aspx");
            }
        }
    }
}