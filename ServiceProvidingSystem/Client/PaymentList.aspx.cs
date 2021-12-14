using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Client
{
    public partial class PaymentList : System.Web.UI.Page
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
            String strSelect = "SELECT * FROM SERVICE_REQUEST WHERE CLIENT_ID = @strId AND (STATUS = 'W' OR STATUS = 'V') ORDER BY DATE_AND_TIME DESC";
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
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
            displayPayListRep.DataSource = dtrFav;
            displayPayListRep.DataBind();
            con.Close();
        }

        protected void btnViewPayment_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = (sender as LinkButton);
            RepeaterItem item = linkbtn.NamingContainer as RepeaterItem;
            HiddenField selectedServiceRequest = (HiddenField)item.FindControl("HiddenField1");
            Session["RequestNoStatus"] = selectedServiceRequest.Value;
            Response.Redirect("~/Client/PayService.aspx");
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }
    }
}