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
    public partial class ViewServicerDetails : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            String strRequestNo = Session["RequestNoStatus"].ToString();
            String strProfilePicture = "";
            String strServicerId = "";
            String strServicerName = "";
            String strTotalCompleteService = "";
            
            //get servicer id
            try
            {
                con.Open();
                String strAdd = "select s.full_name, s.contact_no, s.servicer_id, s.profile_picture from servicer s, service_request r where r.servicer_id = s.servicer_id and r.request_no = @request_no";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@request_no", strRequestNo);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strProfilePicture = dataReader["profile_picture"].ToString();
                    strServicerId = dataReader["servicer_id"].ToString();
                    strServicerName = dataReader["full_name"].ToString();
                    Session["servicingPhoneNo"] = dataReader["contact_no"].ToString();
                }
                con.Close();
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
            Session["servicerId"] = strServicerId;
            //Get total complete services
            try
            {
                con.Open();
                String strAdd = "SELECT COUNT(REQUEST_NO) FROM SERVICE_REQUEST WHERE STATUS = 'D' AND SERVICER_ID = @servicer_id";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strTotalCompleteService = dataReader[0].ToString();
                }
                con.Close();
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
            //Display profile details
            lblServicerName.Text = strServicerName;
            lblTotalCompleteService.Text = strTotalCompleteService;
            lblPhoneNo.Text = Session["servicingPhoneNo"].ToString();


            if (!strProfilePicture.Equals(""))
            {
                imgProfile.ImageUrl = strProfilePicture;
            }
            else
            {
                imgProfile.ImageUrl = "/Image/generaluser.png";
            }

            //Get servicer average rating and total review receive
            int count = 0;
            double totalreview = 0.00;            
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT RATING_NUMBER FROM CLIENT_REVIEW WHERE SERVICER_ID = @servicer_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader;
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    totalreview =+ totalreview + Convert.ToDouble(dataReader["RATING_NUMBER"].ToString());
                    count++;


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
            }

            double avgReview = (totalreview / count);
            lblRating.Text = avgReview.ToString("0.00") + "("+ count.ToString() +")";
            ////display review of servicer
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT TOP 5 C.PROFILE_PICTURE, C.FULL_NAME, R.RATING_NUMBER, R.COMMENT, R.IS_ANONYMOUS FROM CLIENT C, CLIENT_REVIEW R WHERE R.CLIENT_ID = C.CLIENT_ID AND R.SERVICER_ID = @servicer_id AND r.comment != '';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();

                if (!dtrProd.HasRows)
                {
                    RepeaterPanel.Visible = false;
                }

                RepeaterRequest.DataSource = dtrProd;
                RepeaterRequest.DataBind();


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

        protected void btnContact_Click(object sender, EventArgs e)
        {
            String client_name = Session["strRetrieveName"].ToString();
            String whatsappLink = "https://wa.me/6" + Session["servicingPhoneNo"].ToString() + "?text=Hi!%20I%20am%20"+ client_name + ",Your%20Client%20from%20SpeedServ!";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "window.open('" + whatsappLink + "');", true);
        }

        protected void lbViewMoreDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ViewServicerReviews.aspx");
        }
    }

}