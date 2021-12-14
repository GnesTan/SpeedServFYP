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
    public partial class ServicerWithdrawal : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Withdrawal_Request";

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

        //bind data to repeater
        private void BindGrid()
        {

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Withdrawal_Request WHERE status = 'P' AND servicer_id IS NOT NULL;";
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

        //mark record as paid
        protected void btnPaid_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedWithdrawal = (Label)item.FindControl("lblWithdrawalNo");

            //get backend username
            String strUsername = Session["strUsername"].ToString();


            con.Open();

            try
            {

                //update withdrawal details
                String strUpdate = "UPDATE " + table + " SET status = 'D', username = @username  WHERE withdrawal_no = @withdrawal_no";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@withdrawal_no", selectedWithdrawal.Text);

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

            //find servicer id
            String strServicerId = "";
            Double dblAmt = 0.00;
            Double dblBalance = 0.00;
            String strBankName = "";
            String strBankAcc = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE withdrawal_no = @withdrawal_no;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@withdrawal_no", selectedWithdrawal.Text);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strServicerId = dataReader["servicer_id"].ToString();
                    dblAmt = Convert.ToDouble(dataReader["withdrawal_amount"]);
                    strBankName = dataReader["bank_name"].ToString();
                    strBankAcc = dataReader["bank_account_no"].ToString();
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

            String strEmail = "";
            String strName = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    dblBalance = Convert.ToDouble(dataReader["credit_balance"]);
                    strEmail = dataReader["email_address"].ToString();
                    strName = dataReader["full_name"].ToString();
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

            con.Open();

            try
            {

                //update credit balance
                String strUpdate = "UPDATE Servicer SET credit_balance = @credit_balance WHERE servicer_id = @servicer_id;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@credit_balance", dblBalance - dblAmt);
                cmdUpdate.Parameters.AddWithValue("@servicer_id", strServicerId);

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

            con.Open();

            try
            {

                //create payment record
                String strAdd = "INSERT INTO Payment (payment_id, date_and_time, servicer_amount, remark, withdrawal_no, servicer_id) VALUES (@payment_id, @date_and_time, @servicer_amount, @remark, @withdrawal_no, @servicer_id);";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                cmdAdd.Parameters.AddWithValue("@payment_id", strPaymentId);
                cmdAdd.Parameters.AddWithValue("@date_and_time", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                cmdAdd.Parameters.AddWithValue("@servicer_amount", dblAmt * -1.00);
                cmdAdd.Parameters.AddWithValue("@remark", "Withdrawal");
                cmdAdd.Parameters.AddWithValue("@withdrawal_no", selectedWithdrawal.Text);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);

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

            if (strBankAcc.Length > 8)
            {
                strBankAcc = strBankAcc.Remove(0, 4);
                strBankAcc = "XXXX" + strBankAcc;
            }

            //send mail
            SendEmailSuccess(strEmail, strName, strBankName, strBankAcc, selectedWithdrawal.Text, dblAmt.ToString());



            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Withdrawal Request has successfully marked as paid.');window.location.href='ServicerWithdrawal.aspx';", true);


        }

        //mark record as rejected
        protected void btnReject_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedWithdrawal = (Label)item.FindControl("lblWithdrawalNo");

            //get current username
            String strUsername = "";

            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }


            con.Open();

            try
            {

                //update withdrawal to rejected
                String strUpdate = "UPDATE " + table + " SET status = 'R', username = @username WHERE withdrawal_no = @withdrawal_no;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@withdrawal_no", selectedWithdrawal.Text);

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

            //prepare mail
            //find servicer id
            String strServicerId = "";
            Double dblAmt = 0.00;
            String strBankName = "";
            String strBankAcc = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE withdrawal_no = @withdrawal_no;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@withdrawal_no", selectedWithdrawal.Text);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strServicerId = dataReader["servicer_id"].ToString();
                    dblAmt = Convert.ToDouble(dataReader["withdrawal_amount"]);
                    strBankName = dataReader["bank_name"].ToString();
                    strBankAcc = dataReader["bank_account_no"].ToString();
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

            String strEmail = "";
            String strName = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strEmail = dataReader["email_address"].ToString();
                    strName = dataReader["full_name"].ToString();
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

            //send mail
            SendEmailFailed(strEmail, strName, strBankName, strBankAcc, selectedWithdrawal.Text, dblAmt.ToString());


            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Withdrawal Request has successfully rejected.');window.location.href='ServicerWithdrawal.aspx';", true);

        }

        public void SendEmailSuccess(String strEmail, String strName, String strBankName, String strBankAcc, String strWithdrawalNo, String strAmt)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Withdrawal successful";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Your withdrawal request have approved and the payment have successfully made!<br /><br />" +
                           "Withdrawal no: " + strWithdrawalNo + "<br />" +
                           "Amount Withdraw(RM): " + strAmt + "<br />" +
                           "Bank Name: " + strBankName + "<br />" +
                           "Bank Account No.: " + strBankAcc + "<br />" +
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

        public void SendEmailFailed(String strEmail, String strName, String strBankName, String strBankAcc, String strWithdrawalNo, String strAmt)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Withdrawal failed";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Your withdrawal request have rejected and the payment have failed.<br /><br />" +
                           "Withdrawal no: " + strWithdrawalNo + "<br />" +
                           "Amount Withdraw(RM): " + strAmt + "<br />" +
                           "Bank Name: " + strBankName + "<br />" +
                           "Bank Account No.: " + strBankAcc + "<br />" +
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

    }
}