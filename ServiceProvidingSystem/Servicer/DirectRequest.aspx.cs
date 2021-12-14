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
using System.Globalization;

namespace ServiceProvidingSystem.Servicer
{
    public partial class DirectRequest : System.Web.UI.Page
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


                //retrieve servicer default area and service type and category
                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }


                DateTime validityDate = DateTime.Now.AddDays(-1);

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

                        if (dataReader["valid_date"] != DBNull.Value)
                        {
                            validityDate = DateTime.ParseExact(dataReader["valid_date"].ToString(), "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
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



                Session["validityDate"] = validityDate;


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


            String strSelect = "SELECT S.request_no, S.date_and_time, S.service_category, S.service_type, S.state, S.status, S.district, C.full_name FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.status = 'L' AND s.servicer_id = '" + strId + "';";


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

            DateTime validityDate = DateTime.Now;

            if (Session["validityDate"] != null)
            {
                validityDate = Convert.ToDateTime(Session["validityDate"]);
            }

            if (validityDate > DateTime.Now)
            {
                if (dt.Rows.Count == 0)
                {
                    RepeaterPanel.Visible = false;
                    NonePanel.Visible = true;
                    ExpiredPanel.Visible = false;
                }
                else
                {
                    RepeaterPanel.Visible = true;
                    NonePanel.Visible = false;
                    ExpiredPanel.Visible = false;
                }
            }
            else
            {
                RepeaterPanel.Visible = false;
                NonePanel.Visible = false;
                ExpiredPanel.Visible = true;
            }

            //Finally, set the datasource of the repeater
            RepeaterRequest.DataSource = pgitems;
            RepeaterRequest.DataBind();


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

            Response.Redirect("~/Servicer/ViewRequest.aspx");
        }

        protected void lbRefresh_Click(object sender, EventArgs e)
        {
            this.BindGrid();
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