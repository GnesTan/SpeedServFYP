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
    public partial class PayService : System.Web.UI.Page
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
            String strDateTime = "";
            String strServiceCat = "";
            String strServiceType = "";
            String strHomeAddress = "";
            String strState = "";
            String strDistrict = "";
            String strRemark = "";
            String strStatus = "";
            String strPayServicerId = "";
            int intRewardPoints = 0;
            decimal decAmountCharge = 0;
            con.Open();
            //Retreive data
            String strAdd = "SELECT * FROM Service_Request S,Client C WHERE C.client_id = S.client_id AND S.client_id = @strId AND (S.status = 'W' OR S.status = 'V') AND request_no = @requestNo;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            cmdAdd.Parameters.AddWithValue("@requestNo", strRequestNo);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strServiceTitle = dataReader["title"].ToString();
                strDateTime = dataReader["date_and_time"].ToString();
                strServiceCat = dataReader["service_category"].ToString();
                strServiceType = dataReader["service_type"].ToString();
                strHomeAddress = dataReader["address"].ToString();
                strState = dataReader["state"].ToString();
                strDistrict = dataReader["district"].ToString();
                strRemark = dataReader["remark"].ToString();
                strStatus = dataReader["status"].ToString();
                intRewardPoints = Int32.Parse(dataReader["reward_points"].ToString());
                decAmountCharge = Decimal.Parse(dataReader["amount_charges"].ToString());

                if (dataReader["servicer_id"] != DBNull.Value)
                {
                    strPayServicerId = dataReader["servicer_id"].ToString();
                }


            }
            con.Close();

            if (strRequestNo != null)
            {
                con.Open();
                //Retreive data
                String strSel = "SELECT * FROM banktransfer_request where request_no = @requestNo and status = 'P';";

                SqlCommand cmdSel = new SqlCommand(strSel, con);
                cmdSel.Parameters.AddWithValue("@requestNo", strRequestNo);
                SqlDataReader dr;
                dr = cmdSel.ExecuteReader();
                if (dr.HasRows)
                {
                    btnContinuePay.Visible = false;
                    lblPending.Visible = true;
                    servbar.Attributes.Add("class", "active");
                    cbConfirmDiscount.Enabled = false;
                }
                else
                {
                    btnContinuePay.Visible = true;
                    lblPending.Visible = false;
                    

                }
                con.Close();
            }


            // Display Data
            lblServiceTitle.Text = strServiceTitle;
            lblDateTime.Text = strDateTime;
            lblServiceCat.Text = strServiceCat;
            lblServiceType.Text = strServiceType;
            lblHomeAddress.Text = strHomeAddress;
            lblState.Text = strState;
            lblDistrict.Text = strDistrict;
            lblRemark.Text = strRemark;
            lblSubTotal.Text = "RM " + decAmountCharge.ToString();
            lblTotal.Text = "RM " + decAmountCharge.ToString();
            lblRewardPoints.Text = intRewardPoints.ToString() + " pts";
            Session["points"] = intRewardPoints;
            Session["amountCharge"] = decAmountCharge;
            Session["strPayServicerId"] = strPayServicerId;
            Session["strServiceTitle"] = strServiceTitle;


        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void cbConfirmDiscount_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConfirmDiscount.Checked == true)
            {
                int intRewardPoints = Convert.ToInt32(Session["points"]);
                decimal decAmountCharge = Convert.ToDecimal(Session["amountCharge"]);
                if (intRewardPoints < 100)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('You need at least 100 rewards points to get discount')", true);
                    cbConfirmDiscount.Checked = false;
                }
                else
                {
                    double decHalf = 0.5;
                    double dblRewardPoints = intRewardPoints;
                    double convertPoint = dblRewardPoints / 100;
                    //Count discountAmount
                    decimal calDiscount = (decAmountCharge * Convert.ToDecimal(decHalf)) - (Convert.ToDecimal(convertPoint));
                    if (calDiscount >= 0)
                    {
                        int intRemainPoints = 0;
                        decimal decDiscountAmount = Convert.ToDecimal(convertPoint);
                        decimal decTotal = decAmountCharge - decDiscountAmount;
                        Session["totalPayment"] = decTotal.ToString("0.00");
                        Session["remainPoints"] = intRemainPoints.ToString();
                        lblRemain.Text = intRemainPoints.ToString() + " pts";
                        lblDis.Text = "RM " + decDiscountAmount.ToString("0.00");
                        lblDis.ForeColor = System.Drawing.Color.Green;
                        lblTotal.Text = "RM " + decTotal.ToString("0.00");

                    }
                    else if (calDiscount <= 0)
                    {
                        int negative = -1;
                        decimal decRemainPoints = (calDiscount * Convert.ToDecimal(negative)) * 100;
                        int intRemainPoints = Decimal.ToInt32(decRemainPoints);
                        decimal decDiscountAmount = decAmountCharge * Convert.ToDecimal(decHalf);
                        decimal decTotal = decAmountCharge - decDiscountAmount;
                        Session["totalPayment"] = decTotal.ToString("0.00");
                        Session["remainPoints"] = intRemainPoints.ToString();
                        lblRemain.Text = intRemainPoints.ToString() + " pts";
                        lblDis.Text = "RM " + decDiscountAmount.ToString("0.00");
                        lblDis.ForeColor = System.Drawing.Color.Green;
                        lblTotal.Text = "RM " + decTotal.ToString("0.00");
                    }
                }
            }
            else if (cbConfirmDiscount.Checked == false)
            {
                lblDis.ForeColor = System.Drawing.Color.Black;
                lblDis.Text = "-";
                lblRemain.Text = "-";
                lblTotal.Text = "RM " + Session["amountCharge"].ToString();
            }

        }

        protected void btnContinuePay_Click(object sender, EventArgs e)
        {
            if (cbConfirmDiscount.Checked == false)
            {
                Session["finalTotalPrice"] = Convert.ToDecimal(Session["amountCharge"]);
                Session["finalCurrentPoints"] = Convert.ToInt32(Session["points"]);
                Response.Redirect("PaymentMethodSelectionClient.aspx");

            }
            else if (cbConfirmDiscount.Checked == true)
            {
                Session["finalTotalPrice"] = Convert.ToDecimal(Session["totalPayment"]);
                Session["finalCurrentPoints"] = Session["remainPoints"];
                Response.Redirect("PaymentMethodSelectionClient.aspx");
            }

        }

        protected void btnMoreDetails_Click(object sender, EventArgs e)
        {
            Session["redirectId"] = null;
            Response.Redirect("~/Client/PaymentDetails.aspx");
        }
    }
}