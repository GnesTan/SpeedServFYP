using System;
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


    public partial class PostService : System.Web.UI.Page
    {
        String table = "";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_ddlstate();

                txtRemarks.TextMode = TextBoxMode.MultiLine;
                txtRemarks.Rows = 10;
            }
        }

        public void Bind_ddlstate()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select state_name, state_id FROM state", con);
            SqlDataReader dr = cmd.ExecuteReader();
            ddlstate.DataSource = dr;
            ddlstate.Items.Clear();
            ddlstate.Items.Add("--Please Select state--");
            ddlstate.DataTextField = "state_name";
            ddlstate.DataValueField = "state_id";
            ddlstate.DataBind();
            
            
            

            con.Close();
        }
        public void Bind_ddldistrict()
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("select district_name, district_id FROM district WHERE state_id ='" + ddlstate.SelectedValue + "'", con);

            SqlDataReader dr = cmd.ExecuteReader();
            ddldistrict.DataSource = dr;
            ddldistrict.Items.Clear();
            ddldistrict.Items.Add("--Please Select district--");
            ddldistrict.DataTextField = "district_name";
            ddldistrict.DataValueField = "district_id";
            ddldistrict.DataBind();
            con.Close();
            

        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlstate.SelectedValue.Equals("--Please Select state--"))
            {
                Bind_ddldistrict();
            }
            else
            {
                ddldistrict.Items.Clear();
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

        protected void btnPost_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/MyService.aspx");
        }
    }
}