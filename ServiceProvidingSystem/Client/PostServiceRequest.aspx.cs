using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Client
{
    public partial class PostServiceRequest : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
                Bind_ddlState();
            }
        }
        private void BindGrid()
        {
            String strId = "";
            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            lblError.Text = "";
            String strName = "";
            String strPhnNo = "";
            String strAddress = "";

            con.Open();

            //Retreive data
            String strAdd = "SELECT * FROM CLIENT WHERE CLIENT_ID = @strId;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strName = dataReader["full_name"].ToString();
                strPhnNo = dataReader["contact_no"].ToString();
                strAddress = dataReader["home_address"].ToString();
            }

            con.Close();
            //Display Data
            txtName.Text = strName;
            txtPhnNo.Text = strPhnNo;
            txtHomeAddress.Text = strAddress;
        }
        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                ddlInstallType.Visible = true;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = true;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = true;
            }
        }


        public void Bind_ddlState()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select state_name, state_id FROM state", con);
            SqlDataReader dr = cmd.ExecuteReader();
            ddlState.DataSource = dr;
            ddlState.Items.Clear();
            ddlState.Items.Add("--Please Select State--");
            ddlState.DataTextField = "state_name";
            ddlState.DataValueField = "state_id";
            ddlState.DataBind();
            con.Close();
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlState.SelectedValue.Equals("--Please Select State--"))
            {
                lblError.Text = "";
                Bind_ddlDistrict();
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add("--Please Select District--");
            }
        }

        public void Bind_ddlDistrict()
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select district_name, district_id FROM district WHERE state_id ='" + ddlState.SelectedValue + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                ddlDistrict.DataSource = dr;
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add("--Please Select District--");
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_id";
                ddlDistrict.DataBind();
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

            if (ddlDistrict.Items.Count == 1)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem("", ""));
            }

        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlDistrict.SelectedValue.Equals("--Please Select District--"))
            {
                lblError.Text = "";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ClientHomePage.aspx");
        }

        protected void btnRequest_Click(object sender, EventArgs e)
        {
            if (!ddlState.SelectedItem.Text.Equals("--Please Select State--") && !ddlDistrict.SelectedItem.Text.Equals("--Please Select District--"))
            {
                //Send request
                String strId = "";
                int totalCurrentService = 0;
                char currentRank = ' ';
                int maxService = 0;
                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }
                //Get client current rank
                con.Open();
                String strAdd = "SELECT CLIENT_RANK FROM CLIENT WHERE CLIENT_ID = @strId";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@strId", strId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    currentRank = Convert.ToChar(dataReader["CLIENT_RANK"].ToString());
                }
                con.Close();

                if (currentRank == 'B')
                {
                    maxService = 2;
                }
                else if (currentRank == 'S')
                {
                    maxService = 3;
                }
                else if (currentRank == 'G')
                {
                    maxService = 5;
                }

                //Get current waiting, serving and pending payment services
                con.Open();
                String strAdd1 = "SELECT COUNT(REQUEST_NO) FROM [dbo].[Service_Request] WHERE CLIENT_ID = @strId AND (STATUS = 'L' OR STATUS = 'S' OR STATUS = 'W' OR STATUS = 'V')";
                SqlCommand cmdAdd1 = new SqlCommand(strAdd1, con);
                cmdAdd1.Parameters.AddWithValue("@strId", strId);
                SqlDataReader dataReader1;
                dataReader1 = cmdAdd1.ExecuteReader();
                while (dataReader1.Read())
                {
                    totalCurrentService = Convert.ToInt32(dataReader1[0].ToString());
                }
                con.Close();

                //Check client total current service requests and compare with current rank limit
                if (totalCurrentService >= maxService)
                {
                    Response.Write("<script>alert('Maximum Limit of Service Requests Reached')</script>");
                }
                else if (totalCurrentService < maxService)
                {
                    SendServiceRequest();
                }
            }
            else
            {
                lblError.Text = "Please select a State and City";
            }

        }

        private void SendServiceRequest()
        {

            //Send request
            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                Session["serviceType"] = ddlInstallType.SelectedValue;
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                Session["serviceType"] = ddlRepairType.SelectedValue;
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                Session["serviceType"] = ddlOtherType.SelectedValue;
            }

            String strId = Session["strId"].ToString();
            String strFullName = txtName.Text;
            String strPhnNo = txtPhnNo.Text;
            String strHomeAddress = txtHomeAddress.Text;
            String strState = ddlState.SelectedItem.Text;
            String strDistrict = "";
            if (!ddlDistrict.SelectedItem.Text.Equals("--Please Select district--"))
            {
                strDistrict = ddlDistrict.SelectedItem.Text;
            }
            String strServiceCat = ddlCategory.SelectedValue;
            String strServiceType = Session["serviceType"].ToString();
            String strServiceTitle = txtServiceTitle.Text;
            String strBudget = "RM" + txtBudget.Text;
            String strRemark = txtRemark.Text;
            String strDateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
            String strStatus = "L";



            //Save Personal Info if checkbox ticked
            if (cbSaveHomeAddress.Checked == true)
            {
                con.Open();
                String strAdd = "UPDATE CLIENT SET FULL_NAME = @full_name, CONTACT_NO = @contact_no, HOME_ADDRESS = @home_address WHERE CLIENT_ID = @strId;";
                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@full_name", strFullName);
                cmdAdd.Parameters.AddWithValue("@contact_no", strPhnNo);
                cmdAdd.Parameters.AddWithValue("@home_address", strHomeAddress);
                cmdAdd.Parameters.AddWithValue("@strId", strId);
                cmdAdd.ExecuteNonQuery();
                con.Close();
            }

            //retrieve request id sequence
            int newRequestId = 0;
            con.Open();
            String strSel = "SELECT * FROM Sequence WHERE sequence_id = 'default';";
            SqlCommand cmdSel = new SqlCommand(strSel, con);
            SqlDataReader dataReader;
            dataReader = cmdSel.ExecuteReader();
            while (dataReader.Read())
            {
                newRequestId = (int)dataReader["request_sequence"];
            }
            con.Close();
            String strFavouriteId = "SR" + newRequestId.ToString("D" + 8);

            //Start Send Request
            string query = "INSERT INTO [SERVICE_REQUEST](REQUEST_NO, DATE_AND_TIME, SERVICE_CATEGORY, SERVICE_TYPE, ADDRESS, STATE, DISTRICT, BUDGET, REMARK,TITLE, STATUS, CLIENT_ID ) VALUES (@request_no, @date_and_time, @service_category, @service_type, @address, @state, @district, @budget, @remark, @title, @status, @strId)";
            using (SqlConnection con = new SqlConnection(str))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@request_no", strFavouriteId);
                    cmd.Parameters.AddWithValue("@date_and_time", strDateTime);
                    cmd.Parameters.AddWithValue("@service_category", strServiceCat);
                    cmd.Parameters.AddWithValue("@service_type", strServiceType);
                    cmd.Parameters.AddWithValue("@address", strHomeAddress);
                    cmd.Parameters.AddWithValue("@state", strState);
                    cmd.Parameters.AddWithValue("@district", strDistrict);
                    cmd.Parameters.AddWithValue("@budget", strBudget);
                    cmd.Parameters.AddWithValue("@remark", strRemark);
                    cmd.Parameters.AddWithValue("@title", strServiceTitle);
                    cmd.Parameters.AddWithValue("@status", strStatus);
                    cmd.Parameters.AddWithValue("@strId", strId);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            con.Open();

            try
            {

                //update service request sequence id
                String strUpdate = "UPDATE Sequence SET request_sequence = @request_sequence WHERE sequence_id = 'default'";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                cmdUpdate.Parameters.AddWithValue("@request_sequence", newRequestId + 1);

                cmdUpdate.ExecuteNonQuery();


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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Service had been Requested successfully'); window.location='" + Request.ApplicationPath + "Client/ClientHomePage.aspx';", true);
        }

    }
}
