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
    public partial class Register : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateValidator.MinimumValue = DateTime.Now.Date.AddYears(-100).ToString("yyyy/MM/dd");
            DateValidator.MaximumValue = DateTime.Now.Date.AddYears(-18).ToString("yyyy/MM/dd");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["RegisterUser"] != null)
                {
                    lblUser.Text = Session["RegisterUser"].ToString();
                }

                txtDob.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }


        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            String strError = "";



            //check duplicate phone number
            int phoneExist = 0;

            con.Open();
            try
            {
                //retrieve data
                SqlCommand check_servicer_phone = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (contact_no = @contact_no)", con);
                check_servicer_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
                phoneExist += (int)check_servicer_phone.ExecuteScalar();

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
                SqlCommand check_client_phone = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (contact_no = @contact_no)", con);
                check_client_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
                phoneExist += (int)check_client_phone.ExecuteScalar();

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

            //check duplicate ic no.
            int icExist = 0;

            con.Open();
            try
            {
                //retrieve data
                SqlCommand check_servicer_ic = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (identity_no = @identity_no)", con);
                check_servicer_ic.Parameters.AddWithValue("@identity_no", txtIC.Text);
                icExist += (int)check_servicer_ic.ExecuteScalar();

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
                SqlCommand check_client_ic = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (identity_no = @identity_no)", con);
                check_client_ic.Parameters.AddWithValue("@identity_no", txtIC.Text);
                icExist += (int)check_client_ic.ExecuteScalar();

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

            if (phoneExist > 0)
            {
                strError += "*Phone number have registered by another account. <br/>";
            }

            if (icExist > 0)
            {
                strError += "*Identity No. have registered by another account. <br/>";
            }

            //check date is valid
            DateTime dateValue = default(DateTime);
            if (!DateTime.TryParse(txtDob.Text, out dateValue))
            {
                strError += "*The date format for Date of Birth is invalid.";
            }

            if (strError.Length == 0)
            {
                string name = txtName.Text;
                string phoneNo = txtPhone.Text;
                string ic = txtIC.Text;
                DateTime dtDob = DateTime.ParseExact(txtDob.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string dob = dtDob.ToString("dd/MM/yyyy");
                Session["name"] = name;
                Session["phoneNo"] = phoneNo;
                Session["ic"] = ic;
                Session["dob"] = dob;

                Response.Redirect("Register2.aspx");


            }
            else
            {
                lblError.Text = strError;
            }

        }
    }
}