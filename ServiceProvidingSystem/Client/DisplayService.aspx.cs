using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Client
{
    public partial class DisplayService : System.Web.UI.Page
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
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();
            

            //check is district selected
            String strSQL = "";

            if (!strDistrict.Equals(""))
            {
                strSQL = "AND s.DISTRICT = '" + strDistrict + "'";
            }

            //only display active servicer
            String dateNow = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
                
            con.Open();
            try
            {
                //Get Service Offered
                String strSelect = "SELECT * FROM SERVICE_OFFER s, SERVICER r WHERE s.servicer_id = r.servicer_id AND s.SERVICE_CATEGORY = @strServiceCat AND s.SERVICE_TYPE = @strServiceType AND s.STATE = @strState " + strSQL + " AND r.isActive = 'A' AND CONVERT(DATETIME, r.valid_date, 103) > CONVERT(DATETIME, @date_now, 103) ORDER BY r.collected_point DESC;";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);                
                cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                cmdSelect.Parameters.AddWithValue("@strState", strState);
                cmdSelect.Parameters.AddWithValue("@date_now", dateNow);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                if (dtrProd.HasRows)
                {
                    noItemLogo.Visible = false;
                    noItemText.Visible = false;
                    noItemLinkText.Visible = false;

                }
                else
                {
                    noItemLogo.Visible = true;
                    noItemText.Visible = true;
                    noItemLinkText.Visible = true;
                }
                displayServRepeater.DataSource = dtrProd;
                displayServRepeater.DataBind();
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



        protected void ddlSort_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strSearch = tbSearch.Text;
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();
            if (ddlSort.SelectedValue.Equals("LowToHighPrice"))
            {
                if (!strSearch.Equals(""))
                {
                    LowToHighPriceSearch();
                }
                else
                {
                    con.Open();
                    try
                    {
                        //Get Service Offered
                        String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict ORDER BY CAST (FEES AS DECIMAL(10,2)) ASC";
                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                        cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                        cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                        cmdSelect.Parameters.AddWithValue("@strState", strState);
                        cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                        SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                        displayServRepeater.DataSource = dtrProd;
                        displayServRepeater.DataBind();
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
            else if (ddlSort.SelectedValue.Equals("HighToLowPrice"))
            {

                if (!strSearch.Equals(""))
                {
                    HighToLowPriceSearch();
                }
                else
                {
                    con.Open();
                    try
                    {
                        //Get Service Offered
                        String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict ORDER BY CAST (FEES AS DECIMAL(10,2)) DESC";
                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                        cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                        cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                        cmdSelect.Parameters.AddWithValue("@strState", strState);
                        cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                        SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                        displayServRepeater.DataSource = dtrProd;
                        displayServRepeater.DataBind();
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
            else if (ddlSort.SelectedValue.Equals("Default"))
            {
                if (!strSearch.Equals(""))
                {
                    DefaultSearch();
                }
                else
                {
                    BindGrid();
                }
            }
        }

        protected void tbSearch_TextChanged(object sender, EventArgs e)
        {
            string strSearch = tbSearch.Text;
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();
            if (!strSearch.Equals(""))
            {
                if (ddlSort.SelectedValue.Equals("LowToHighPrice"))
                {
                    LowToHighPriceSearch();
                }
                else if (ddlSort.SelectedValue.Equals("HighToLowPrice"))
                {
                    HighToLowPriceSearch();
                }
                else if (ddlSort.SelectedValue.Equals("Default"))
                {
                    DefaultSearch();
                }



            }
            else if (strSearch.Equals(""))
            {
                con.Open();
                try
                {
                    //Get Service Offered
                    String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict";
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                    cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                    cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                    cmdSelect.Parameters.AddWithValue("@strState", strState);
                    cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                    cmdSelect.Parameters.AddWithValue("@strSearch", strSearch);
                    SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                    if (dtrProd.HasRows)
                    {
                        noItemLogo.Visible = false;
                        noItemText.Visible = false;
                        noItemLinkText.Visible = false;
                    }
                    else
                    {
                        noItemLogo.Visible = true;
                        noItemText.Visible = true;
                        noItemLinkText.Visible = true;
                    }
                    displayServRepeater.DataSource = dtrProd;
                    displayServRepeater.DataBind();
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

        private void LowToHighPriceSearch()
        {
            string strSearch = tbSearch.Text;
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();

            con.Open();
            try
            {
                //Get Service Offered
                String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict AND SERVICE_TITLE LIKE '%' + @strSearch + '%' ORDER BY CAST (FEES AS DECIMAL(10,2)) ASC";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                cmdSelect.Parameters.AddWithValue("@strState", strState);
                cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                cmdSelect.Parameters.AddWithValue("@strSearch", strSearch);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                if (dtrProd.HasRows)
                {
                    noItemLogo.Visible = false;
                    noItemText.Visible = false;
                    noItemLinkText.Visible = false;
                }
                else
                {
                    noItemLogo.Visible = true;
                    noItemText.Visible = true;
                    noItemLinkText.Visible = true;
                }
                displayServRepeater.DataSource = dtrProd;
                displayServRepeater.DataBind();
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

        private void HighToLowPriceSearch()
        {
            string strSearch = tbSearch.Text;
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();
            con.Open();
            try
            {
                //Get Service Offered
                String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict AND SERVICE_TITLE LIKE '%' + @strSearch + '%' ORDER BY CAST (FEES AS DECIMAL(10,2)) DESC";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                cmdSelect.Parameters.AddWithValue("@strState", strState);
                cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                cmdSelect.Parameters.AddWithValue("@strSearch", strSearch);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                if (dtrProd.HasRows)
                {
                    noItemLogo.Visible = false;
                    noItemText.Visible = false;
                    noItemLinkText.Visible = false;
                }
                else
                {
                    noItemLogo.Visible = true;
                    noItemText.Visible = true;
                    noItemLinkText.Visible = true;
                }
                displayServRepeater.DataSource = dtrProd;
                displayServRepeater.DataBind();
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
        private void DefaultSearch()
        {
            string strSearch = tbSearch.Text;
            string strServiceCat = Session["strServiceCat"].ToString();
            string strServiceType = Session["strServiceType"].ToString();
            string strState = Session["strState"].ToString();
            string strDistrict = Session["strDistrict"].ToString();

            con.Open();
            try
            {
                //Get Service Offered
                String strSelect = "SELECT * FROM SERVICE_OFFER WHERE SERVICE_CATEGORY = @strServiceCat AND SERVICE_TYPE = @strServiceType AND STATE = @strState AND DISTRICT = @strDistrict AND SERVICE_TITLE LIKE '%' + @strSearch + '%'";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);

                cmdSelect.Parameters.AddWithValue("@strServiceCat", strServiceCat);
                cmdSelect.Parameters.AddWithValue("@strServiceType", strServiceType);
                cmdSelect.Parameters.AddWithValue("@strState", strState);
                cmdSelect.Parameters.AddWithValue("@strDistrict", strDistrict);
                cmdSelect.Parameters.AddWithValue("@strSearch", strSearch);
                SqlDataReader dtrProd = cmdSelect.ExecuteReader();
                if (dtrProd.HasRows)
                {
                    noItemLogo.Visible = false;
                    noItemText.Visible = false;
                    noItemLinkText.Visible = false;
                }
                else
                {
                    noItemLogo.Visible = true;
                    noItemText.Visible = true;
                    noItemLinkText.Visible = true;
                }
                displayServRepeater.DataSource = dtrProd;
                displayServRepeater.DataBind();
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

        protected void btnViewMore_Click(object sender, EventArgs e)
        {
            
            LinkButton linkbutton = (sender as LinkButton);            
            RepeaterItem item = linkbutton.NamingContainer as RepeaterItem;            
            HiddenField selectedServiceOffer = (HiddenField)item.FindControl("HiddenField1");
           
            Session["selectedServOffer"] = selectedServiceOffer.Value;            
            Response.Redirect("~/Client/DisplayServiceDetails.aspx");
        }
    }
}

