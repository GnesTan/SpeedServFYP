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
using PayPal.Api;

namespace ServiceProvidingSystem
{
    public partial class Login : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Request.Cookies["rmbEmail"] != null)
                {
                    txtEmail.Text = Request.Cookies["rmbEmail"].Value;
                }

                if (Request.Cookies["rmbPwd"] != null)
                {
                    txtPassword.Attributes.Add("value", Request.Cookies["rmbPwd"].Value);
                }

                if (Request.Cookies["rmbEmail"] != null && Request.Cookies["rmbPwd"] != null)
                {
                    cbRemember.Checked = true;
                }

            }

                
        }


        protected void hlServicer_Click(object sender, EventArgs e)
        {
            //go to Register page for Servicer
            Session["RegisterUser"] = "Servicer";

            Response.Redirect("~/Register.aspx");
        }

        protected void hlClient_Click(object sender, EventArgs e)
        {
            //go to Register page for Client
            Session["RegisterUser"] = "Client";

            Response.Redirect("Register.aspx");
        }

        protected void btnStaffLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/StaffLogin.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //get text from controls
            String strEmail = txtEmail.Text;
            String strPassword = txtPassword.Text;

            //declare all needed information
            String strId = "";
            String strRetrieveName = "";
            String strRetrievePassword = "";
            int recordFound = 0;
            String userType = "Servicer";


            //check from servicer database
            con.Open();
            try
            {
                //retrieve Servicer data
                String strSelect = "SELECT * FROM Servicer WHERE email_address = @email_address;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@email_address", strEmail);
                SqlDataReader dataReader;
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strId = dataReader["servicer_id"].ToString();
                    strRetrieveName = dataReader["full_name"].ToString();
                    strRetrievePassword = dataReader["password"].ToString();
                    recordFound++;
                    userType = "Servicer";
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

            //check from client database
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Client WHERE email_address = @email_address;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@email_address", strEmail);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strId = dataReader["client_id"].ToString();
                    strRetrieveName = dataReader["full_name"].ToString();
                    strRetrievePassword = dataReader["password"].ToString();
                    recordFound++;
                    userType = "Client";
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


            //check is the password matched
            if (recordFound > 0 && strPassword.Equals(strRetrievePassword))
            {
                //save email and password to cookies if checked the check box
                if (cbRemember.Checked == true)
                {
                    Response.Cookies["rmbEmail"].Value = txtEmail.Text;
                    Response.Cookies["rmbPwd"].Value = txtPassword.Text;
                    Response.Cookies["rmbEmail"].Expires = DateTime.Now.AddDays(15);
                    Response.Cookies["rmbPwd"].Expires = DateTime.Now.AddDays(15);
                }
                else
                {
                    Response.Cookies["rmbEmail"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["rmbPwd"].Expires = DateTime.Now.AddDays(-1);
                }

                //save id and name to session
                Session["strId"] = strId;
                Session["strRetrieveName"] = strRetrieveName;
                Session["userType"] = userType;
                //redirect user to homepage according the type of user they are
                if (userType.Equals("Servicer"))
                {

                    Response.Redirect("~/Servicer/RequestList.aspx");
                }
                else
                {

                    Response.Redirect("~/Client/ClientHomepage.aspx");
                }
            }
            else
            {
                //display error if record not found or password is invalid
                lblWrong.Text = "The email address or password is invalid.";
            }



        }



    }
}