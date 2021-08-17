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

        String table = "Servicer";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateValidator.MinimumValue = DateTime.Now.Date.AddYears(-100).ToString("yyyy/MM/dd");
            DateValidator.MaximumValue = DateTime.Now.Date.AddYears(-18).ToString("yyyy/MM/dd");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterUser"] != null)
            {
                lblUser.Text = Session["RegisterUser"].ToString();
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string phoneNo = txtPhone.Text;
            string ic = txtIC.Text;
            string dob = txtDob.Text;
            Session["name"] = name;
            Session["phoneNo"] = phoneNo;
            Session["ic"] = ic;
            Session["dob"] = dob;

            Response.Redirect("Register2.aspx");
        }
    }
}