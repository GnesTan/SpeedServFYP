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
    public partial class ViewRequest : System.Web.UI.Page
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

                String selectedRequest = "";

                if (Session["selectedRequest"] != null)
                {
                    selectedRequest = Session["selectedRequest"].ToString();
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

                con.Open();

                //retrieve data
                String strAdd = "SELECT S.address, S.remark, S.service_category, S.service_type, S.title, S.budget, C.full_name, C.profile_picture, S.status, S.amount_charges FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.request_no = @request_no;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@request_no", selectedRequest);
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
                    strStatus = dataReader["status"].ToString();
                    if (strStatus != "" && strStatus != null)
                    {
                        charStatus = char.Parse(strStatus);
                    }
                    if (dataReader["amount_charges"] != DBNull.Value)
                    {
                        strAmountCharges = string.Format("{0:N2}", dataReader["amount_charges"]);
                    }
                }

                con.Close();

                lblClientName.Text = strClientName;
                lblCategory1.Text = strCategory;
                lblCategory3.Text = strCategory;
                lblType.Text = strType;
                lblRemark.Text = strRemark;
                lblAddress.Text = strAddress;
                ProfilePic.ImageUrl = strProfilePicture;
                lblTitle.Text = strTitle;
                lblBudget.Text = strBudget;

                if(charStatus == 'D')
                {
                    lblPrice.Visible = true;
                    lblPriceAmount.Text = strAmountCharges;
                    lblPriceAmount.Visible = true;
                    ButtonPanel.Visible = false;
                    StatusPanel.Visible = true;
                }
                else if(charStatus == 'C')
                {
                    ButtonPanel.Visible = false;
                    StatusPanel.Visible = true;
                    lblServingStatus.Text = "Cancelled";
                }

            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/RequestList.aspx");
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }


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
                recordFound++;
            }

            con.Close();




            if (recordFound == 0)
            {

                char charStatus = 'S';
                String strServicerId = "";

                if (Session["strId"] != null)
                {
                    strServicerId = Session["strId"].ToString();
                }




                con.Open();

                try
                {

                    String selectedRequest = "";

                    if (Session["selectedRequest"] != null)
                    {
                        selectedRequest = Session["selectedRequest"].ToString();
                    }

                    //retrieve data
                    strAdd = "UPDATE " + table + " SET servicer_id = @servicer_id, status = @status WHERE request_no = @request_no;";

                    cmdAdd = new SqlCommand(strAdd, con);
                    cmdAdd.Parameters.AddWithValue("@servicer_id", strServicerId);
                    cmdAdd.Parameters.AddWithValue("@status", charStatus);
                    cmdAdd.Parameters.AddWithValue("@request_no", selectedRequest);

                    cmdAdd.ExecuteNonQuery();

                    con.Close();

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Successfully Accepted.');window.location.href='CurrentServing.aspx';", true);


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
                lblError.Visible = true;
            }




        }

    }
}