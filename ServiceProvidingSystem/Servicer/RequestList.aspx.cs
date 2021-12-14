using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.Collections;

namespace ServiceProvidingSystem.Servicer
{
    public partial class RequestList : System.Web.UI.Page
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


                Bind_ddlstate();


                //retrieve servicer default area and service type and category
                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                String stateName = "";
                String districtName = "";
                String serviceCategory = "";
                String serviceType = "";
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
                        if (dataReader["state_name"] != DBNull.Value)
                        {
                            stateName = dataReader["state_name"].ToString();
                        }
                        if (dataReader["district_name"] != DBNull.Value)
                        {
                            districtName = dataReader["district_name"].ToString();
                        }
                        if (dataReader["service_category"] != DBNull.Value)
                        {
                            serviceCategory = dataReader["service_category"].ToString();
                        }
                        if (dataReader["service_type"] != DBNull.Value)
                        {
                            serviceType = dataReader["service_type"].ToString();
                        }

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

                if (!stateName.Equals(""))
                {

                    ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(stateName));

                    Bind_ddldistrict();

                    if (!districtName.Equals(""))
                    {

                        ddldistrict.SelectedIndex = ddldistrict.Items.IndexOf(ddldistrict.Items.FindByText(districtName));
                    }

                }

                if (!serviceCategory.Equals(""))
                {

                    ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(serviceCategory));

                    bindType();

                    if (!serviceType.Equals(""))
                    {

                        ddlType.SelectedIndex = ddlType.Items.IndexOf(ddlType.Items.FindByText(serviceType));
                    }

                }

                Session["validityDate"] = validityDate;


                this.BindGrid();


                String strSelectCount = "SELECT COUNT(S.request_no) FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.status = 'L' AND s.servicer_id = '" + strId + "';";

                try
                {
                    con.Open();
                    SqlCommand comm = new SqlCommand(strSelectCount, con);
                    Int32 count = (Int32)comm.ExecuteScalar();

                    if (count > 0)
                    {
                        lbDirectRequest.Text = " Direct Request(" + count.ToString() + ")";
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




            }



        }
        private void BindGrid()
        {
            //add all filter to statement
            String strSQL = "";

            if (!ddlstate.SelectedItem.Text.Equals("--All State--"))
            {
                strSQL += "AND S.state = '" + ddlstate.SelectedItem.Text + "' ";
                if (!ddldistrict.SelectedItem.Text.Equals("--All District--"))
                {
                    strSQL += "AND S.district = '" + ddldistrict.SelectedItem.Text + "' ";
                }
            }

            strSQL += "AND S.service_category = '" + ddlCategory.SelectedItem.Text + "' ";

            if (!ddlType.SelectedItem.Text.Equals("--Any Type--"))
            {
                strSQL += "AND S.service_type = '" + ddlType.SelectedItem.Text + "' ";
            }



            String strSelect = "SELECT S.request_no, S.date_and_time, S.service_category, S.service_type, S.state, S.status, S.district, C.full_name FROM Service_Request S, Client C WHERE S.client_id = C.client_id AND S.status = 'L' AND S.servicer_id IS NULL " + strSQL + ";";


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

            if(Session["validityDate"] != null)
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

        public void Bind_ddlstate()
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select state_name, state_id FROM state", con);
                SqlDataReader dr = cmd.ExecuteReader();
                ddlstate.DataSource = dr;
                ddlstate.Items.Clear();
                ddlstate.Items.Add("--All State--");
                ddlstate.DataTextField = "state_name";
                ddlstate.DataValueField = "state_id";
                ddlstate.DataBind();

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
        public void Bind_ddldistrict()
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select district_name, district_id FROM district WHERE state_id ='" + ddlstate.SelectedValue + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                ddldistrict.DataSource = dr;
                ddldistrict.Items.Clear();
                ddldistrict.Items.Add("--All District--");
                ddldistrict.DataTextField = "district_name";
                ddldistrict.DataValueField = "district_id";
                ddldistrict.DataBind();
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

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlstate.SelectedValue.Equals("--All State--"))
            {
                Bind_ddldistrict();
            }
            else
            {
                ddldistrict.Items.Clear();
                ddldistrict.Items.Add("--All District--");
            }

            this.BindGrid();
        }

        protected void ddldistrict_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.BindGrid();
        }

        protected void btndefault_Click(object sender, EventArgs e)
        {

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            String stateName = "";
            String districtName = "";
            String serviceCategory = "";
            String serviceType = "";

            if (!ddlstate.SelectedItem.Text.Equals("--All State--"))
            {
                stateName = ddlstate.SelectedItem.Text;
            }

            if (!ddldistrict.SelectedItem.Text.Equals("--All District--"))
            {
                districtName = ddldistrict.SelectedItem.Text;
            }

            serviceCategory = ddlCategory.SelectedItem.Text;

            if (!ddlType.SelectedItem.Text.Equals("--Any Type--"))
            {
                serviceType = ddlType.SelectedItem.Text;
            }

            //save selection as default
            con.Open();

            try
            {


                //update data
                String strAdd = "UPDATE Servicer SET state_name = @state_name, district_name = @district_name, service_category = @service_category, service_type = @service_type WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@state_name", stateName);
                cmdAdd.Parameters.AddWithValue("@district_name", districtName);
                cmdAdd.Parameters.AddWithValue("@service_category", serviceCategory);
                cmdAdd.Parameters.AddWithValue("@service_type", serviceType);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);

                cmdAdd.ExecuteNonQuery();

                con.Close();


                //display successful message
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Sucessfully saved as default area." + "');", true);

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

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindType();
            this.BindGrid();
        }



        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        //change the type dropdownlist when category selection is changed
        private void bindType()
        {
            ddlType.Items.Clear();
            ddlType.Items.Add(new ListItem("--Any Type--", "--Any Type--"));

            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                ddlType.Items.Add(new ListItem("Vehicles", "Vehicles"));
                ddlType.Items.Add(new ListItem("Home appliances", "Home appliances"));
                ddlType.Items.Add(new ListItem("Mobile & Gadget", "Mobile & Gadget"));
                ddlType.Items.Add(new ListItem("Computer & Accessories", "Computer & Accessories"));
                ddlType.Items.Add(new ListItem("Musical instrument", "Musical instrument"));
                ddlType.Items.Add(new ListItem("Industrial Machine", "Industrial Machine"));
                ddlType.Items.Add(new ListItem("Gaming PC", "Gaming PC"));
                ddlType.Items.Add(new ListItem("Camera & Drones", "Camera & Drones"));
                ddlType.Items.Add(new ListItem("Network Infrastructure", "Network Infrastructure"));
                ddlType.Items.Add(new ListItem("Others", "Others"));
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                ddlType.Items.Add(new ListItem("Vehicles", "Vehicles"));
                ddlType.Items.Add(new ListItem("Home appliances", "Home appliances"));
                ddlType.Items.Add(new ListItem("Mobile & Gadget", "Mobile & Gadget"));
                ddlType.Items.Add(new ListItem("Computer & Accessories", "Computer & Accessories"));
                ddlType.Items.Add(new ListItem("Musical instrument", "Musical instrument"));
                ddlType.Items.Add(new ListItem("Industrial Machine", "Industrial Machine"));
                ddlType.Items.Add(new ListItem("Watches", "Watches"));
                ddlType.Items.Add(new ListItem("Game Console", "Game Console"));
                ddlType.Items.Add(new ListItem("Camera & Drones", "Camera & Drones"));
                ddlType.Items.Add(new ListItem("Network Infrastructure", "Network Infrastructure"));
                ddlType.Items.Add(new ListItem("Others", "Others"));
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                ddlType.Items.Add(new ListItem("Insecticide", "Insecticide"));
                ddlType.Items.Add(new ListItem("Porter services", "Porter services"));
                ddlType.Items.Add(new ListItem("Data entry", "Data entry"));
                ddlType.Items.Add(new ListItem("Distribute flyers", "Distribute flyers"));
                ddlType.Items.Add(new ListItem("Transportation", "Transportation"));
                ddlType.Items.Add(new ListItem("Others", "Others"));
            }
        }


        protected void lbDirectRequest_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/DirectRequest.aspx");

        }



    }
}