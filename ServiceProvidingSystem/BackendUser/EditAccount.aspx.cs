using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class EditAccount : System.Web.UI.Page
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



                String selectedUsername = "";
                String strFullname = "";
                String strPassword = "";

                if(Session["selectedUsername"] != null)
                {
                    selectedUsername = Session["selectedUsername"].ToString();
                }


                //check from admin database
                con.Open();

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

                con.Close();

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
            String strFullname = "";
            String strPassword = "";

            strFullname = txtName.Text;
            strPassword = txtPassword.Text;




            con.Open();

            try
            {

                String selectedUsername = "";

                if (Session["selectedUsername"] != null)
                {
                    selectedUsername = Session["selectedUsername"].ToString();
                }

                //retrieve data
                String strAdd = "UPDATE " + table + " SET FULL_NAME = @FULL_NAME, PASSWORD = @PASSWORD WHERE USERNAME = @USERNAME;";
                
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@FULL_NAME", strFullname);
                cmdAdd.Parameters.AddWithValue("@PASSWORD", strPassword);
                cmdAdd.Parameters.AddWithValue("@USERNAME", selectedUsername);

                cmdAdd.ExecuteNonQuery();

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