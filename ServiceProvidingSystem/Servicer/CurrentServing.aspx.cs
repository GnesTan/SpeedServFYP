using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;

namespace ServiceProvidingSystem.Servicer
{
    public partial class CurrentServing : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Service_Request";

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

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
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
                String strPhoneNo = "";
                String strEstimateTime = "";

                int recordFound = 0;

                con.Open();
                try
                {

                    //retrieve data
                    String strAdd = "SELECT S.request_no, S.address, S.remark, S.service_category, S.service_type, S.title, S.budget, S.estimate_time, C.full_name, C.profile_picture, S.status, S.amount_charges, C.contact_no FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.request_no = @request_no;";

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
                        strEstimateTime = dataReader["estimate_time"].ToString();
                        if (strStatus != "" && strStatus != null)
                        {
                            charStatus = char.Parse(strStatus);
                        }
                        if (dataReader["amount_charges"] != DBNull.Value)
                        {
                            strAmountCharges = string.Format("{0:N2}", dataReader["amount_charges"]);
                        }
                        strPhoneNo = dataReader["contact_no"].ToString();
                        recordFound++;
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

                //check whether there are any service servicing
                if (recordFound == 0)
                {
                    //display no item interface
                    UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
                    NonePanel.Visible = true;
                    UpdatePanel1.Update();
                }
                else
                {
                    //display the service
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
                    lblEstimate.Text = strEstimateTime;
                    if (!strAmountCharges.Equals(""))
                    {
                        lblPriceAmount.Text = strAmountCharges;
                        lblPrice.Visible = true;
                        lblPriceAmount.Visible = true;
                    }


                    if (charStatus == 'W')
                    {
                        lblServingStatus.Text = "Waiting for Payment";
                        btnComplete.Visible = false;
                        ItemPricePanel.Visible = true;
                        BindItemPrice();
                    }

                    Session["servicingPhoneNo"] = strPhoneNo;

                }

                BindRepeater();

            }

        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;

            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0 && dt.Rows.Count <= 10)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        TextBox txtDesc = (TextBox)Repeater1.Items[rowIndex].FindControl("txtDesc");
                        TextBox txtPrice = (TextBox)Repeater1.Items[rowIndex].FindControl("txtPrice");
                        drCurrentRow = dt.NewRow();
                        dt.Rows[i - 1]["txtDesc"] = txtDesc.Text;
                        dt.Rows[i - 1]["txtPrice"] = txtPrice.Text;
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["Curtbl"] = dt;
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState Value is Null");
            }
            SetOldData();
        }

        protected void btndel_Click(object sender, EventArgs e)
        {
            RepeaterItem item = (sender as LinkButton).NamingContainer as RepeaterItem;

            int rowIndex = 0;
            int rowID = item.ItemIndex;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= Repeater1.Items.Count; i++)
                    {
                        TextBox txtDesc = (TextBox)Repeater1.Items[rowIndex].FindControl("txtDesc");
                        TextBox txtPrice = (TextBox)Repeater1.Items[rowIndex].FindControl("txtPrice");
                        dt.Rows[i - 1]["txtDesc"] = txtDesc.Text;
                        dt.Rows[i - 1]["txtPrice"] = txtPrice.Text;
                        rowIndex++;
                    }

                    if (item.ItemIndex <= dt.Rows.Count - 1 && dt.Rows.Count > 1)
                    {
                        dt.Rows.Remove(dt.Rows[rowID]);
                    }
                }

                ViewState["Curtbl"] = dt;
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }

            SetOldData();
        }

        protected void BindItemPrice()
        {
            String selectedRequest = "";

            if (Session["selectedRequest"] != null)
            {
                selectedRequest = Session["selectedRequest"].ToString();
            }


            SqlCommand cmd = new SqlCommand("Select description, price FROM ItemPrice WHERE request_no = '" + selectedRequest + "';", con);

            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                rptTable.DataSource = ds;
                rptTable.DataBind();
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

        }

        protected void BindRepeater()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("txtDesc", typeof(string));
            dt.Columns.Add("txtPrice", typeof(string));
            DataRow dr = dt.NewRow();
            dr["txtDesc"] = string.Empty;
            dr["txtPrice"] = string.Empty;
            dt.Rows.Add(dr);
            ViewState["Curtbl"] = dt;
            Repeater1.DataSource = dt;
            Repeater1.DataBind();

        }

        private void SetOldData()
        {
            int rowIndex = 0;
            if (ViewState["Curtbl"] != null)
            {
                DataTable dt = (DataTable)ViewState["Curtbl"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtDesc = (TextBox)Repeater1.Items[rowIndex].FindControl("txtDesc");
                        TextBox txtPrice = (TextBox)Repeater1.Items[rowIndex].FindControl("txtPrice");
                        txtDesc.Text = dt.Rows[i]["txtDesc"].ToString();
                        txtPrice.Text = dt.Rows[i]["txtPrice"].ToString();
                        rowIndex++;
                    }

                    sumTotalPrice();
                }
            }
        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/RequestList.aspx");
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            String servicerName = Session["strRetrieveName"].ToString();
            String whatsappLink = "https://wa.me/6" + Session["servicingPhoneNo"].ToString() + "?text=Hi!%20Thanks%20for%20requesting%20Service%20from%20SpeedServ!%20My%20name%20is%20" + servicerName + ",%20we%20can%20start%20our%20conversion%20here%20for%20the%20service%20you%20requested.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", "window.open('" + whatsappLink + "');", true);
        }


        protected void btnComplete_Click(object sender, EventArgs e)
        {
            UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
            WholeFormPanel.Visible = false;
            InputAmountPanel.Visible = true;
            UpdatePanel1.Update();
        }

        protected void btnCancelInterface_Click(object sender, EventArgs e)
        {
            //display cancellation interface
            UpdatePanel1.UpdateMode = UpdatePanelUpdateMode.Conditional;
            WholeFormPanel.Visible = false;
            CancelPanel.Visible = true;
            UpdatePanel1.Update();
        }




        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            double dblAmount = 0.00;

            //get request no
            String selectedRequest = Session["selectedRequest"].ToString();

            String strAdd = "";

            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox txtDesc = (TextBox)item.FindControl("txtDesc");
                TextBox txtPrice = (TextBox)item.FindControl("txtPrice");

                dblAmount += Convert.ToDouble(txtPrice.Text);

                con.Open();

                try
                {

                    strAdd = "INSERT INTO ItemPrice (request_no, description, price) VALUES ('" + selectedRequest + "', '" + txtDesc.Text + "'," + txtPrice.Text + ");";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                    // execute the query to update the database
                    cmdAdd.ExecuteNonQuery();


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

            }



            String currentRequestNo = "";

            if (Session["selectedRequest"] != null)
            {
                currentRequestNo = Session["selectedRequest"].ToString();
            }



            // update service request status to waiting for payment
            con.Open();

            try
            {



                //retrieve data
                String strUpdate = "UPDATE " + table + " SET status = 'W', amount_charges = @amount_charges WHERE request_no = @request_no;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@amount_charges", dblAmount);
                cmdUpdate.Parameters.AddWithValue("@request_no", currentRequestNo);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                //disp;ay popup message and refresh
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

        //for canceling service
        protected void btnCancelService_Click(object sender, EventArgs e)
        {

            String currentRequestNo = "";

            if (Session["selectedRequest"] != null)
            {
                currentRequestNo = Session["selectedRequest"].ToString();
            }




            con.Open();

            try
            {



                //retrieve data
                String strAdd = "UPDATE " + table + " SET status = 'C', cancel_reason = @cancel_reason WHERE request_no = @request_no;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@request_no", currentRequestNo);
                cmdAdd.Parameters.AddWithValue("@cancel_reason", txtReason.Text);
                cmdAdd.ExecuteNonQuery();

                con.Close();

                //disp;ay popup message and refresh
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Successful cancelled for the service.');window.location.href='CurrentServingList.aspx';", true);


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


        protected void txtPrice_TextChanged(object sender, EventArgs e)
        {
            sumTotalPrice();
        }

        private void sumTotalPrice()
        {
            double sum = 0;

            for (int index = 0; index < this.Repeater1.Items.Count; index++)
            {
                TextBox txtPrice = this.Repeater1.Items[index].FindControl("txtPrice") as TextBox;
                double number;
                if (double.TryParse(txtPrice.Text, out number))
                {
                    sum += txtPrice.Text.Length > 0 ? Convert.ToDouble(txtPrice.Text) : 0;

                }


            }
        (this.Repeater1.Controls[this.Repeater1.Items.Count + 1].FindControl("txtTotal") as TextBox).Text = sum.ToString("0.00");
        }

    }
}