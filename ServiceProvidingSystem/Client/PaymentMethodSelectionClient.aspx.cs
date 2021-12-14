using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PayPal.Api;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace ServiceProvidingSystem.Client
{
    public partial class PaymentMethodSelectionClient : System.Web.UI.Page
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
                //to verify servicer login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Client"))
                {
                    Response.Redirect("~/Login.aspx");
                }

                Double usdRate = Import();

                Double paymentAmt = Convert.ToDouble(Session["finalTotalPrice"]);

                hfPaymentAmount.Value = Math.Round((paymentAmt * usdRate), 2).ToString();

                hfPaymentAmount.Value = hfPaymentAmount.Value;




            }
        }


        public static Double Import()
        {
            try
            {
                String URLString = "https://v6.exchangerate-api.com/v6/585cd75ddbd3f67972b3dcd2/latest/MYR";
                string Strjson = new WebClient().DownloadString(URLString);
                API_Obj test = JsonConvert.DeserializeObject<API_Obj>(Strjson);

                return test.conversion_rates.USD;

            }
            catch (Exception)
            {
                return 0.00;
            }

        }


        public class API_Obj
        {
            public string result { get; set; }
            public string documentation { get; set; }
            public string terms_of_use { get; set; }
            public string time_zone { get; set; }
            public string time_last_update { get; set; }
            public string time_next_update { get; set; }
            public ConversionRate conversion_rates { get; set; }
        }

        public class ConversionRate
        {

            public double MYR { get; set; }
            public double USD { get; set; }

        }
        protected void lblBankTransfer_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/PayServiceBankTransfer.aspx");
        }

        protected void lbPay_Click(object sender, EventArgs e)
        {
            //code after successful paid
            String currentRequestNo = "";

            if (Session["RequestNoStatus"] != null)
            {
                currentRequestNo = Session["RequestNoStatus"].ToString();
            }
            //update the request status to Done
            con.Open();


            try
            {
                //retrieve data
                String strUpdate = "UPDATE Service_Request SET status = 'D' WHERE request_no = @request_no;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@request_no", currentRequestNo);
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

            //Get Charge Amount AKA Subtotal
            Double paymentAmt = 0;
            con.Open();
            String strSel = "SELECT * FROM SERVICE_REQUEST WHERE request_no = @request_no";
            SqlCommand cmdSel = new SqlCommand(strSel, con);
            cmdSel.Parameters.AddWithValue("@request_no", currentRequestNo);
            SqlDataReader dataReader;
            dataReader = cmdSel.ExecuteReader();
            while (dataReader.Read())
            {
                paymentAmt = Convert.ToDouble(dataReader["amount_charges"].ToString());
            }
            con.Close();

            //update servicer wallet
            Double usdRate = Import();

            Double usdAmt = Math.Round((paymentAmt * usdRate), 2);

            Double paypalChargesUsd = Math.Round((usdAmt * 0.0349) + 0.49, 2);

            Double paypalChargesMyr = Math.Round((paypalChargesUsd / usdRate), 2);

            //find servicer id
            String strPayServicerId = "";
            if (Session["strPayServicerId"] != null)
            {
                strPayServicerId = Session["strPayServicerId"].ToString();
            }

            //get balance amount and email
            Double dblBalance = 0.00;
            int avilablePoints = 0;
            string strServicerEmail = "";
            string strServicerName = "";
            con.Open();

            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strPayServicerId);
                SqlDataReader dataReader1 = cmdSelect.ExecuteReader();
                while (dataReader1.Read())
                {
                    dblBalance = Convert.ToDouble(dataReader1["credit_balance"]);
                    avilablePoints = Convert.ToInt32(dataReader1["available_points"]);
                    strServicerEmail = dataReader1["email_address"].ToString();
                    strServicerName = dataReader1["full_name"].ToString();
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

            con.Open();
            try
            {
                //update credit balance
                String strUpdate = "UPDATE Servicer SET credit_balance = @credit_balance WHERE servicer_id = @servicer_id;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@credit_balance", dblBalance + paymentAmt);
                cmdUpdate.Parameters.AddWithValue("@servicer_id", strPayServicerId);
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

            int intPoint = Convert.ToInt32(paymentAmt);

            con.Open();
            try
            {
                //retrieve data
                String strAdd = "UPDATE Servicer SET available_points = @available_points WHERE servicer_id = @servicer_id;";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@available_points", intPoint + avilablePoints);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strPayServicerId);
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

            //get client id
            String strId = "";
            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();

            }

            //get service title
            String strServiceTitle = Session["strServiceTitle"].ToString();

            //create payment record
            //Get next number for payment
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
                cmdAdd.Parameters.AddWithValue("@payment_amount", Convert.ToDouble(Session["finalTotalPrice"]));
                cmdAdd.Parameters.AddWithValue("@servicer_amount", paymentAmt);
                cmdAdd.Parameters.AddWithValue("@remark", strServiceTitle);
                cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);
                cmdAdd.Parameters.AddWithValue("@client_id", strId);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strPayServicerId);
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

            // Modify Client Points
            double pointsMultipler = 1.5;
            double calculatedPoints = Convert.ToDouble(Session["finalTotalPrice"]) * pointsMultipler;
            int totalCurrentPoints = Convert.ToInt32(calculatedPoints) + Convert.ToInt32(Session["finalCurrentPoints"]);
            Session["earnedPoints"] = Convert.ToInt32(calculatedPoints);
            con.Open();
            try
            {
                //retrieve data
                String strAdd = "UPDATE Client SET reward_points = @rewardPoints WHERE client_id = @client_id;";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@rewardPoints", totalCurrentPoints);
                cmdAdd.Parameters.AddWithValue("@client_id", strId);
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

            int countTotalPayment = 0;
            char currentRank = ' ';
            //Update client rank
            con.Open();
            try
            {
                String strSel3 = "SELECT COUNT(PAYMENT_ID) FROM PAYMENT WHERE CLIENT_ID = @client_id";
                SqlCommand cmdSel3 = new SqlCommand(strSel3, con);
                cmdSel3.Parameters.AddWithValue("@client_id", strId);
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
                String strUpdate = "UPDATE CLIENT SET CLIENT_RANK = @currentRank WHERE client_id = @client_id;";
                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@client_id", strId);
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
            string strCustomerName = "";

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Client WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strId);
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strCustomerName = dataReader["full_name"].ToString();

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

            SendEmailServicer(strServicerEmail, strCustomerName, strServicerName, strServiceTitle, currentRequestNo, Convert.ToString(paymentAmt));
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('The transaction have successful paid by Paypal.');window.location.href='PaymentSuccess.aspx';", true);
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

        protected void ibBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/PayService.aspx");
        }
    }
}