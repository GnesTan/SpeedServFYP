using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class UserTypeRatioAnalysisReport : System.Web.UI.Page
    {
        String table = "Subscription_Request";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
        {
            String strUsername = "";

            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }

            int clientCount = 0;
            int servicerCount = 0;


            con.Open();

            try
            {
                //retrieve data
                String strSelect = "SELECT COUNT(*) FROM Client;";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                clientCount = (int)cmdSelect.ExecuteScalar();

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

            con.Open();

            try
            {
                //retrieve data
                String strSelect = "SELECT COUNT(*) FROM Servicer;";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                servicerCount = (int)cmdSelect.ExecuteScalar();

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

            // Create a new DataTable.
            System.Data.DataTable table = new DataTable("UserType");
            table.Columns.Add("user_type", typeof(string));
            table.Columns.Add("count", typeof(int));

            DataRow workRow = table.NewRow();

            workRow["user_type"] = "Client";
            workRow["count"] = clientCount;

            table.Rows.Add(workRow);

            DataRow workRow2 = table.NewRow();

            workRow2["user_type"] = "Servicer";
            workRow2["count"] = servicerCount;

            table.Rows.Add(workRow2);

            DataSet s = new DataSet();

            s.Tables.Add(table);


            ReportDocument r = new ReportDocument();
            r.Load(Server.MapPath("~/Reports/UserTypeRatioAnalysisReport.rpt"));
            r.SetDataSource(s);
            // Assign Paramters after set datasource
            r.SetParameterValue("Username", strUsername);
            crystalreportviewer1.ReportSource = r;
            r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "User Type Ratio Analysis Report");



        }
    }
}