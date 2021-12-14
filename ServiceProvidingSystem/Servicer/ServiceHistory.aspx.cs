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
    public partial class ServiceHistory : System.Web.UI.Page
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
                String strSelect = "SELECT S.request_no, S.date_and_time, S.service_category, S.service_type, S.state, S.status, S.district, C.full_name FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND (S.status = 'C' OR S.status = 'D') AND S.servicer_id = '" + strId + "';";

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
                pgitems.PageSize = 10;
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

                if (dt.Rows.Count == 0)
                {
                    RepeaterPanel.Visible = false;
                    NonePanel.Visible = true;
                }
                else
                {
                    RepeaterPanel.Visible = true;
                    NonePanel.Visible = false;
                }

                //Finally, set the datasource of the repeater
                RepeaterRequest.DataSource = pgitems;
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

            Response.Redirect("~/Servicer/ViewRequest.aspx");
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