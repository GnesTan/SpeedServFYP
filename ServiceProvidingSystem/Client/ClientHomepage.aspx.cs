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
    public partial class ClientHomePage : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_ddlState();
            }
        }

        protected void liRequest_Click(object sender, EventArgs e)
        {
            String userType = "";
            if (Session["userType"] != null)
            {
                userType = Session["userType"].ToString();
            }

            if (!userType.Equals("Client"))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Please Login before request service'); window.location='" + Request.ApplicationPath + "Login.aspx';", true);
            }

            if (Session["strId"] != null)
            {
                Response.Redirect("~/Client/PostServiceRequest.aspx");
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                ddlInstallType.Visible = true;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = true;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = true;
            }
        }


        public void Bind_ddlState()
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select state_name, state_id FROM state", con);
                SqlDataReader dr = cmd.ExecuteReader();
                ddlState.DataSource = dr;
                ddlState.Items.Clear();
                ddlState.Items.Add("--Please Select State--");
                ddlState.DataTextField = "state_name";
                ddlState.DataValueField = "state_id";
                ddlState.DataBind();
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

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlState.SelectedValue.Equals("--Please Select State--"))
            {
                Bind_ddlDistrict();
            }
            else
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add("--Please Select District--");
            }
        }

        public void Bind_ddlDistrict()
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select district_name, district_id FROM district WHERE state_id ='" + ddlState.SelectedValue + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                ddlDistrict.DataSource = dr;
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add("--Please Select District--");
                ddlDistrict.DataTextField = "district_name";
                ddlDistrict.DataValueField = "district_id";
                ddlDistrict.DataBind();
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

            if (ddlDistrict.Items.Count == 1)
            {
                ddlDistrict.Items.Clear();
                ddlDistrict.Items.Add(new ListItem("", ""));
            }

        }

        protected void btnClientSignUp_Click(object sender, EventArgs e)
        {
            Session["RegisterUser"] = "Client";
            Response.Redirect("../Register.aspx");
        }

        protected void btnServicerSignUp_Click(object sender, EventArgs e)
        {
            Session["RegisterUser"] = "Servicer";
            Response.Redirect("../Register.aspx");
        }

        protected void btnFindNow_Click(object sender, EventArgs e)
        {
            Session["strServiceCat"] = ddlCategory.SelectedValue;
            Session["strState"] = ddlState.SelectedItem.Text;
            Session["strDistrict"] = ddlDistrict.SelectedItem.Text;

            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                Session["strServiceType"] = ddlInstallType.SelectedValue;
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                Session["strServiceType"] = ddlRepairType.SelectedValue;
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                Session["strServiceType"] = ddlOtherType.SelectedValue;
            }

            if (!Session["strDistrict"].Equals("--Please Select District--") && !Session["strState"].Equals("--Please Select State--"))
            {
                Response.Redirect("~/Client/DisplayService.aspx");
            }
            else
            {
                lblError.Text = "Please select a State and City";

            }

        }
    }
}