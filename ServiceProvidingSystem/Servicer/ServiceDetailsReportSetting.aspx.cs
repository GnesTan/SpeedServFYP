using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



namespace ServiceProvidingSystem.Servicer
{
    public partial class ServiceDetailsReportSetting : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //to verify servicer login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Servicer"))
                {
                    Response.Redirect("~/Login.aspx");
                }

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                //set textbox to current date
                txtFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");



            }


        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            DateTime dateValue = default(DateTime);
            if (DateTime.TryParse(txtFrom.Text, out dateValue) && DateTime.TryParse(txtTo.Text, out dateValue))
            {
                //get date from textbox and save in session
                DateTime dtFromDate = DateTime.Now;
                DateTime dtToDate = DateTime.Now;

                dtFromDate = DateTime.ParseExact(txtFrom.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                dtToDate = DateTime.ParseExact(txtTo.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);

                Session["dtFromDate"] = dtFromDate;
                Session["dtToDate"] = dtToDate;

                ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('ServiceDetailsReport.aspx');", true);


            }
            else
            {
                lblError.Text = "*Invalid date entered, please try it again.";
            }



        }
    }
}