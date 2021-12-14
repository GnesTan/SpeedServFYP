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
    public partial class SubscriptionRequest : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Subscription_Request";

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
                String strSelect = "SELECT subscription_no, date_and_time, payment_amount, payment_method, servicer_id, receipt_image FROM Subscription_Request WHERE status = 'P';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
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
            Label selectedSubscriptionNo = (Label)item.FindControl("lblSubscriptionNo");

            //get current username
            String strUsername = "";

            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }

            con.Open();

            try
            {

                //update credit balance
                String strUpdate = "UPDATE " + table + " SET status = 'R', username = @username WHERE subscription_no = @subscription_no;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@subscription_no", selectedSubscriptionNo.Text);

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


            //find servicer id
            String strServicerId = "";
            String strEmail = "";
            String strName = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE subscription_no = @subscription_no;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@subscription_no", selectedSubscriptionNo.Text);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strServicerId = dataReader["servicer_id"].ToString();
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
            SendEmailFailed(strEmail, strName, selectedSubscriptionNo.Text);


            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Subscription Request has successfully rejected.');window.location.href='SubscriptionRequest.aspx';", true);




        }

        //mark request as approved and extend validity
        protected void btnAppr_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedSubscriptionNo = (Label)item.FindControl("lblSubscriptionNo");

            //get backend username
            String strUsername = Session["strUsername"].ToString();

            con.Open();

            try
            {

                //update status
                String strUpdate = "UPDATE " + table + " SET status = 'D', username = @username WHERE subscription_no = @subscription_no";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@username", strUsername);
                cmdUpdate.Parameters.AddWithValue("@subscription_no", selectedSubscriptionNo.Text);

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
            DateTime validDate = DateTime.Now;
            String strEmail = "";
            String strName = "";

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE subscription_no = @subscription_no;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@subscription_no", selectedSubscriptionNo.Text);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    strServicerId = dataReader["servicer_id"].ToString();
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

                //retrieve data
                String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strServicerId);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader["valid_date"] != DBNull.Value)
                    {
                        validDate = DateTime.ParseExact(dataReader["valid_date"].ToString(), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    }
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

            //add 365 days to the valid date
            DateTime newDateTime = validDate.AddYears(1);

            con.Open();

            try
            {

                //update servicer validity date
                String strUpdate = "UPDATE Servicer SET valid_date = @valid_date WHERE servicer_id = @servicer_id";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@valid_date", newDateTime.ToString("dd/MM/yyyy HH:mm"));
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




            //send mail
            SendEmailSuccess(strEmail, strName, newDateTime.ToString("dd/MM/yyyy"), selectedSubscriptionNo.Text);


            //display popup message and refresh
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Subscription Request has successfully approved.');window.location.href='SubscriptionRequest.aspx';", true);


        }


        public void SendEmailSuccess(String strEmail, String strName, String strValidityDate, String strSubscriptionNo)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Payment Successful for Subscription";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Your payment made for subscription is successfully made!<br /><br />" +
                           "Subscription no: " + strSubscriptionNo + "<br />" +
                           "Payment amount(RM): 80.00<br />" +
                           "New validity date: " + strValidityDate + "<br />" +
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

        public void SendEmailFailed(String strEmail, String strName, String strSubscriptionNo)
        {


            MailAddress to = new MailAddress(strEmail);
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Payment Failed for Subscription";
            message.Body = "Hi " + strName + "<br /><br />" +
                           "Your payment made for subscription is failed.<br /><br />" +
                           "Subscription no: " + strSubscriptionNo + "<br />" +
                           "Payment amount(RM): 80.00<br />" +
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