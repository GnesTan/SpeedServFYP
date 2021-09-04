using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace ServiceProvidingSystem
{
    public partial class PasswordRecovery2 : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        String randomNumber;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["emailAddress"] != null)
                {
                    lblEmail.Text = Session["emailAddress"].ToString();
                }
                else
                {
                    if (Session["randomNumber"] != null)
                    {
                        randomNumber = Session["randomNumber"].ToString();
                    }
                }

                SendEmail();

            }
            else
            {
                if (Session["randomNumber"] != null)
                {
                    randomNumber = Session["randomNumber"].ToString();
                }
            }
        }

        public void SendEmail()
        {

            Random r = new Random();

            randomNumber = r.Next(100000, 999999).ToString();

            Session["randomNumber"] = randomNumber;

            String emailAddress = "";

            if (Session["emailAddress"] != null)
            {
                emailAddress = Session["emailAddress"].ToString();
            }
            MailAddress to = new MailAddress(emailAddress);
            MailAddress from = new MailAddress("speedservofficial@gmail.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Verification for Password Recovery";
            message.Body = "Hi " + Session["fullName"].ToString() + "<br /><br />" +
                           "Please enter the 6-digit PIN number to complete your password recovery.<br /><br />" +
                           "This is your PIN number:<br />" +
                           "<b>" + randomNumber.ToString() + "</b>" +
                           "<br /><br />Please do not reply to this email.";

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("speedservofficial@gmail.com", "speedserv123"),
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

        protected void btnNext_Click(object sender, EventArgs e)
        {
            String pinEntered = txtPinNumber.Text;

            if (pinEntered.Equals(randomNumber))
            {
                Response.Redirect("~/PasswordRecovery3.aspx");
            }
            else
            {
                lblInvalid.Text = "Invalid pin number, please try again.";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void imgBack_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
}