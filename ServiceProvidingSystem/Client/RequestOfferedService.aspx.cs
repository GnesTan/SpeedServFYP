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
    public partial class RequestOfferedService : System.Web.UI.Page
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
            //Display Service Details
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

            //Retrieve Client Details
            String strId = "";
            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            String strName = "";
            String strPhnNo = "";
            String strAddress = "";
            con.Open();
            try
            {            
                //Retreive data
                String strAdd1 = "SELECT * FROM CLIENT WHERE CLIENT_ID = @strId;";

                SqlCommand cmdAdd1 = new SqlCommand(strAdd1, con);
                cmdAdd1.Parameters.AddWithValue("@strId", strId);
                SqlDataReader dataReader1;
                dataReader1 = cmdAdd1.ExecuteReader();
                while (dataReader1.Read())
                {
                    strName = dataReader1["full_name"].ToString();
                    strPhnNo = dataReader1["contact_no"].ToString();
                    strAddress = dataReader1["home_address"].ToString();
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
            
            //Display Data
            txtName.Text = strName;
            txtPhnNo.Text = strPhnNo;
            txtHomeAddress.Text = strAddress;
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String strId = "";
            int totalCurrentService = 0;
            string strOfferNo = Session["selectedServOffer"].ToString();
            char currentRank = ' ';
            int maxService = 0;
            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }
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
                    //Get Service Details
                    if (Session["strId"] != null)
                    {
                        strId = Session["strId"].ToString();
                    }
                    string strFullName = txtName.Text;
                    string strPhnNo = txtPhnNo.Text;
                    string strHomeAddress = txtHomeAddress.Text;
                    string strServiceTitle = "";
                    string strServiceCat = "";
                    string strServcieType = "";
                    string strServiceState = "";
                    string strServiceDistrict = "";
                    string strServicerId = "";
                    if (txtBudget.Text == null)
                    {

                    }
                    string strBudget = "RM" + txtBudget.Text;
                    string strRemark = txtRemark.Text;
                    string strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                    string strStatus = "L";

                    con.Open();
                    try
                    {
                        string strAdd2 = "SELECT * FROM SERVICE_OFFER WHERE OFFER_ID = @offerId";
                        SqlCommand cmdAdd2 = new SqlCommand(strAdd2, con);
                        cmdAdd2.Parameters.AddWithValue("@offerId", strOfferNo);
                        SqlDataReader dataReader2;
                        dataReader2 = cmdAdd2.ExecuteReader();
                        while (dataReader2.Read())
                        {
                            strServiceTitle = dataReader2["service_title"].ToString();
                            strServiceCat = dataReader2["service_category"].ToString();
                            strServcieType = dataReader2["service_type"].ToString();
                            strServiceState = dataReader2["state"].ToString();
                            strServiceDistrict = dataReader2["district"].ToString();
                            strServicerId = dataReader2["servicer_id"].ToString();
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex != null)
                        {
                            string message = ex.Message;
                            Application["ErrorMessage"] = message;
                        }
                        Application["ErrorCode"] = " ";
                        Response.Redirect("~/ErrorPage.aspx");
                    }
                    finally
                    {
                        con.Close();
                    }

                    //Save Personal Info if checkbox ticked
                    if (cbSaveHomeAddress.Checked == true)
                    {
                        con.Open();
                        try
                        {
                            string strAdd3 = "UPDATE CLIENT SET FULL_NAME = @full_name, CONTACT_NO = @contact_no, HOME_ADDRESS = @home_address WHERE CLIENT_ID = @strId;";
                            SqlCommand cmdAdd3 = new SqlCommand(strAdd3, con);
                            cmdAdd3.Parameters.AddWithValue("@full_name", strFullName);
                            cmdAdd3.Parameters.AddWithValue("@contact_no", strPhnNo);
                            cmdAdd3.Parameters.AddWithValue("@home_address", strHomeAddress);
                            cmdAdd3.Parameters.AddWithValue("@strId", strId);
                            cmdAdd3.ExecuteNonQuery();
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

                    //retrieve request id sequence
                    int newRequestId = 0;
                    con.Open();
                    try
                    {
                        string strSel = "SELECT * FROM Sequence WHERE sequence_id = 'default';";
                        SqlCommand cmdSel = new SqlCommand(strSel, con);
                        SqlDataReader dataReader3;
                        dataReader3 = cmdSel.ExecuteReader();
                        while (dataReader3.Read())
                        {
                            newRequestId = (int)dataReader3["request_sequence"];
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
                    String strFavouriteId = "SR" + newRequestId.ToString("D" + 8);


                    //Start Send Request
                    string query = "INSERT INTO [SERVICE_REQUEST](REQUEST_NO, DATE_AND_TIME, SERVICE_CATEGORY, SERVICE_TYPE, ADDRESS, STATE, DISTRICT, BUDGET, REMARK,TITLE, STATUS, CLIENT_ID, SERVICER_ID,OFFER_ID) VALUES (@request_no, @date_and_time, @service_category, @service_type, @address, @state, @district, @budget, @remark, @title, @status, @strId, @servicerId, @offerId)";
                    using (SqlConnection con = new SqlConnection(str))
                    {
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Parameters.AddWithValue("@request_no", strFavouriteId);
                            cmd.Parameters.AddWithValue("@date_and_time", strDateTime);
                            cmd.Parameters.AddWithValue("@service_category", strServiceCat);
                            cmd.Parameters.AddWithValue("@service_type", strServcieType);
                            cmd.Parameters.AddWithValue("@address", strHomeAddress);
                            cmd.Parameters.AddWithValue("@state", strServiceState);
                            cmd.Parameters.AddWithValue("@district", strServiceDistrict);
                            cmd.Parameters.AddWithValue("@budget", strBudget);
                            cmd.Parameters.AddWithValue("@remark", strRemark);
                            cmd.Parameters.AddWithValue("@title", strServiceTitle);
                            cmd.Parameters.AddWithValue("@status", strStatus);
                            cmd.Parameters.AddWithValue("@strId", strId);
                            cmd.Parameters.AddWithValue("@servicerId", strServicerId);
                            cmd.Parameters.AddWithValue("@offerId", strOfferNo);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    //update service request sequence id
                    con.Open();
                    try
                    {
                        String strUpdate = "UPDATE Sequence SET request_sequence = @request_sequence WHERE sequence_id = 'default'";
                        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                        cmdUpdate.Parameters.AddWithValue("@request_sequence", newRequestId + 1);
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
                    }
                    // Display Success Message and Redirect to Homepage
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Service had been Requested successfully'); window.location='" + Request.ApplicationPath + "Client/ClientHomePage.aspx';", true);
                }                     
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/DisplayServiceDetails.aspx");
        }
    }
}