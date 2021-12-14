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


namespace ServiceProvidingSystem.Servicer
{
    public partial class PaymentMethodSelection : System.Web.UI.Page
    {

        String table = "Subscription_Request";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                Double usdRate = Import();

                hfPaymentAmount.Value = Math.Round((80.00 * usdRate), 2).ToString();

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

        //setup API Obkect for currency rate
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


        protected void lbBankTransfer_Click(object sender, EventArgs e)
        {


            Response.Redirect("~/Servicer/PayByBankTransfer.aspx");



        }

        // will be triggered after paypal payment successful maid.
        protected void lbPay_Click(object sender, EventArgs e)
        {

            Double dblPaymentAmt = 0.00;


            String strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            dblPaymentAmt = 80.00;
            String strStatus = "D";
            String strPaymentMethod = "PP";
            String strId = Session["strId"].ToString();
            String strReceiptImg = "N/A";

            //find new subscription id
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

            String strSubscriptionId = "SU" + newSubscriptionId.ToString("D" + 8);

            con.Open();

            try
            {

                //create client account
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

            DateTime validDate = DateTime.Now;

            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    if (dataReader["valid_date"] != DBNull.Value)
                    {
                        validDate = DateTime.ParseExact(dataReader["valid_date"].ToString(), "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                    }

                }


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


            //add 365 days to the valid date
            DateTime newDateTime = validDate.AddYears(1);


            con.Open();

            try
            {

                //update servicer validity date
                String strUpdate = "UPDATE Servicer SET valid_date = @valid_date WHERE servicer_id = @servicer_id";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@valid_date", newDateTime.ToString("dd/MM/yyyy HH:mm"));
                cmdUpdate.Parameters.AddWithValue("@servicer_id", strId);


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



            con.Open();

            try
            {

                //update subscription sequence no
                String strUpdate = "UPDATE Sequence SET subscription_sequence = @subscription_sequence WHERE sequence_id = 'default'";

                SqlCommand cmdAdd = new SqlCommand(strUpdate, con);

                cmdAdd.Parameters.AddWithValue("@subscription_sequence", newSubscriptionId + 1);

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





            //display successful message and redirect
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Subscription payment have successfully made by PayPal. Thank you!');window.location.href='MySubscription.aspx';", true);




        }



        protected void ibBack_Click(object sender, EventArgs e)
        {


            Response.Redirect("~/Servicer/MySubscription.aspx");



        }
    }
}