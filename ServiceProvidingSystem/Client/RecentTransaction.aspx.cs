using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ServiceProvidingSystem.Client
{
    public partial class RecentTransaction : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            String strId = Session["strId"].ToString();
            con.Open();
            //retrieve data
            String strCheck = "SELECT P.DATE_AND_TIME, R.TITLE, P.PAYMENT_ID, P.PAYMENT_AMOUNT, P.DONEREVIEW,P.REQUEST_NO FROM SERVICE_REQUEST R, PAYMENT P WHERE P.CLIENT_ID = R.CLIENT_ID AND P.REQUEST_NO = R.REQUEST_NO AND P.CLIENT_ID = @strId ORDER BY P.DATE_AND_TIME DESC";
            SqlCommand cmdSelect = new SqlCommand(strCheck, con);
            cmdSelect.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dtrFav = cmdSelect.ExecuteReader();                       
           
            if (dtrFav.HasRows)
            {
                noItemLogo.Visible = false;
                noItemText.Visible = false;
                noItemLinkText.Visible = false;
            }
            else
            {
                noItemLogo.Visible = true;
                noItemText.Visible = true;
                noItemLinkText.Visible = true;
            }
            con.Close();
            
            
            String strSelect = "SELECT P.DATE_AND_TIME, R.TITLE, P.PAYMENT_ID, P.PAYMENT_AMOUNT, P.DONEREVIEW,P.REQUEST_NO FROM SERVICE_REQUEST R, PAYMENT P WHERE P.CLIENT_ID = R.CLIENT_ID AND P.REQUEST_NO = R.REQUEST_NO AND P.CLIENT_ID = '" + strId + "' ORDER BY P.DATE_AND_TIME DESC";           
            SqlConnection cn = new SqlConnection(str);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            cmd.CommandText = strSelect;
          
            DataTable dt = new DataTable();
            ad.SelectCommand = cmd;
            ad.Fill(dt);
            PagedDataSource pgitems = new PagedDataSource();
            pgitems.DataSource = dt.DefaultView;
            pgitems.AllowPaging = true;
            
            pgitems.PageSize = 5;
            pgitems.CurrentPageIndex = PageNumber;
            if (pgitems.PageCount > 1)
            {
                rptPaging.Visible = true;
                ArrayList pages = new ArrayList();
                for (int i = 0; i <= pgitems.PageCount - 1; i++)
                {
                    if (pgitems.CurrentPageIndex == i)
                    {
                        pages.Add("<u>" + (i + 1).ToString() + "</u>");
                    }
                    else
                    {
                        pages.Add((i + 1).ToString());
                    }

                }
                rptPaging.DataSource = pages;
                rptPaging.DataBind();
            }
            else
            {
                rptPaging.Visible = false;
            }
            
            RecentTransactPanel.DataSource = pgitems;
            RecentTransactPanel.DataBind();
        }


        protected void btnReview_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = (sender as LinkButton);
            RepeaterItem item = linkbtn.NamingContainer as RepeaterItem;
            HiddenField selectedServiceRequest = (HiddenField)item.FindControl("HiddenField1");
            Session["RequestNoStatus"] = selectedServiceRequest.Value;
            Response.Redirect("~/Client/ReviewService.aspx");
        }
        protected void btnViewPayment_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = (sender as LinkButton);
            RepeaterItem item = linkbtn.NamingContainer as RepeaterItem;
            HiddenField selectedServiceRequest = (HiddenField)item.FindControl("HiddenField1");
            HiddenField strPaymentId = (HiddenField)item.FindControl("HiddenField2");
            Session["RequestNoStatus"] = selectedServiceRequest.Value;
            Session["strPaymentId"] = strPaymentId.Value;
            Response.Redirect("~/Client/ViewTransactionDetails.aspx");
        }

        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            try
            {
                PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
                BindGrid();
            }
            catch (Exception ex)
            {

            }
        }
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }
    }
}