using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;


namespace ServiceProvidingSystem.BackendUser
{
    public partial class ClientPayment : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "BankTransfer_Request";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //to verify backend user login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Backend"))
                {
                    Response.Redirect("~/StaffLogin.aspx");
                }

                this.BindGrid();
            }



        }

        //bind subscription request to repeater
        private void BindGrid()
        {
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT b.transfer_no, b.date_and_time, r.amount_charges, b.client_id, b.receipt_image, b.request_no FROM BankTransfer_Request b, Service_Request r WHERE b.request_no = r.request_no AND b.status = 'P';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();

                if (!dtrProd.HasRows)
                {
                    RepeaterPanel.Visible = false;
                    NonePanel.Visible = true;
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


        //mark request as rejected
        protected void btnReject_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedTransferNo = (Label)item.FindControl("lblTransferNo");

            //get currect username
            String strUsername = "";

            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }

            con.Open();

            try
            {

                //update credit balance
                String strUpdate = "UPDATE " + table + " SET status = 'R', username = @username WHERE transfer_no = @transfer_no;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@transfer_no", selectedTransferNo.Text);

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

            //prepare for send email

            string strRequestNo = "";
            string strClientId = "";

            //Get request_no and client id
            con.Open();
            String strSel = "SELECT * FROM BANKTRANSFER_REQUEST WHERE transfer_no = @transfer_no";
            SqlCommand cmdSel = new SqlCommand(strSel, con);
            cmdSel.Parameters.AddWithValue("@transfer_no", selectedTransferNo.Text);
            SqlDataReader dataReader;
            dataReader = cmdSel.ExecuteReader();
            while (dataReader.Read())
            {
                strRequestNo = dataReader["request_no"].ToString();
                strClientId = dataReader["client_id"].ToString();
            }
            con.Close();


            String strEmail = "";
            String strName = "";
            String strTitle = "";
            String strAmount = "";


            //retrieve servicer information
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Client WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strClientId);
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strEmail = dataReader["email_address"].ToString();
                    strName = dataReader["full_name"].ToString();

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

            //retrieve request information
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Service_Request WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strClientId);
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strTitle = dataReader["title"].ToString();
                    strAmount = dataReader["amount_charges"].ToString();

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

            //send mail
            SendEmailFailed(strEmail, strName, strTitle, strRequestNo, strAmount);



            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Transfer Request has successfully rejected.');window.location.href='ClientPayment.aspx';", true);




        }

        //mark request as approved and extend validity
        protected void btnAppr_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);
            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;
            //Find the label control
            Label selectedTransferNo = (Label)item.FindControl("lblTransferNo");
            //get backend username
            String strUsername = Session["strUsername"].ToString();

            con.Open();

            try
            {

                //update status
                String strUpdate = "UPDATE " + table + " SET status = 'A', username = @username WHERE transfer_no = @transfer_no";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@transfer_no", selectedTransferNo.Text);

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

            string strRequestNo = "";
            string strServicerId = "";
            string strClientId = "";
            string strServiceTitle = "";
            double chargedAmt = 0;
            double finaltotalprice = 0;
            int finalcurrentpoints = 0;
            int avilablePoints = 0;
            
            Double dblBalance = 0.00;

            //Get request_no and client id
            con.Open();           
            String strSel = "SELECT * FROM BANKTRANSFER_REQUEST WHERE transfer_no = @transfer_no";
            SqlCommand cmdSel = new SqlCommand(strSel, con);            
            cmdSel.Parameters.AddWithValue("@transfer_no", selectedTransferNo.Text);
            SqlDataReader dataReader;
            dataReader = cmdSel.ExecuteReader();
            while (dataReader.Read())
            {
                strClientId = dataReader["client_id"].ToString();
                strRequestNo = dataReader["request_no"].ToString();
                finaltotalprice = Convert.ToDouble(dataReader["final_totalprice"].ToString());
                finalcurrentpoints = Convert.ToInt32(dataReader["final_currentpoints"].ToString());
            }
            con.Close();

            String strServicerEmail = "";
            String strServicerName = "";

            // Get servicer id
            con.Open();            
            String strSel1 = "SELECT * FROM SERVICER S, SERVICE_REQUEST R WHERE R.SERVICER_ID = S.SERVICER_ID AND REQUEST_NO = @request_no";
            SqlCommand cmdSel1 = new SqlCommand(strSel1, con);
            cmdSel1.Parameters.AddWithValue("@request_no", strRequestNo);
            SqlDataReader dataReader1;
            dataReader1 = cmdSel1.ExecuteReader();
            while (dataReader1.Read())
            {
                strServicerId = dataReader1["servicer_id"].ToString();
                strServicerEmail = dataReader1["email_address"].ToString();
                strServicerName = dataReader1["full_name"].ToString();
                chargedAmt = Convert.ToDouble(dataReader1["amount_charges"].ToString());
                dblBalance = Convert.ToDouble(dataReader1["credit_balance"].ToString());
                avilablePoints = Convert.ToInt32(dataReader1["available_points"]);
                strServiceTitle = dataReader1["title"].ToString();
            }
            con.Close();

            //Update servicer points
            con.Open();
            try
            {
                //update credit balance
                String strUpdate = "UPDATE Servicer SET credit_balance = @credit_balance WHERE servicer_id = @servicer_id;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@credit_balance", dblBalance + chargedAmt);
                cmdUpdate.Parameters.AddWithValue("@servicer_id", strServicerId);
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
            //update servicer available point
            int intPoint = Convert.ToInt32(chargedAmt);
            con.Open();
            try
            {
                //retrieve data
                String strAdd = "UPDATE Servicer SET available_points = @available_points WHERE servicer_id = @servicer_id;";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@available_points", intPoint + avilablePoints);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                cmdAdd.ExecuteNonQuery();
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
            //Create payment record
            int newPaymentId = 0;

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                SqlDataReader dataReader2 = cmdSelect.ExecuteReader();
                while (dataReader2.Read())
                {
                    newPaymentId = (int)dataReader2["payment_sequence"];
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

            String strPaymentId = "PY" + newPaymentId.ToString("D" + 8);
            char reviewStatus = 'N';
            con.Open();

            try
            {
                //create payment record
                String strAdd = "INSERT INTO Payment (payment_id, date_and_time, payment_amount, servicer_amount, remark, request_no, client_id, servicer_id, doneReview) VALUES (@payment_id, @date_and_time, @payment_amount, @servicer_amount, @remark, @request_no, @client_id, @servicer_id, @doneReview);";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@payment_id", strPaymentId);
                cmdAdd.Parameters.AddWithValue("@date_and_time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                cmdAdd.Parameters.AddWithValue("@payment_amount", Convert.ToDouble(finaltotalprice));
                cmdAdd.Parameters.AddWithValue("@servicer_amount", chargedAmt);
                cmdAdd.Parameters.AddWithValue("@remark", strServiceTitle);
                cmdAdd.Parameters.AddWithValue("@request_no", strRequestNo);
                cmdAdd.Parameters.AddWithValue("@client_id", strClientId);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                cmdAdd.Parameters.AddWithValue("@doneReview", reviewStatus);
                cmdAdd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    String exMessage = ex.Message;
                    Application["ErrorMessage"] = exMessage;
                }
                Application["ErrorCode"] = " ";
                Response.Redirect("~/ErrorPage.aspx");
            }
            finally
            {
                con.Close();
            }

            con.Open();

            try
            {
                //update payment sequence id
                String strUpdate = "UPDATE Sequence SET payment_sequence = @payment_sequence WHERE sequence_id = 'default'";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@payment_sequence", newPaymentId + 1);
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
            //Update client points
            double pointsMultipler = 1.5;
            double calculatedPoints = Convert.ToDouble(finaltotalprice) * pointsMultipler;
            int totalCurrentPoints = Convert.ToInt32(calculatedPoints) + Convert.ToInt32(finalcurrentpoints);
            Session["earnedPoints"] = Convert.ToInt32(calculatedPoints);
            con.Open();
            try
            {
                //retrieve data
                String strAdd = "UPDATE Client SET reward_points = @rewardPoints WHERE client_id = @client_id;";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@rewardPoints", totalCurrentPoints);
                cmdAdd.Parameters.AddWithValue("@client_id", strClientId);
                cmdAdd.ExecuteNonQuery();
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

            //update the request status to Done
            con.Open();
            try
            {
                //retrieve data
                String strUpdate = "UPDATE Service_Request SET status = 'D' WHERE request_no = @request_no;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@request_no", strRequestNo);
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

            //Update client rank            
            int countTotalPayment = 0;
            char currentRank = ' ';
            //Update client rank
            con.Open();
            try
            {
                String strSel3 = "SELECT COUNT(PAYMENT_ID) FROM PAYMENT WHERE CLIENT_ID = @client_id";
                SqlCommand cmdSel3 = new SqlCommand(strSel3, con);
                cmdSel3.Parameters.AddWithValue("@client_id", strClientId);
                SqlDataReader dtr3;
                dtr3 = cmdSel3.ExecuteReader();
                while (dtr3.Read())
                {
                    countTotalPayment = Convert.ToInt32(dtr3[0].ToString());
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

            if (countTotalPayment < 5)
            {
                currentRank = 'B';

            }
            if (countTotalPayment >= 5 && countTotalPayment < 10)
            {
                currentRank = 'S';

            }
            if (countTotalPayment >= 10)
            {
                currentRank = 'G';

            }

            con.Open();
            try
            {
                //retrieve data
                String strUpdate = "UPDATE CLIENT SET CLIENT_RANK = @currentRank WHERE client_id = @client_id;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@client_id", strClientId);
                cmdUpdate.Parameters.AddWithValue("@currentRank", currentRank);
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

            //prepare for send email
            String strEmail = "";
            String strName = "";
            String strTitle = "";
            String strAmount = "";


            //retrieve servicer information
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Client WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strClientId);
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strEmail = dataReader["email_address"].ToString();
                    strName = dataReader["full_name"].ToString();

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

            //retrieve request information
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Service_Request WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strClientId);
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strTitle = dataReader["title"].ToString();
                    strAmount = dataReader["amount_charges"].ToString();

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

            //send mail
            SendEmailSuccess(strEmail, strName, strTitle, strRequestNo, strAmount);

            SendEmailServicer(strServicerEmail, strName, strServicerName, strTitle, strRequestNo, strAmount);


            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Transfer Request has successfully approved.');window.location.href='ClientPayment.aspx';", true);


        }

        public void SendEmailSuccess(String strEmail, String strName, String strTitle, String strRequestNo, String strAmount)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Payment Successful for Service Request";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Your payment made for service request is successfully to made!<br /><br />" +
                           "Request no: " + strRequestNo + "<br />" +
                           "Service Title: " + strTitle + "<br />" +
                           "Payment amount(RM): " + strAmount + "<br />" +
                           "Status: Success <br />" +
                           "<br />" +
                           "Thank you for using SpeedServ!<br />" +
                           "<br /><br />Please do not reply to this email.";

            SmtpClient client = new SmtpClient("smtp.live.com", 587)
            {
                Credentials = new NetworkCredential("speedservofficial2021@outlook.com", "speedserv123"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendEmailFailed(String strEmail, String strName, String strTitle, String strRequestNo, String strAmount)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Payment Failed for Service Request";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Unfortunely, your payment made for service request is failed.<br /><br />" +
                           "Request no: " + strRequestNo + "<br />" +
                           "Service Title: " + strTitle + "<br />" +
                           "Payment amount(RM): " + strAmount + "<br />" +
                           "Status: Failed <br />" +
                           "<br />" +
                           "Thank you for using SpeedServ!<br />" +
                           "<br /><br />Please do not reply to this email.";

            SmtpClient client = new SmtpClient("smtp.live.com", 587)
            {
                Credentials = new NetworkCredential("speedservofficial2021@outlook.com", "speedserv123"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void SendEmailServicer(String strEmail, String strCusName, String strName, String strTitle, String strRequestNo, String strAmount)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Payment Received for Service Request";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Payment received from client for service request.<br /><br />" +
                           "------------------------------------------------<br />" +
                           "Request Details<br />" +
                           "------------------------------------------------<br /><br />" +
                           "Request no: " + strRequestNo + "<br />" +
                           "Client name: " + strCusName + "<br />" +
                           "Service Title: " + strTitle + "<br />" +
                           "Payment amount(RM): " + strAmount + "<br />" +
                           "<br />" +
                           "Thank you for using SpeedServ!<br />" +
                           "<br /><br />Please do not reply to this email.";

            SmtpClient client = new SmtpClient("smtp.live.com", 587)
            {
                Credentials = new NetworkCredential("speedservofficial2021@outlook.com", "speedserv123"),
                EnableSsl = true
            };
            // code in brackets above needed if authentication required 

            try
            {
                client.Send(message);
            }
            catch (SmtpException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


    }
}