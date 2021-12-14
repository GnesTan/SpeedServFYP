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
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

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
            MailAddress from = new MailAddress("speedservofficial2021@outlook.com");

            MailMessage message = new MailMessage(from, to);

            message.IsBodyHtml = true;
            message.Subject = "Verification of Email Address";
            message.Body = "Hi " + Session["name"].ToString() + "<br /><br />" +
                           "Thank you for registering from SpeedServ!<br />" +
                           "Please enter the 6-digit PIN number to complete your registration.<br /><br />" +
                           "This is your PIN number:<br />" +
                           "<b>" + randomNumber.ToString() + "</b>" +
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

                if (strUser.Equals("Servicer"))
                {

                    int newServicerId = 0;

                    con.Open();
                    try
                    {
                        //retrieve data
                        String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        SqlDataReader dataReader;
                        dataReader = cmdSelect.ExecuteReader();
                        while (dataReader.Read())
                        {
                            newServicerId = (int)dataReader["servicer_sequence"];
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


                    String strServicerId = "SV" + newServicerId.ToString("D" + 8);
                    String name = Session["name"].ToString();
                    String phoneNo = Session["phoneNo"].ToString();
                    String ic = Session["ic"].ToString();
                    String dob = Session["dob"].ToString();
                    String emailAddress = Session["emailAddress"].ToString();
                    String password = Session["password"].ToString();
                    String profilePic = "/Image/generaluser.png";



                    con.Open();

                    try
                    {

                        //create servicer account
                        String strAdd = "INSERT INTO Servicer (servicer_id, full_name, identity_no, date_of_birth, contact_no, email_address, password, available_points, collected_point, credit_balance, isActive, posted_service, created_date, profile_picture) VALUES (@servicer_id, @full_name, @identity_no, @date_of_birth, @contact_no, @email_address, @password, @available_points, @collected_point, @credit_balance, @isActive, @posted_service, @created_date, @profile_picture);";

                        SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                        cmdAdd.Parameters.AddWithValue("@full_name", name);
                        cmdAdd.Parameters.AddWithValue("@identity_no", ic);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", dob);
                        cmdAdd.Parameters.AddWithValue("@contact_no", phoneNo);
                        cmdAdd.Parameters.AddWithValue("@email_address", emailAddress);
                        cmdAdd.Parameters.AddWithValue("@password", password);
                        cmdAdd.Parameters.AddWithValue("@available_points", 0);
                        cmdAdd.Parameters.AddWithValue("@collected_point", 0);
                        cmdAdd.Parameters.AddWithValue("@credit_balance", 0.00);
                        cmdAdd.Parameters.AddWithValue("@isActive", "A");
                        cmdAdd.Parameters.AddWithValue("@posted_service", 0);
                        cmdAdd.Parameters.AddWithValue("@created_date", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                        cmdAdd.Parameters.AddWithValue("@profile_picture", profilePic);

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
                        String strUpdate = "UPDATE Sequence SET servicer_sequence = @servicer_sequence WHERE sequence_id = 'default'";

                        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                        cmdUpdate.Parameters.AddWithValue("@servicer_sequence", newServicerId + 1);

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

                }
                else
                {
                    int newClientId = 0;

                    con.Open();
                    try
                    {
                        //retrieve data
                        String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        SqlDataReader dataReader;
                        dataReader = cmdSelect.ExecuteReader();
                        while (dataReader.Read())
                        {
                            newClientId = (int)dataReader["client_sequence"];
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

                    String strClientId = "CL" + newClientId.ToString("D" + 8);
                    String name = Session["name"].ToString();
                    String phoneNo = Session["phoneNo"].ToString();
                    String ic = Session["ic"].ToString();
                    String dob = Session["dob"].ToString();
                    String emailAddress = Session["emailAddress"].ToString();
                    String password = Session["password"].ToString();
                    String profilePic = "/Image/generaluser.png";



                    con.Open();

                    try
                    {

                        //create client account
                        String strAdd = "INSERT INTO Client (client_id, full_name, identity_no, date_of_birth, contact_no, email_address, password, reward_points, profile_picture, client_rank) VALUES (@client_id, @full_name, @identity_no, @date_of_birth, @contact_no, @email_address, @password, @reward_points, @profile_picture, @client_rank);";

                        SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                        cmdAdd.Parameters.AddWithValue("@client_id", strClientId);
                        cmdAdd.Parameters.AddWithValue("@full_name", name);
                        cmdAdd.Parameters.AddWithValue("@identity_no", ic);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", dob);
                        cmdAdd.Parameters.AddWithValue("@contact_no", phoneNo);
                        cmdAdd.Parameters.AddWithValue("@email_address", emailAddress);
                        cmdAdd.Parameters.AddWithValue("@password", password);
                        cmdAdd.Parameters.AddWithValue("@reward_points", 0);
                        cmdAdd.Parameters.AddWithValue("@profile_picture", profilePic);
                        cmdAdd.Parameters.AddWithValue("@client_rank", 'B');

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
                        String strUpdate = "UPDATE Sequence SET client_sequence = @client_sequence WHERE sequence_id = 'default'";

                        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                        cmdUpdate.Parameters.AddWithValue("@client_sequence", newClientId + 1);

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

                }




                Response.Redirect("~/RegisterSuccessful.aspx");

            }
            else
            {
                lblInvalid.Text = "Invalid pin number, please try again.";
            }

        }
    }
}