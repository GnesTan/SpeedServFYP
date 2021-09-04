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
    public partial class CurrentServing : System.Web.UI.Page
    {
        String table = "Service_Request";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);



        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Servicer"))
                {
                    Response.Redirect("~/Login.aspx");
                }

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                String strProfilePicture = "";
                String strClientName = "";
                String strCategory = "";
                String strType = "";
                String strTitle = "";
                String strRemark = "";
                String strBudget = "";
                String strAddress = "";
                char charStatus = ' ';
                String strStatus = "";
                String strAmountCharges = "";

                int recordFound = 0;

                con.Open();

                //retrieve data
                String strAdd = "SELECT S.request_no, S.address, S.remark, S.service_category, S.service_type, S.title, S.budget, C.full_name, C.profile_picture, S.status, S.amount_charges FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.servicer_id = @servicer_id AND status = 'S' OR status = 'W' OR status = 'Z';";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    strProfilePicture = dataReader["profile_picture"].ToString();
                    strClientName = dataReader["full_name"].ToString();
                    strCategory = dataReader["service_category"].ToString();
                    strType = dataReader["service_type"].ToString();
                    strRemark = dataReader["remark"].ToString();
                    strAddress = dataReader["address"].ToString();
                    strTitle = dataReader["title"].ToString();
                    strBudget = dataReader["budget"].ToString();
                    Session["currentRequestNo"] = dataReader["request_no"].ToString();
                    strStatus = dataReader["status"].ToString();
                    if (strStatus != "" && strStatus != null)
                    {
                        charStatus = char.Parse(strStatus);
                    }
                    if (dataReader["amount_charges"] != DBNull.Value)
                    {
                        strAmountCharges = string.Format("{0:N2}", dataReader["amount_charges"]);
                    }
                    recordFound++;
                }

                con.Close();


                if (recordFound == 0)
                {
                    UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
                    NonePanel.Visible = true;
                    UpdatePanel1.Update();
                }
                else
                {
                    UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
                    ExistPanel.Visible = true;
                    UpdatePanel1.Update();

                    lblClientName.Text = strClientName;
                    lblCategory1.Text = strCategory;
                    lblCategory3.Text = strCategory;
                    lblType.Text = strType;
                    lblRemark.Text = strRemark;
                    lblAddress.Text = strAddress;
                    ProfilePic.ImageUrl = strProfilePicture;
                    lblTitle.Text = strTitle;
                    lblBudget.Text = strBudget;
                    if(!strAmountCharges.Equals(""))
                    {
                        lblPriceAmount.Text = strAmountCharges;
                        lblPrice.Visible = true;
                        lblPriceAmount.Visible = true;
                    }
                    

                    if (charStatus == 'W')
                    {
                        lblServingStatus.Text = "Waiting for Payment";
                        btnComplete.Visible = false;
                    }
                    else if(charStatus == 'Z')
                    {
                        lblServingStatus.Text = "Client have selected Cash payment.";
                        btnComplete.Visible = false;
                        ReceivedPanel.Visible = true;
                    }

                }



            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/RequestList.aspx");
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {



        }

        protected void btnComplete_Click(object sender, EventArgs e)
        {
            UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
            WholeFormPanel.Visible = false;
            InputAmountPanel.Visible = true;
            UpdatePanel1.Update();
        }

        protected void btnReceived_Click(object sender, EventArgs e)
        {
            String currentRequestNo = "";

            if (Session["currentRequestNo"] != null)
            {
                currentRequestNo = Session["currentRequestNo"].ToString();
            }




            con.Open();

            try
            {



                //retrieve data
                String strAdd = "UPDATE " + table + " SET status = 'D' WHERE request_no = @request_no;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);

                cmdAdd.ExecuteNonQuery();

                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Successful Recorded, the transaction is completed.');location.reload();", true);



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





        protected void btnConfirm_Click(object sender, EventArgs e)
        {

            double dblAmount = 0.00;

            dblAmount = Convert.ToDouble(txtAmount.Text);



            String currentRequestNo = "";

            if(Session["currentRequestNo"] != null)
            {
                currentRequestNo = Session["currentRequestNo"].ToString();
            }

            


            con.Open();

            try
            {



                //retrieve data
                String strAdd = "UPDATE " + table + " SET status = 'W', amount_charges = @amount_charges WHERE request_no = @request_no;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@amount_charges", dblAmount);
                cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);

                cmdAdd.ExecuteNonQuery();

                con.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Successful recorded, please wait for client to make payment.');window.location.href='CurrentServing.aspx';", true);


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

        protected void btnCancelInput_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/CurrentServing.aspx");
        }

    }
}