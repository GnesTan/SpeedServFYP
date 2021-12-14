using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class SalesTransactionReportSetting : System.Web.UI.Page
    {


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

                txtFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");



            }


        }

        //view report
        protected void btnView_Click(object sender, EventArgs e)
        {
            DateTime dateValue = default(DateTime);

            //check whether date is entered correctly
            if (DateTime.TryParse(txtFrom.Text, out dateValue) && DateTime.TryParse(txtTo.Text, out dateValue))
            {
                DateTime dtFromDate = DateTime.Now;
                DateTime dtToDate = DateTime.Now;

                dtFromDate = DateTime.ParseExact(txtFrom.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                dtToDate = DateTime.ParseExact(txtTo.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                //save date to session
                Session["dtFromDate"] = dtFromDate;
                Session["dtToDate"] = dtToDate;

                //redirect to report
                ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('SalesTransactionReport.aspx');", true);


            }
            else
            {
                //display error message
                lblError.Text = "*Invalid date entered, please try it again.";
            }



        }
    }
}