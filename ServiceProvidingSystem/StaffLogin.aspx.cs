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

namespace ServiceProvidingSystem
{
    public partial class StaffLogin : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Cookies["rmbBEUsername"] != null)
                {
                    txtUsername.Text = Request.Cookies["rmbBEUsername"].Value;
                }

                if (Request.Cookies["rmbBEPwd"] != null)
                {
                    txtPassword.Attributes.Add("value", Request.Cookies["rmbBEPwd"].Value);
                }

                if (Request.Cookies["rmbBEUsername"] != null && Request.Cookies["rmbBEPwd"] != null)
                {
                    cbRemember.Checked = true;
                }

            }
        }


        protected void btnUserLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //get text from controls
            String strUsername = txtUsername.Text;
            String strPassword = txtPassword.Text;

            //declare all needed information
            String strRetrieveName = "";
            String strRetrievePassword = "";
            int recordFound = 0;


            //check from admin database
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Back_end_User WHERE USERNAME = @USERNAME;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@USERNAME", strUsername);
                SqlDataReader dataReader;
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strRetrievePassword = dataReader["PASSWORD"].ToString();
                    recordFound++;
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
                    Response.Cookies["rmbBEUsername"].Value = txtUsername.Text;
                    Response.Cookies["rmbBEPwd"].Value = txtPassword.Text;
                    Response.Cookies["rmbBEUsername"].Expires = DateTime.Now.AddDays(15);
                    Response.Cookies["rmbBEPwd"].Expires = DateTime.Now.AddDays(15);
                }
                else
                {
                    Response.Cookies["rmbBEUsername"].Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies["rmbBEPwd"].Expires = DateTime.Now.AddDays(-1);
                }

                //save id and name to session
                Session["strUsername"] = strUsername;
                Session["strRetrieveName"] = strRetrieveName;
                Session["userType"] = "Backend";

                //redirect user to homepage according the type of user they are
                Response.Redirect("~/BackendUser/BackEndHomepage.aspx");
            }
            else
            {
                //display error if record not found or password is invalid
                lblWrong.Text = "The username or password is invalid.";
            }



        }
    }
}