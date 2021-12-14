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
    public partial class DisplayServiceDetails : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            string strOfferNo = Session["selectedServOffer"].ToString();
            GetServicerDetails();
            //Get Service Offer details
            GetServiceOfferDetails();
            //Get review details
            GetReviewDetails();

        }
        private void GetServicerDetails()
        {
            
            string strOfferNo = Session["selectedServOffer"].ToString();
            String strProfilePicture = "";
            String strServicerId = "";
            String strServicerName = "";
            String strTotalCompleteService = "";

            //get servicer id
            con.Open();
            try
            {                
                String strAdd = "select s.full_name, s.contact_no, s.servicer_id, s.profile_picture from servicer s, service_offer o where o.servicer_id = s.servicer_id and o.offer_id = @offer_id";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@offer_id", strOfferNo);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strProfilePicture = dataReader["profile_picture"].ToString();
                    strServicerId = dataReader["servicer_id"].ToString();
                    strServicerName = dataReader["full_name"].ToString();
                    Session["servicingPhoneNo"] = dataReader["contact_no"].ToString();
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
            Session["servicerId"] = strServicerId;
            //Get total complete services
            con.Open();
            try
            {                
                String strAdd = "SELECT COUNT(REQUEST_NO) FROM SERVICE_REQUEST WHERE STATUS = 'D' AND SERVICER_ID = @servicer_id";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strTotalCompleteService = dataReader[0].ToString();
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
            //Display servicer profile details
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
            Session["servicerId"] = strServicerId;
        }
        private void GetServiceOfferDetails()
        {
            
            string strOfferNo = Session["selectedServOffer"].ToString();
            string strServicePicture = "";
            string strServiceTitle = "";
            string strServiceCat = "";
            string strServcieType = "";
            string strServiceState = "";
            string strServiceDistrict = "";
            decimal decServicePrice = 0;
            decimal decTransportFee = 0;
            string strRemark = "";

            con.Open();
            try
            {
                String strAdd = "SELECT * FROM SERVICE_OFFER WHERE OFFER_ID = @offerId";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@offerId", strOfferNo);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strServicePicture = dataReader["service_picture"].ToString();
                    strServiceTitle = dataReader["service_title"].ToString();
                    strServiceCat = dataReader["service_category"].ToString();
                    strServcieType = dataReader["service_type"].ToString();
                    strServiceState = dataReader["state"].ToString();
                    strServiceDistrict = dataReader["district"].ToString();
                    decServicePrice = Convert.ToDecimal(dataReader["fees"].ToString());
                    decTransportFee = Convert.ToDecimal(dataReader["delivery_fee"].ToString());
                    strRemark = dataReader["remark"].ToString();
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

            //Display Service Details            
            lblServiceName.Text = strServiceTitle;
            lblServiceTypeCat.Text = strServcieType + " - " + strServiceCat;
            lblPrice.Text = "RM " + decServicePrice.ToString("0.00");
            lblTransportFee.Text = "RM " + decTransportFee.ToString("0.00");
            lblRemark.Text = strRemark;
            //Check district
            if (strServiceDistrict.Equals(""))
            {
                lblLocation.Text = strServiceState;
            }
            else
            {
                lblLocation.Text = strServiceState + "," + strServiceDistrict;
            }
            //Check service picture
            if (!strServicePicture.Equals(""))
            {
                imgServicePicture.ImageUrl = strServicePicture;
            }
            else
            {
                imgServicePicture.ImageUrl = "Image/noImage.png";
            }

            //Check Favourite
            if (Session["strId"] != null)
            {
                string strId = Session["strId"].ToString();
                con.Open();
                try
                {
                    //retrieve data
                    String strSelect = "SELECT * FROM FAVOURITE WHERE OFFER_ID = @offerId AND CLIENT_ID = @client_id ;";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@client_id", strId);
                    cmdSelect.Parameters.AddWithValue("@offerId", strOfferNo);
                    SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                    if (dtrProd.HasRows)
                    {
                        btnAddFav.ImageUrl = "../Image/heart-solid-40.png";
                        btnAddFav.ToolTip = "Remove from favourite";
                    }
                    else
                    {
                        btnAddFav.ImageUrl = "../Image/heart-regular-40.png";
                        btnAddFav.ToolTip = "Add to favourite";
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
            }
        }

        private void GetReviewDetails()
        {
            string strServicerId = Session["servicerId"].ToString();
            String strTotalCompleteService = "";
            //Get Total Complete Service
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

                    totalreview = +totalreview + Convert.ToDouble(dataReader["RATING_NUMBER"].ToString());
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
            lblRating.Text = avgReview.ToString("0.00") + "(" + count.ToString() + ")";
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
            if (Session["strId"] != null)
            {
                String client_name = Session["strRetrieveName"].ToString();
                String whatsappLink = "https://wa.me/6" + Session["servicingPhoneNo"].ToString() + "?text=Hi!%20I%20am%20" + client_name + ",Your%20Client%20from%20SpeedServ!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "window.open('" + whatsappLink + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('To contact our servicer, you need to login to the website'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }                      
        }

        protected void btnRequestNow_Click(object sender, EventArgs e)
        {
            if (Session["strId"] != null)
            {
                string strOfferNo = Session["selectedServOffer"].ToString();
                int totalCurrentService = 0;              
                char currentRank = ' ';
                int maxService = 0;
                Session["requestServOfferId"] = strOfferNo;
                string strId = Session["strId"].ToString();                
                //Check whether offered services had requested by client
                con.Open();
                String strCheck = "SELECT * FROM SERVICE_REQUEST WHERE CLIENT_ID = @strId AND offer_id = @offerId and (STATUS = 'L' OR STATUS = 'S' OR STATUS = 'W' OR STATUS = 'V')";
                SqlCommand cmdCheck = new SqlCommand(strCheck, con);
                cmdCheck.Parameters.AddWithValue("@offerId", strOfferNo);
                cmdCheck.Parameters.AddWithValue("@strId", strId);
                SqlDataReader dtrCheck;
                dtrCheck = cmdCheck.ExecuteReader();
                if (dtrCheck.HasRows)
                {
                    Response.Write("<script>alert('You had requested the offered service')</script>");
                }
                else
                {
                    con.Close();
                    //Get client current rank
                    con.Open();
                    String strAdd = "SELECT CLIENT_RANK FROM CLIENT WHERE CLIENT_ID = @strId";
                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                    cmdAdd.Parameters.AddWithValue("@strId", strId);
                    SqlDataReader dataReader;
                    dataReader = cmdAdd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        currentRank = Convert.ToChar(dataReader["CLIENT_RANK"].ToString());
                    }
                    con.Close();

                    if (currentRank == 'B')
                    {
                        maxService = 2;
                    }
                    else if (currentRank == 'S')
                    {
                        maxService = 3;
                    }
                    else if (currentRank == 'G')
                    {
                        maxService = 5;
                    }

                    //Get current waiting, serving and pending payment services
                    con.Open();
                    String strAdd1 = "SELECT COUNT(REQUEST_NO) FROM [dbo].[Service_Request] WHERE CLIENT_ID = @strId AND (STATUS = 'L' OR STATUS = 'S' OR STATUS = 'W' OR STATUS = 'V')";
                    SqlCommand cmdAdd1 = new SqlCommand(strAdd1, con);
                    cmdAdd1.Parameters.AddWithValue("@strId", strId);
                    SqlDataReader dataReader1;
                    dataReader1 = cmdAdd1.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        totalCurrentService = Convert.ToInt32(dataReader1[0].ToString());
                    }
                    con.Close();

                    //Check client total current service requests and compare with current rank limit
                    if (totalCurrentService >= maxService)
                    {
                        Response.Write("<script>alert('Maximum Limit of Service Requests Reached')</script>");
                    }
                    else if (totalCurrentService < maxService)
                    {
                        Response.Redirect("~/Client/RequestOfferedService.aspx");
                    }
                }
            }
            else
            {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('To request service, you need to login to the website'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);                
            }
        }

        protected void addToFav_Click(object sender, EventArgs e)
        {
            if (Session["strId"] != null)
            {
                string strId = Session["strId"].ToString();
                string strOfferNo = Session["selectedServOffer"].ToString();
                con.Open();
                try
                {
                    //retrieve data
                    String strSelect = "SELECT * FROM FAVOURITE WHERE OFFER_ID = @offerId AND CLIENT_ID = @client_id ;";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@client_id", strId);
                    cmdSelect.Parameters.AddWithValue("@offerId", strOfferNo);
                    SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                    if (dtrProd.HasRows)
                    {
                        //Remove from favourite
                        con.Close();
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Service Remove to Favourite Successfully')", true);
                            con.Close();
                        }
                    }
                    else
                    {
                        //Add to favourite                        
                        try
                        {
                            int newFavouriteId = 0;
                            //retrieve favouriteId sequence
                            con.Close();
                            con.Open();
                            String strAdd = "SELECT * FROM Sequence WHERE sequence_id = 'default';";
                            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                            SqlDataReader dataReader;
                            dataReader = cmdAdd.ExecuteReader();
                            while (dataReader.Read())
                            {
                                newFavouriteId = (int)dataReader["favourite_sequence"];
                            }
                            con.Close();                            
                            String strFavouriteId = "FV" + newFavouriteId.ToString("D" + 8);

                            //Insert record to favourite
                            String strInsert = "INSERT INTO [FAVOURITE](FAVOURITE_ID, CLIENT_ID, OFFER_ID) VALUES (@favouriteId, @strId, @offerId)";
                            using (SqlConnection con = new SqlConnection(str))
                            {
                                SqlCommand cmd = new SqlCommand(strInsert, con);
                                SqlParameter paraFavouriteId = new SqlParameter()
                                {
                                    ParameterName = "@favouriteId",
                                    Value = strFavouriteId
                                };
                                cmd.Parameters.Add(paraFavouriteId);

                                SqlParameter parastrId = new SqlParameter()
                                {
                                    ParameterName = "@strId",
                                    Value = strId
                                };
                                cmd.Parameters.Add(parastrId);

                                SqlParameter paraOfferId = new SqlParameter()
                                {
                                    ParameterName = "@offerId",
                                    Value = strOfferNo
                                };
                                cmd.Parameters.Add(paraOfferId);
                                con.Open();
                                cmd.ExecuteNonQuery();
                                con.Close();
                            }
                            //Update Favourite ID sequence
                            con.Open();
                            String strUpdate = "UPDATE Sequence SET favourite_sequence = @favourite_sequence WHERE sequence_id = 'default'";
                            SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                            cmdUpdate.Parameters.AddWithValue("@favourite_sequence", newFavouriteId + 1);
                            cmdUpdate.ExecuteNonQuery();
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
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Service Added to Favourite Successfully')", true);                            
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
                    GetServiceOfferDetails();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('To add service into favourite, you need to login to the website'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }
        }

        protected void lbViewMoreDetails_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ViewServicerReviews.aspx");
        }
    }
}