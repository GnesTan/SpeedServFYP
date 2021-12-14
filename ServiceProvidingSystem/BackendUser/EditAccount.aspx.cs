using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;


namespace ServiceProvidingSystem.BackendUser
{
    public partial class EditAccount : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Back_end_User";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //to verify backend user login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Backend"))
                {
                    Response.Redirect("~/StaffLogin.aspx");
                }



                String selectedUsername = "";
                String strFullname = "";
                String strPassword = "";

                //get username selected by user
                if(Session["selectedUsername"] != null)
                {
                    selectedUsername = Session["selectedUsername"].ToString();
                }


                //check from admin database
                con.Open();
                try
                {

                    //retrieve data
                    String strAdd = "SELECT * FROM Back_end_User WHERE USERNAME = @USERNAME;";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                    cmdAdd.Parameters.AddWithValue("@USERNAME", selectedUsername);
                    SqlDataReader dataReader;
                    dataReader = cmdAdd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        strFullname = dataReader["FULL_NAME"].ToString();
                        strPassword = dataReader["PASSWORD"].ToString();
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

                //place all record to textfield
                txtUsername.Text = selectedUsername;
                txtName.Text = strFullname;
                txtPassword.Attributes["value"] = strPassword;
                txtConPass.Attributes["value"] = strPassword;


            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/BackendUser/AccountMaintenance.aspx");
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //get input from textfield
            String strFullname = "";
            String strPassword = "";

            strFullname = txtName.Text;
            strPassword = txtPassword.Text;



            //update user info
            con.Open();

            try
            {

                String selectedUsername = "";

                if (Session["selectedUsername"] != null)
                {
                    selectedUsername = Session["selectedUsername"].ToString();
                }

                //update data
                String strUpdate = "UPDATE " + table + " SET FULL_NAME = @FULL_NAME, PASSWORD = @PASSWORD WHERE USERNAME = @USERNAME;";
                
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@FULL_NAME", strFullname);
                cmdUpdate.Parameters.AddWithValue("@PASSWORD", strPassword);
                cmdUpdate.Parameters.AddWithValue("@USERNAME", selectedUsername);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                lblSucessful.Text = "Save successfully.";


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

    }
}