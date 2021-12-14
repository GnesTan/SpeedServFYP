using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Web.Mvc;
using PayPal.Api;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Collections;

namespace ServiceProvidingSystem.Servicer
{
    public partial class MySubscription : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Subscription_Request";

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

                //check whether is expired
                String strId = "";
                DateTime validDate = DateTime.Now;
                int isNull = 0;

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                    SqlDataReader dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        if (dataReader["valid_date"] != DBNull.Value)
                        {
                            validDate = DateTime.ParseExact(dataReader["valid_date"].ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                            lblRetreValidity.Text = validDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            isNull = 1;
                            lblRetreValidity.Text = "N/A";
                        }

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



                // check whether is expired
                int result = DateTime.Compare(validDate.Date, DateTime.Now.Date);
                if (result < 0 || isNull == 1)
                {
                    lblRetreAmount.Text = "80.00";
                    lblRetreStatus.Text = "Inactive";
                }


                BindGrid();



            }


        }

        private void BindGrid()
        {

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            //bind for rejected subscription
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Subscription_Request WHERE servicer_id = '" + strId + "' AND status = 'R' ORDER BY date_and_time DESC;";
                //Do your database connection stuff and get your data
                SqlConnection cn = new SqlConnection(str);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                cmd.CommandText = strSelect;

                //save the result in data table
                DataTable dt = new DataTable();
                ad.SelectCommand = cmd;
                ad.Fill(dt);

                //Create the PagedDataSource that will be used in paging
                PagedDataSource pgitems = new PagedDataSource();
                pgitems.DataSource = dt.DefaultView;
                pgitems.AllowPaging = true;

                //Control page size from here 
                pgitems.PageSize = 5;
                pgitems.CurrentPageIndex = PageNumber;
                if (pgitems.PageCount > 1)
                {
                    rptPaging.Visible = true;
                    ArrayList pages = new ArrayList();
                    for (int i = 0; i <= pgitems.PageCount - 1; i++)
                    {
                        if (pgitems.CurrentPageIndex == i)
                        {
                            pages.Add("<u>" + (i + 1).ToString() + "</u>");
                        }
                        else
                        {
                            pages.Add((i + 1).ToString());
                        }

                    }
                    rptPaging.DataSource = pages;
                    rptPaging.DataBind();
                }
                else
                {
                    rptPaging.Visible = false;
                }

                //Finally, set the datasource of the repeater
                RejectedRepeater.DataSource = pgitems;
                RejectedRepeater.DataBind();


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


        protected void lbPay_Click(object sender, EventArgs e)
        {
            //check whether have pending requesyt
            int checkCount = 0;

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            con.Open();

            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE servicer_id = @servicer_id AND status = 'P';";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    checkCount++;
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

            //check there are no pending subscription request
            if (checkCount == 0)
            {
                Session["paymentAmt"] = lblRetreAmount.Text;

                Response.Redirect("~/Servicer/PaymentMethodSelection.aspx");
            }
            else
            {
                lblError.Text = "*Pending request found, please wait request processed before make a new payment.";
            }



        }


        //This property will contain the current page number 
        public int PageNumber
        {
            get
            {
                if (ViewState["PageNumber"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber"] = value; }
        }

        //This method will fire when clicking on the page no link from the pager repeater
        protected void rptPaging_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            try
            {
                PageNumber = Convert.ToInt32(e.CommandArgument) - 1;
                BindGrid();
            }
            catch (Exception ex)
            {

            }

        }


    }




}