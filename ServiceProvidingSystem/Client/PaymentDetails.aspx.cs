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
    public partial class PaymentDetails : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            String strRequestNo = Session["RequestNoStatus"].ToString();           
            decimal price = 0;
            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM ITEMPRICE WHERE REQUEST_NO = @requestNo";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@requestNo", strRequestNo);
                SqlDataReader dtrFav = cmdSelect.ExecuteReader();
                              
                displayPayDet.DataSource = dtrFav;
                displayPayDet.DataBind();   
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
                //retrieve data
                String strSelect1 = "SELECT AMOUNT_CHARGES FROM SERVICE_REQUEST WHERE REQUEST_NO = @requestNo";
                SqlCommand cmdSelect1 = new SqlCommand(strSelect1, con);
                cmdSelect1.Parameters.AddWithValue("@requestNo", strRequestNo);
                SqlDataReader dtrFav1 = cmdSelect1.ExecuteReader();
                while (dtrFav1.Read())
                {
                    price = Convert.ToDecimal(dtrFav1["AMOUNT_CHARGES"].ToString());
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
                        
            lblTotal.Text = "RM " + Convert.ToDecimal(price.ToString("0.00"));
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {            
            if (Session["redirectId"] != null)
            {
                Session["redirectId"] = null;
                Response.Redirect("~/Client/ViewTransactionDetails.aspx");
                
            }
            else
            {
                Response.Redirect("~/Client/PayService.aspx");  
            }
            
        }
    }
}