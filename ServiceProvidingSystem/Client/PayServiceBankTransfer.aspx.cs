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
    public partial class PayServiceBankTransfer : System.Web.UI.Page
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
            lblPayTotal.Text = "RM "+ Session["finalTotalPrice"].ToString();
        }


        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/PaymentMethodSelectionClient.aspx");
        }

        protected void btnProceedPay_Click(object sender, EventArgs e)
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
            String currentRequestNo = Session["RequestNoStatus"].ToString();
            Decimal paymentAmt = Convert.ToDecimal(Session["finalTotalPrice"].ToString());
            int finalpoints = Convert.ToInt32(Session["finalCurrentPoints"].ToString());
            String strStatus = "P";
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
                int newBankTransferId = 0;

                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    SqlDataReader dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        newBankTransferId = (int)dataReader["banktransfer_sequence"];
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
                String strBankTransferId = "BT" + newBankTransferId.ToString("D" + 8);


                con.Open();

                try
                {

                    //add new subscription record
                    String strAdd = "INSERT INTO BANKTRANSFER_REQUEST (transfer_no, date_and_time, status, receipt_image, client_id, request_no, final_totalprice, final_currentpoints) VALUES (@transfer_no, @date_and_time, @status, @receipt_image, @client_id, @request_no, @final_totalprice, @final_currentpoints);";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                    cmdAdd.Parameters.AddWithValue("@transfer_no", strBankTransferId);
                    cmdAdd.Parameters.AddWithValue("@date_and_time", strDateTime);                    
                    cmdAdd.Parameters.AddWithValue("@status", strStatus);                    
                    cmdAdd.Parameters.AddWithValue("@receipt_image", strReceiptImg);
                    cmdAdd.Parameters.AddWithValue("@client_id", strId);
                    cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);
                    cmdAdd.Parameters.AddWithValue("@final_totalprice", paymentAmt);
                    cmdAdd.Parameters.AddWithValue("@final_currentpoints", finalpoints);
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
                    String strUpdate = "UPDATE Sequence SET banktransfer_sequence = @banktransfer_sequence, img_sequence = @img_sequence WHERE sequence_id = 'default'";

                    SqlCommand cmdAdd = new SqlCommand(strUpdate, con);

                    cmdAdd.Parameters.AddWithValue("@banktransfer_sequence", newBankTransferId + 1);
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

                con.Open();

                try
                {
                    //update subscription and img sequence no
                    String strUpdate = "UPDATE SERVICE_REQUEST SET STATUS = 'V' WHERE REQUEST_NO = @request_no AND CLIENT_ID = @strId";                                        
                    SqlCommand cmdAdd = new SqlCommand(strUpdate, con);
                    cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);
                    cmdAdd.Parameters.AddWithValue("@strId", strId);
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
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Request have successfully sent, please wait for verification. (It may take 10 to 20 minutes on working days)');window.location.href='ClientHomePage.aspx';", true);




            }
            else
            {
                //display error message
                lblError.Text = "Please upload your bank receipt*";
            }
        }
    }
}