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
    public partial class AnnualServiceAnalysisReportSetting : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Servicer";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
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


            //get servicer created date
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            DateTime createdDate = DateTime.Now;

            con.Open();
            try
            {

                //retrieve data
                String strAdd = "SELECT * FROM " + table + " WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    createdDate = Convert.ToDateTime(dataReader["created_date"].ToString());
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

            //count and generate the year(s) which from servicer created to now
            int currentYear = DateTime.Now.Year;
            int yearCount = (currentYear - createdDate.Year);


            for (int i = 0; i <= yearCount; i++)
            {
                ddlYear.Items.Add(new ListItem((currentYear - i).ToString(), (currentYear - i).ToString()));
            }
            
            
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            Session["selectedYear"] = ddlYear.SelectedValue;

            ClientScript.RegisterStartupScript(this.Page.GetType(), "", "window.open('AnnualServiceAnalysisReport.aspx');", true);

        }
    }
}