using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        String table = "Back_end_User";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);


        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Backend"))
                {
                    Response.Redirect("~/StaffLogin.aspx");
                }


            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/AccountMaintenance.aspx");
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            String strUsername = "";
            String strFullname = "";
            String strPassword = "";

            strUsername = txtUsername.Text;
            strFullname = txtName.Text;
            strPassword = txtPassword.Text;

            //to determine whether is there any record with same username
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
                recordFound++;
            }

            con.Close();

            if(recordFound == 0)
            {
                con.Open();

                try
                {

                    //retrieve data
                    strAdd = "INSERT INTO " + table + "(USERNAME, FULL_NAME, PASSWORD) VALUES (@USERNAME, @FULL_NAME, @PASSWORD);";

                    cmdAdd = new SqlCommand(strAdd, con);

                    cmdAdd.Parameters.AddWithValue("@USERNAME", strUsername);
                    cmdAdd.Parameters.AddWithValue("@FULL_NAME", strFullname);
                    cmdAdd.Parameters.AddWithValue("@PASSWORD", strPassword);

                    cmdAdd.ExecuteNonQuery();

                    con.Close();

                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Account have successful created.');window.location.href='AccountMaintenance.aspx';", true);


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
            else
            {
                lblErrorUsername.Text = "Usersame have been used, please try again with another username.";
            }





        }
    }
}