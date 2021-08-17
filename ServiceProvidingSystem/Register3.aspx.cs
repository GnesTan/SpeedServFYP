using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;
using System.Net;
using System.Net.Mail;

namespace ServiceProvidingSystem
{
    public partial class Register3 : System.Web.UI.Page
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
            message.Subject = "Verification of Email Address";
            message.Body = "Hi " + Session["name"].ToString() + "<br /><br />" +
                           "Thank you for registering from SpeedServ!<br />" +
                           "Please enter the 6-digit PIN number to complete your registration.<br /><br />" +
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

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            String pinEntered = txtPinNumber.Text;

            

            if (pinEntered.Equals(randomNumber))
            {
                String strUser = "Servicer";

                if (Session["RegisterUser"] != null)
                {
                    strUser = Session["RegisterUser"].ToString();
                }

                if(strUser.Equals("Servicer"))
                {

                    int newServicerId = 0;

                    con.Open();

                    //retrieve data
                    String strAdd = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                    SqlDataReader dataReader;
                    dataReader = cmdAdd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        newServicerId = (int)dataReader["servicer_sequence"];
                    }

                    con.Close();

                    String strServicerId = "SR" + newServicerId.ToString("D" + 8);
                    String name = Session["name"].ToString();
                    String phoneNo = Session["phoneNo"].ToString();
                    String ic = Session["ic"].ToString();
                    String dob = Session["dob"].ToString();
                    String emailAddress = Session["emailAddress"].ToString();
                    String password = Session["password"].ToString();



                    con.Open();

                    try
                    {

                        //create servicer account
                        strAdd = "INSERT INTO Servicer (servicer_id, full_name, identity_no, date_of_birth, contact_no, email_address, password, available_points, collected_point, credit_balance, isActive, posted_service) VALUES (@servicer_id, @full_name, @identity_no, @date_of_birth, @contact_no, @email_address, @password, @available_points, @collected_point, @credit_balance, @isActive, @posted_service);";

                        cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                        cmdAdd.Parameters.AddWithValue("@full_name", name);
                        cmdAdd.Parameters.AddWithValue("@identity_no", phoneNo);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", ic);
                        cmdAdd.Parameters.AddWithValue("@contact_no", dob);
                        cmdAdd.Parameters.AddWithValue("@email_address", emailAddress);
                        cmdAdd.Parameters.AddWithValue("@password", password);
                        cmdAdd.Parameters.AddWithValue("@available_points", 0);
                        cmdAdd.Parameters.AddWithValue("@collected_point", 0);
                        cmdAdd.Parameters.AddWithValue("@credit_balance", 0.00);
                        cmdAdd.Parameters.AddWithValue("@isActive", "N");
                        cmdAdd.Parameters.AddWithValue("@posted_service", 0);

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

                        //update servicer sequence id
                        strAdd = "UPDATE Sequence SET servicer_sequence = @servicer_sequence WHERE sequence_id = 'default'";

                        cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@servicer_sequence", newServicerId + 1);

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

                }
                else
                {
                    int newClientId = 0;

                    con.Open();

                    //retrieve data
                    String strAdd = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                    SqlDataReader dataReader;
                    dataReader = cmdAdd.ExecuteReader();
                    while (dataReader.Read())
                    {
                        newClientId = (int)dataReader["client_sequence"];
                    }

                    con.Close();

                    String strClientId = "CL" + newClientId.ToString("D" + 8);
                    String name = Session["name"].ToString();
                    String phoneNo = Session["phoneNo"].ToString();
                    String ic = Session["ic"].ToString();
                    String dob = Session["dob"].ToString();
                    String emailAddress = Session["emailAddress"].ToString();
                    String password = Session["password"].ToString();



                    con.Open();

                    try
                    {

                        //create client account
                        strAdd = "INSERT INTO Client (client_id, full_name, identity_no, date_of_birth, contact_no, email_address, password, reward_points, credit_balance) VALUES (@client_id, @full_name, @identity_no, @date_of_birth, @contact_no, @email_address, @password, @reward_points, @credit_balance);";

                        cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@client_id", strClientId);
                        cmdAdd.Parameters.AddWithValue("@full_name", name);
                        cmdAdd.Parameters.AddWithValue("@identity_no", phoneNo);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", ic);
                        cmdAdd.Parameters.AddWithValue("@contact_no", dob);
                        cmdAdd.Parameters.AddWithValue("@email_address", emailAddress);
                        cmdAdd.Parameters.AddWithValue("@password", password);
                        cmdAdd.Parameters.AddWithValue("@reward_points", 0);
                        cmdAdd.Parameters.AddWithValue("@credit_balance", 0.00);

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

                        //update client sequence id
                        strAdd = "UPDATE Sequence SET client_sequence = @client_sequence WHERE sequence_id = 'default'";

                        cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@client_sequence", newClientId + 1);

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

                }




                Response.Redirect("RegisterSuccessful.aspx");

            }
            else
            {
                lblInvalid.Text = "Invalid pin number, please try again.";
            }

        }
    }
}