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
    public partial class PaymentSuccess : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            int intEarnedPoints = Convert.ToInt32(Session["earnedPoints"].ToString());
            String currentRequestNo = "";
            if (Session["RequestNoStatus"] != null)
            {
                currentRequestNo = Session["RequestNoStatus"].ToString();
            }

            String strId = Session["strId"].ToString();
            

            decimal decPayAmount = 0;
            decimal decAmountCharge = 0;            
            String strDateTime = "";
            int intRewardPoints = 0;

            con.Open();
            //Retreive data
            String strAdd = "SELECT P.PAYMENT_AMOUNT, S.AMOUNT_CHARGES, P.DATE_AND_TIME, C.REWARD_POINTS FROM PAYMENT P, SERVICE_REQUEST S, CLIENT C WHERE P.REQUEST_NO = S.REQUEST_NO AND P.REQUEST_NO = @requestNo AND C.CLIENT_ID = @strId;";

            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@requestNo", currentRequestNo);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                decPayAmount = Convert.ToDecimal(dataReader["PAYMENT_AMOUNT"].ToString());
                decAmountCharge = Convert.ToDecimal(dataReader["AMOUNT_CHARGES"].ToString());
                strDateTime = dataReader["DATE_AND_TIME"].ToString();
                intRewardPoints = Convert.ToInt32(dataReader["REWARD_POINTS"].ToString());
            }
            con.Close();
            //Display Data
            decimal decDiscountAmt = decAmountCharge - decPayAmount;
            lblHeadTotal.Text = "RM " +decPayAmount ;
            lblDateTime.Text = strDateTime;
            lblPrice.Text = "RM " +decAmountCharge;
            lblDiscountAmount.Text = "- RM " + decDiscountAmt;
            lblTotal.Text = "RM " + decPayAmount;
            lblEarnedPoints.Text = "+ " + intEarnedPoints + " pts";
            lblCurrentTotalPoints.Text = + intRewardPoints + " pts";
        }

        protected void btnProceedReview_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/ReviewService.aspx");
        }

    }
}