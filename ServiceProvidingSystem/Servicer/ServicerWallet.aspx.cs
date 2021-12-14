using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace ServiceProvidingSystem.Servicer
{
    public partial class ServicerWallet : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Payment";

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

                //retrieve credit balance
                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                    SqlDataReader dataReader;
                    dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        lblRetreBalance.Text = dataReader["credit_balance"].ToString();
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

                this.BindGrid();
            }


        }

        //retrieve payment information and bind
        private void BindGrid()
        {

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            //bind for all payments
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM " + table + " WHERE servicer_id = '" + strId + "' ORDER BY date_and_time DESC;";
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
                RepeaterPayment.DataSource = pgitems;
                RepeaterPayment.DataBind();

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

            //bind for rejected withdrawal
            con.Open();
            try
            {

                //retrieve data
                String strSelect = "SELECT * FROM Withdrawal_Request WHERE servicer_id = '" + strId + "' AND status = 'R' ORDER BY date_and_time DESC;";
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
                pgitems.CurrentPageIndex = PageNumber2;
                if (pgitems.PageCount > 1)
                {
                    rptPaging2.Visible = true;
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
                    rptPaging2.DataSource = pages;
                    rptPaging2.DataBind();
                }
                else
                {
                    rptPaging2.Visible = false;
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



        protected void lbWithdraw_Click(object sender, EventArgs e)
        {
            if (!lblRetreBalance.Text.Equals("0.00"))
            {
                //check whether have pending request
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
                    String strSelect = "SELECT * FROM Withdrawal_Request WHERE servicer_id = @servicer_id AND status = 'P';";

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

                //if no pending request, redirect to credit withdraw page
                if (checkCount == 0)
                {
                    Session["creditBalance"] = lblRetreBalance.Text;

                    Response.Redirect("~/Servicer/CreditWithdraw.aspx");


                }
                else
                {
                    lblError.Text = "*Pending withdrawal found, please wait request processed before make a new withdrawal.";
                }


            }
            else
            {
                lblError.Text = "*There is no balance available to withdraw.";
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

        //This property will contain the current page number 
        public int PageNumber2
        {
            get
            {
                if (ViewState["PageNumber2"] != null)
                {
                    return Convert.ToInt32(ViewState["PageNumber2"]);
                }
                else
                {
                    return 0;
                }
            }
            set { ViewState["PageNumber2"] = value; }
        }

        //This method will fire when clicking on the page no link from the pager repeater
        protected void rptPaging2_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            try
            {
                PageNumber2 = Convert.ToInt32(e.CommandArgument) - 1;
                BindGrid();
            }
            catch (Exception ex)
            {

            }

        }



    }
}