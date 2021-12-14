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
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);


        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                lblError.Text = "";
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            String emailAddress = txtEmail.Text;
            String recoveryUserType = "";
            String fullName = "";

            int emailExistServicer = 0;
            int emailExistClient = 0;

            con.Open();
            try
            {
                //retrieve data


                SqlCommand check_servicer_email = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (email_address = @email)", con);
                check_servicer_email.Parameters.AddWithValue("@email", emailAddress);
                emailExistServicer += (int)check_servicer_email.ExecuteScalar();

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

            
            if (emailExistServicer > 0)
            {
                //mark the user is servicer
                recoveryUserType = "Servicer";

                //get user's name
                con.Open();
                try
                {
                    //retrieve data
                    String strSelect = "SELECT * FROM Servicer WHERE email_address = @email;";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@email", emailAddress);
                    SqlDataReader dataReader;
                    dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        fullName = dataReader["full_name"].ToString();
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
            }


            con.Open();
            try
            {
                //retrieve data
                SqlCommand check_client_email = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (email_address = @email)", con);
                check_client_email.Parameters.AddWithValue("@email", emailAddress);
                emailExistClient += (int)check_client_email.ExecuteScalar();

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

            if (emailExistClient > 0)
            {
                //mark the user is servicer
                recoveryUserType = "Client";

                //get user's name
                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM Client WHERE email_address = @email;";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@email", emailAddress);
                    SqlDataReader dataReader;
                    dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        fullName = dataReader["full_name"].ToString();
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
            }


            if (emailExistServicer > 0 || emailExistClient > 0)
            {
                Session["emailAddress"] = emailAddress;
                Session["recoveryUserType"] = recoveryUserType;
                Session["fullName"] = fullName;
                Response.Redirect("~/PasswordRecovery2.aspx");
            }
            else
            {
                lblError.Text = "The Email is not registered.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void imgBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}