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
    public partial class Favourite : System.Web.UI.Page
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
            try
            {
                //retrieve data
                String strSelect = "SELECT F.FAVOURITE_ID, F.OFFER_ID, S.SERVICE_TITLE, S.SERVICE_PICTURE, S.FEES FROM FAVOURITE F, SERVICE_OFFER S WHERE F.OFFER_ID = S.OFFER_ID AND F.CLIENT_ID = @strId";
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
                displayFavRepeater.DataSource = dtrFav;
                displayFavRepeater.DataBind();
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

        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            LinkButton linkbtn = (sender as LinkButton);
            RepeaterItem item = linkbtn.NamingContainer as RepeaterItem;
            HiddenField selectedServiceOffer = (HiddenField)item.FindControl("HiddenField1");
            Session["selectedServOffer"] = selectedServiceOffer.Value;
            Response.Redirect("~/Client/DisplayServiceDetails.aspx");
        }

        protected void removeFromFav_Click(object sender, EventArgs e)
        {
            String strId = Session["strId"].ToString();
            ImageButton imagebtn = (sender as ImageButton);
            RepeaterItem item = imagebtn.NamingContainer as RepeaterItem;
            HiddenField selectedServiceOffer = (HiddenField)item.FindControl("HiddenField1");
            Session["selectedServOffer"] = selectedServiceOffer.Value;
            string strOfferNo = Session["selectedServOffer"].ToString();

            con.Open();
            try
            {
                string query = "DELETE FROM FAVOURITE WHERE OFFER_ID = @offerId AND CLIENT_ID =  @strId";
                using (SqlConnection con = new SqlConnection(str))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@offerId", strOfferNo);
                        cmd.Parameters.AddWithValue("@strId", strId);
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
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
                this.BindGrid();
            }

        }

    }

}