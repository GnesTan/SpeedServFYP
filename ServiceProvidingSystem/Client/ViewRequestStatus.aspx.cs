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
    public partial class ViewRequestStatus : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindGrid();
                
            }
        }
           
        private void BindGrid()
        {           
            String strId = Session["strId"].ToString();
            String strRequestNo = Session["RequestNoStatus"].ToString();
            String strServiceTitle = "";
            String strReqDateTime = "";
            String strAccDateTime = "";
            String strEstTime = "";
            String strDisTime = "";
            String strTimeLabel = "";
            String strServiceCat = "";
            String strServiceType = "";
            String strHomeAddress = "";
            String strState = "";
            String strDistrict = "";
            String strRemark = "";
            String strStatus = "";
            con.Open();
            //Retreive data
            String strAdd = "SELECT * FROM Service_Request WHERE client_id = @strId AND (status = 'L' OR status = 'S') AND request_no = @requestNo;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            cmdAdd.Parameters.AddWithValue("@requestNo", strRequestNo);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {               
                strServiceTitle = dataReader["title"].ToString();
                strReqDateTime = dataReader["date_and_time"].ToString();
                strAccDateTime = dataReader["accept_time"].ToString();
                strEstTime = dataReader["estimate_time"].ToString();
                strServiceCat = dataReader["service_category"].ToString();
                strServiceType = dataReader["service_type"].ToString();
                strHomeAddress = dataReader["address"].ToString();
                strState = dataReader["state"].ToString();
                strDistrict = dataReader["district"].ToString();
                strRemark = dataReader["remark"].ToString();
                strStatus = dataReader["status"].ToString();
                    if (strStatus == "L")
                    {
                        strStatus = "Pending";
                        strTimeLabel = "Request Date & Time";
                        strDisTime = strReqDateTime;
                        txtEstTime.Visible = false;
                    }
                    else if (strStatus == "S")
                    {
                        strStatus = "Serving";
                        servbar.Attributes.Add("class", "active");
                        btnViewServicer.Visible = true;
                    strTimeLabel = "Accept Date & Time";
                    strDisTime = strAccDateTime;
                    }
                    // Display Data            
                    lblServiceTitle.Text = strServiceTitle;
                    lblDateTime.Text = strDisTime;
                    lblDateText.Text = strTimeLabel;
                    lblServiceCat.Text = strServiceCat;
                    lblServiceType.Text = strServiceType;
                    lblHomeAddress.Text = strHomeAddress;
                    lblState.Text = strState;
                    lblDistrict.Text = strDistrict;
                    lblRemark.Text = strRemark;
                    lblEstTime.Text = strEstTime;
            }
            con.Close();            
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {           
                BindGrid();            
        }

        protected void btnViewServicer_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewServicerDetails.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            String strRequestNo = Session["RequestNoStatus"].ToString();
            String strId = Session["strId"].ToString();
            con.Open();
            //Retreive data
            String strAdd = "SELECT * FROM Service_Request WHERE request_no = @requestNo AND client_id = @strId ;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@requestNo", strRequestNo);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                Session["requestStatus"] = dataReader["status"].ToString();
                

                if (Session["requestStatus"].ToString() == "L")
                {
                    Response.Redirect("CancelService.aspx");

                }
                else if (Session["requestStatus"].ToString() == "S")
                {
                    //Get date and time
                    DateTime requestDateTime = Convert.ToDateTime(dataReader["accept_time"]);
                    DateTime dateTimeNow = DateTime.Now;
                    // Compare and convert to minutes
                    TimeSpan ts = dateTimeNow - requestDateTime;
                    //Set cancel time constraint
                    if (ts.TotalMinutes <= 5)
                    {
                        Response.Redirect("CancelService.aspx");
                    }
                    else if(ts.TotalMinutes > 5)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Unable to Cancel, The servicer is already on their way')", true);
                    }
                
                    
                }
            }
            con.Close();
        }
    }
}