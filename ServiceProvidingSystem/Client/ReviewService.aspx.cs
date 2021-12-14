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
    public partial class ReviewService : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            Control myControlMenu = Page.Master.FindControl("headerControl");
            if (myControlMenu != null)
            {
                myControlMenu.Visible = false;
            }
            if (!IsPostBack)
            {
                this.BindGrid();
            }

            btnrate1.ForeColor = System.Drawing.Color.DimGray;
            btnrate2.ForeColor = System.Drawing.Color.DimGray;
            btnrate3.ForeColor = System.Drawing.Color.DimGray;
            btnrate4.ForeColor = System.Drawing.Color.DimGray;
            btnrate5.ForeColor = System.Drawing.Color.DimGray;
        }

        private void BindGrid()
        {

            String strId = Session["strId"].ToString();
            String strRequestNo = Session["RequestNoStatus"].ToString();
            String strServicerName = "";

            con.Open();
            //Retreive data
            String strAdd = "SELECT r.servicer_id, s.full_name FROM Service_Request R, Servicer S WHERE R.servicer_id = S.servicer_id AND R.request_no = @requestNo";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@requestNo", strRequestNo);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strServicerName = dataReader["full_name"].ToString();
                Session["servicerId"] = dataReader["servicer_id"].ToString();
            }
            con.Close();
            // Display Data
            lblServicerName.Text = strServicerName;
        }

        protected void btnReview_OnClick(object sender, EventArgs e)
        {
            Session["rating"] = 1;
            btnrate1.ForeColor = System.Drawing.Color.Gold;
            btnrate2.ForeColor = System.Drawing.Color.DimGray;
            btnrate3.ForeColor = System.Drawing.Color.DimGray;
            btnrate4.ForeColor = System.Drawing.Color.DimGray;
            btnrate5.ForeColor = System.Drawing.Color.DimGray;
        }

        protected void btnReview2_OnClick(object sender, EventArgs e)
        {
            Session["rating"] = 2;
            btnrate1.ForeColor = System.Drawing.Color.Gold;
            btnrate2.ForeColor = System.Drawing.Color.Gold;
            btnrate3.ForeColor = System.Drawing.Color.DimGray;
            btnrate4.ForeColor = System.Drawing.Color.DimGray;
            btnrate5.ForeColor = System.Drawing.Color.DimGray;
        }

        protected void btnReview3_OnClick(object sender, EventArgs e)
        {
            Session["rating"] = 3;
            btnrate1.ForeColor = System.Drawing.Color.Gold;
            btnrate2.ForeColor = System.Drawing.Color.Gold;
            btnrate3.ForeColor = System.Drawing.Color.Gold;
            btnrate4.ForeColor = System.Drawing.Color.DimGray;
            btnrate5.ForeColor = System.Drawing.Color.DimGray;
        }

        protected void btnReview4_OnClick(object sender, EventArgs e)
        {
            Session["rating"] = 4;
            btnrate1.ForeColor = System.Drawing.Color.Gold;
            btnrate2.ForeColor = System.Drawing.Color.Gold;
            btnrate3.ForeColor = System.Drawing.Color.Gold;
            btnrate4.ForeColor = System.Drawing.Color.Gold;
            btnrate5.ForeColor = System.Drawing.Color.DimGray;
        }

        protected void btnReview5_OnClick(object sender, EventArgs e)
        {
            Session["rating"] = 5;
            btnrate1.ForeColor = System.Drawing.Color.Gold;
            btnrate2.ForeColor = System.Drawing.Color.Gold;
            btnrate3.ForeColor = System.Drawing.Color.Gold;
            btnrate4.ForeColor = System.Drawing.Color.Gold;
            btnrate5.ForeColor = System.Drawing.Color.Gold;
        }

        protected void btnDismiss_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ClientHomePage.aspx");
        }
        protected void btnSubmitReview_Click(object sender, EventArgs e)
        {
            String strRequestNo = Session["RequestNoStatus"].ToString();
            string strPaymentId = "";  
            if (Session["rating"] != null)
            {
                String strComment = txtComment.Text;
                if (cbAnonymous.Checked == true)
                {
                    Session["isAnonymous"] = "Y";
                }
                else
                {
                    Session["isAnonymous"] = "N";
                }
                //retrieve request id sequence
                int newReviewId = 0;
                con.Open();
                String strSel = "SELECT * FROM Sequence WHERE sequence_id = 'default';";
                SqlCommand cmdSel = new SqlCommand(strSel, con);
                SqlDataReader dataReader;
                dataReader = cmdSel.ExecuteReader();
                while (dataReader.Read())
                {
                    newReviewId = (int)dataReader["review_sequence"];
                }
                con.Close();
                String strFavouriteId = "RV" + newReviewId.ToString("D" + 8);
                String strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                int intRating = Convert.ToInt32(Session["rating"].ToString());
                char isAnony = Convert.ToChar(Session["isAnonymous"].ToString());
                //Start Send Request
                string query = "INSERT INTO [CLIENT_REVIEW](REVIEW_ID, DATE_AND_TIME, RATING_NUMBER, COMMENT, IS_ANONYMOUS, CLIENT_ID, SERVICER_ID ) VALUES (@review_id, @date_and_time, @rating_number, @comment, @isAnonymous, @clientId, @servicer_id)";
                using (SqlConnection con = new SqlConnection(str))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@review_id", strFavouriteId);
                        cmd.Parameters.AddWithValue("@date_and_time", strDateTime);
                        cmd.Parameters.AddWithValue("@rating_number", intRating);
                        cmd.Parameters.AddWithValue("@comment", strComment);
                        cmd.Parameters.AddWithValue("@isAnonymous", isAnony);
                        cmd.Parameters.AddWithValue("@clientId", Session["strId"].ToString());
                        cmd.Parameters.AddWithValue("@servicer_id", Session["servicerId"].ToString());
                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                //Update sequence
                con.Open();
                try
                {
                    //update payment sequence id
                    String strUpdate = "UPDATE Sequence SET review_sequence = @review_sequence WHERE sequence_id = 'default'";
                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                    cmdUpdate.Parameters.AddWithValue("@review_sequence", newReviewId + 1);
                    cmdUpdate.ExecuteNonQuery();
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
                //Get Payment ID
                con.Open();
                try
                {
                    //retrieve data
                    String strSelect = "SELECT * FROM Payment WHERE request_no = @requestNo;";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@requestNo", strRequestNo);
                    SqlDataReader dataReader1 = cmdSelect.ExecuteReader();
                    while (dataReader1.Read())
                    {
                        strPaymentId = dataReader1["payment_id"].ToString();
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
                //Update doneReview to Yes
                con.Open();
                try
                {                    
                    String strUpdate = "UPDATE Payment SET doneReview = 'Y' WHERE payment_id = @paymentId";
                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                    cmdUpdate.Parameters.AddWithValue("@paymentId", strPaymentId);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Thanks for using SpeedServ and have a nice day!'); window.location='" + Request.ApplicationPath + "Client/ClientHomePage.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "Alert1", "alert('Please give a rating for your servicer');", true);
            }

        }
    }
}