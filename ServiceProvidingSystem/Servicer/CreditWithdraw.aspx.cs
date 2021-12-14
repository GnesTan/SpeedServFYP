using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Servicer
{
    public partial class CreditWithdraw : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Withdrawal_Request";

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

                if (Session["creditBalance"] != null)
                {
                    lblRetreAmt.Text = Session["creditBalance"].ToString();
                }

            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            Double dblBalance = Convert.ToDouble(lblRetreAmt.Text);

            Double dblWithdrawAmt = Convert.ToDouble(txtAmt.Text);

            //check whether the amount to withdraw is less or equal to balance
            if(dblWithdrawAmt <= dblBalance)
            {
                //get withdrawal sequence no
                int newWithdrawalNo = 0;

                con.Open();
                try
                {
                    //retrieve data
                    String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    SqlDataReader dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        newWithdrawalNo = (int)dataReader["withdrawal_sequence"];
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

                //set withdrawal no
                String strWithdrawalNo = "WT" + newWithdrawalNo.ToString("D" + 8);

                String strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                String strBankName = ddlBank.SelectedValue;
                String strHolderName = txtAccName.Text;
                String strAccNo = txtAccNo.Text;
                Double dblAmt = Convert.ToDouble(txtAmt.Text);
                String strStatus = "P";
                String strId = Session["strId"].ToString();

                con.Open();

                try
                {

                    //add new withdraw record
                    String strAdd = "INSERT INTO " + table + " (withdrawal_no, date_and_time, bank_name, holder_name, bank_account_no, withdrawal_amount, status, servicer_id) VALUES (@withdrawal_no, @date_and_time, @bank_name, @holder_name, @bank_account_no, @withdrawal_amount, @status, @servicer_id);";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                    cmdAdd.Parameters.AddWithValue("@withdrawal_no", strWithdrawalNo);
                    cmdAdd.Parameters.AddWithValue("@date_and_time", strDateTime);
                    cmdAdd.Parameters.AddWithValue("@bank_name", strBankName);
                    cmdAdd.Parameters.AddWithValue("@holder_name", strHolderName);
                    cmdAdd.Parameters.AddWithValue("@bank_account_no", strAccNo);
                    cmdAdd.Parameters.AddWithValue("@withdrawal_amount", dblAmt);
                    cmdAdd.Parameters.AddWithValue("@status", strStatus);
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

                    //update withdrawal sequence no
                    String strUpdate = "UPDATE Sequence SET withdrawal_sequence = @withdrawal_sequence WHERE sequence_id = 'default'";

                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                    cmdUpdate.Parameters.AddWithValue("@withdrawal_sequence", newWithdrawalNo + 1);

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

                //display popup message and redirect
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Withdrawal Request have successfully sent, please wait for verification. (It may take 10 to 20 minutes on working days)');window.location.href='ServicerWallet.aspx';", true);



            }
            else
            {
                //display error message
                lblError.Text = "*Withdraw amount cannot be more than the balance amount.";
            }



        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/ServicerWallet.aspx");

        }


    }
}