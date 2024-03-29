﻿using System;
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
    public partial class CurrentServingList : System.Web.UI.Page
    {
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
        private void BindGrid()
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            SqlConnection con;
            String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
            con = new SqlConnection(str);

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT S.request_no, S.date_and_time, S.service_category, S.service_type, S.state, S.status, S.district, C.full_name FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND (S.status = 'S' OR S.status = 'W') AND S.servicer_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

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

        protected void btnView_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedRequest = (Label)item.FindControl("lblRequestNo");

            //save the selectedUsername to redirect to the edit page
            Session["selectedRequest"] = selectedRequest.Text;

            Response.Redirect("~/Servicer/CurrentServing.aspx");
        }

    }
}