using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

namespace ServiceProvidingSystem.Servicer
{
    public partial class MyService : System.Web.UI.Page
    {
        //setup SQL connection
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

                this.BindGrid();
            }



        }

        //bind service offer to repeater
        private void BindGrid()
        {
            String strId = "";

            if(Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }


            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT offer_id, service_picture, service_category, service_type, state, district, delivery_fee, fees, service_title FROM Service_Offer WHERE servicer_id = @servicer_id;";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();

                if (!dtrProd.HasRows)
                {
                    RepeaterPanel.Visible = false;
                    NonePanel.Visible = true;
                }


                RepeaterRequest.DataSource = dtrProd;
                RepeaterRequest.DataBind();


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

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedOfferNo = (Label)item.FindControl("lblOfferNo");

            //save the selectedOfferNo and redirect to the edit page
            Session["selectedOfferNo"] = selectedOfferNo.Text;

            Response.Redirect("~/Servicer/EditService.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedOfferNo = (Label)item.FindControl("lblOfferNo");

            //declare String for the select account's username
            String strOfferNo = selectedOfferNo.Text;



            //execute delete query to delete selected service
            string query = "DELETE FROM Service_Offer WHERE offer_id = @offer_id";
            string constr = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Parameters.AddWithValue("@offer_id", strOfferNo);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            this.BindGrid();



        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            int countOffer = 0;

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            con.Open();
            try
            {
                //Retreive data
                String strAdd = "SELECT * FROM Service_Offer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    countOffer++;
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

            if(countOffer < 5)
            {
                Response.Redirect("~/Servicer/PostService.aspx");
            }
            else
            {
                //display error message if there are 5 services
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('You are not able to post more than 5 service, please delete existing service to add new one.');", true);
            }
        }


    }
}