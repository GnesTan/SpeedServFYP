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
    public partial class ViewServicerReviews : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            string strServicerId = Session["servicerId"].ToString();            
            ////display review of servicer
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT C.PROFILE_PICTURE, C.FULL_NAME, R.RATING_NUMBER, R.COMMENT, R.IS_ANONYMOUS, R.DATE_AND_TIME FROM CLIENT C, CLIENT_REVIEW R WHERE R.CLIENT_ID = C.CLIENT_ID AND R.SERVICER_ID = @servicer_id AND r.comment != '' ORDER BY DATE_AND_TIME DESC";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                if (dtrProd.HasRows)
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
 
                    reviewpanel.DataSource = dtrProd;
                    reviewpanel.DataBind();
                           
                



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
        }

        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string strServicerId = Session["servicerId"].ToString();
            string strSearch = tbSearch.Text;
            con.Open();
            
            String strSelect = "SELECT C.PROFILE_PICTURE, C.FULL_NAME, R.RATING_NUMBER, R.COMMENT, R.IS_ANONYMOUS, R.DATE_AND_TIME FROM CLIENT C, CLIENT_REVIEW R WHERE R.CLIENT_ID = C.CLIENT_ID AND R.SERVICER_ID = @servicer_id AND R.COMMENT LIKE '%' + @strSearch + '%' AND r.comment != '' ORDER BY DATE_AND_TIME DESC";
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);
            cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
            cmdSelect.Parameters.AddWithValue("@strSearch", strSearch);
            SqlDataReader dtrProd = cmdSelect.ExecuteReader();
            if (dtrProd.HasRows)
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
            reviewpanel.DataSource = dtrProd;
            reviewpanel.DataBind();
            con.Close();
        }
    }
}