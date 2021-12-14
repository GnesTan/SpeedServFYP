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
    public partial class ViewTransactionDetails : System.Web.UI.Page
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
            String currentRequestNo = "";
            if (Session["RequestNoStatus"] != null)
            {
                currentRequestNo = Session["RequestNoStatus"].ToString();
            }
            String currentPaymentId = "";
            if (Session["strPaymentId"] != null)
            {
                currentPaymentId = Session["strPaymentId"].ToString();
            }
            String strId = Session["strId"].ToString();

            decimal decPayAmount = 0;
            decimal decAmountCharge = 0;
            String strDateTime = "";            

            con.Open();
            //Retreive data
            String strAdd = "SELECT P.PAYMENT_AMOUNT, S.AMOUNT_CHARGES, P.DATE_AND_TIME FROM PAYMENT P, SERVICE_REQUEST S WHERE P.REQUEST_NO = S.REQUEST_NO AND P.REQUEST_NO = @requestNo AND P.CLIENT_ID = @strId AND P.PAYMENT_ID = @paymentId";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@requestNo", currentRequestNo);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            cmdAdd.Parameters.AddWithValue("@paymentId", currentPaymentId);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                decPayAmount = Convert.ToDecimal(dataReader["PAYMENT_AMOUNT"].ToString());
                decAmountCharge = Convert.ToDecimal(dataReader["AMOUNT_CHARGES"].ToString());
                strDateTime = dataReader["DATE_AND_TIME"].ToString();                
            }
            con.Close();
            double rewardPointsMultipler = 1.5;
            decimal decDiscountAmt = decAmountCharge - decPayAmount;
            int getRewardPoints = Convert.ToInt32((Convert.ToDouble(decPayAmount)) * (rewardPointsMultipler));            
            lblHeadTotal.Text = "RM " + decPayAmount;
            lblDateTime.Text = strDateTime;
            lblPrice.Text = "RM " + decAmountCharge;
            lblDiscountAmount.Text = "- RM " + decDiscountAmt;
            lblTotal.Text = "RM " + decPayAmount;
            lblEarnedPoints.Text = "+ " + getRewardPoints + " pts";            
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/RecentTransaction.aspx");
        }

        protected void btnViewServiceDetails_Click(object sender, EventArgs e)
        {
            Session["redirectId"] = "ViewTransactionDetails";
            Response.Redirect("~/Client/PaymentDetails.aspx");
        }
    }
}