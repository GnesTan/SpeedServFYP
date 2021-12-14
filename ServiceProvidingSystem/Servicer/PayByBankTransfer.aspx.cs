using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Servicer
{
    public partial class PayByBankTransfer : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Subscription_Request";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //to verify servicer login credential
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Servicer"))
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            //Get next number for IMG naming
            int newImgId = 0;

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
                    newImgId = (int)dataReader["img_sequence"];
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

            //create image file name
            String strImgName = "I" + newImgId.ToString("D" + 8);


            String strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            Double dblPaymentAmt = Convert.ToDouble(lblAmount.Text);
            String strStatus = "P";
            String strPaymentMethod = "BT";
            String strId = Session["strId"].ToString();
            String strReceiptImg = "N/A";

            //check whether user have uploaded file
            if (ImageUpload.HasFile)
            {
                string folderPath = Server.MapPath("~/Image/");

                if (ImageUpload.PostedFile.FileName.Length > 0)
                {
                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists Create it.
                        Directory.CreateDirectory(folderPath);
                    }
                    //Get extension
                    String extension = System.IO.Path.GetExtension(ImageUpload.PostedFile.FileName);

                    strImgName += extension;

                    //Save the File to the Directory (Folder).
                    ImageUpload.SaveAs(folderPath + strImgName);
                    strReceiptImg = "/Image/" + strImgName;

                }

                //find subscription sequence no
                int newSubscriptionId = 0;

                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    SqlDataReader dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        newSubscriptionId = (int)dataReader["subscription_sequence"];
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

                //create new subscription id
                String strSubscriptionId = "SU" + newSubscriptionId.ToString("D" + 8);

               
                con.Open();

                try
                {

                    //add new subscription record
                    String strAdd = "INSERT INTO " + table + " (subscription_no, date_and_time, payment_amount, status, payment_method, receipt_image, servicer_id) VALUES (@subscription_no, @date_and_time, @payment_amount, @status, @payment_method, @receipt_image, @servicer_id);";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                    cmdAdd.Parameters.AddWithValue("@subscription_no", strSubscriptionId);
                    cmdAdd.Parameters.AddWithValue("@date_and_time", strDateTime);
                    cmdAdd.Parameters.AddWithValue("@payment_amount", dblPaymentAmt);
                    cmdAdd.Parameters.AddWithValue("@status", strStatus);
                    cmdAdd.Parameters.AddWithValue("@payment_method", strPaymentMethod);
                    cmdAdd.Parameters.AddWithValue("@receipt_image", strReceiptImg);
                    cmdAdd.Parameters.AddWithValue("@servicer_id", strId);

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

                    //update subscription and img sequence no
                    String strUpdate = "UPDATE Sequence SET subscription_sequence = @subscription_sequence, img_sequence = @img_sequence WHERE sequence_id = 'default'";

                    SqlCommand cmdAdd = new SqlCommand(strUpdate, con);

                    cmdAdd.Parameters.AddWithValue("@subscription_sequence", newSubscriptionId + 1);
                    cmdAdd.Parameters.AddWithValue("@img_sequence", newImgId + 1);

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

                //display popup message and redirect
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Request have successfully sent, please wait for verification. (It may take 10 to 20 minutes on working days)');window.location.href='MySubscription.aspx';", true);

            


            }
            else
            {
                //display error message
                lblError.Text = "Please upload reference image to submit subscription request.";
            }




            


        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/PaymentMethodSelection.aspx");

        }

    }
}