using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ServiceProvidingSystem.Client
{
    public partial class TransactionReportSetting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //to verify client login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Client"))
                {
                    Response.Redirect("~/Login.aspx");
                }

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                txtFrom.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txtTo.Text = DateTime.Now.ToString("yyyy-MM-dd");



            }


        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            DateTime dateValue = default(DateTime);
            if (DateTime.TryParse(txtFrom.Text, out dateValue) && DateTime.TryParse(txtTo.Text, out dateValue))
            {
                DateTime dtFromDate = DateTime.Now;
                DateTime dtToDate = DateTime.Now;

                dtFromDate = Convert.ToDateTime(txtFrom.Text);
                dtToDate = Convert.ToDateTime(txtTo.Text);

                Session["dtFromDate"] = dtFromDate;
                Session["dtToDate"] = dtToDate;

                ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('TransactionReport.aspx');", true);


            }
            else
            {
                lblError.Text = "*Invalid date entered, please try it again.";
            }



        }
    }
}