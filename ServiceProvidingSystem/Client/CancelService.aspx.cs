using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Client
{
    public partial class CancelService : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReason.SelectedValue.Equals("others"))
            {
                txtOtherReason.Visible = true;
            }
            else
            {
                txtOtherReason.Visible = false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ViewRequestStatus.aspx");
        }

        protected void btnCancelService_Click(object sender, EventArgs e)
        {


            if (ddlReason.SelectedValue.Equals("others"))
            {
                Session["CancelReason"] = txtOtherReason.Text;
            }
            else
            {
                Session["CancelReason"] = ddlReason.SelectedItem.Text;                
            }
            
            String strCancelReason = Session["CancelReason"].ToString();
            String strId = Session["strId"].ToString();
            String strRequestNo = Session["RequestNoStatus"].ToString();


            //retrieve data
            con.Open();
            try
            {
            String strAdd = "UPDATE service_request SET status = 'C', cancel_reason = @cancelReason WHERE CLIENT_ID = @strId AND request_no = @requestNo;";
            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@cancelReason", strCancelReason);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            cmdAdd.Parameters.AddWithValue("@requestNo", strRequestNo);            
            cmdAdd.ExecuteNonQuery();
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
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Service Cancel Successfully.');window.location.href='ClientHomePage.aspx';", true);
        }
    }
}