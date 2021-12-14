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
    public partial class SalesTransactionReport : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
        {
            //get username and from/to date
            String strUsername = "";

            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }

            String strFromDate = Convert.ToDateTime(Session["dtFromDate"]).ToString("dd/MM/yyyy");

            DateTime dtToDate = Convert.ToDateTime(Session["dtToDate"]);

            String strToDate = dtToDate.ToString("dd/MM/yyyy");

            dtToDate = dtToDate.AddDays(1);

            String strToDateEx = dtToDate.ToString("dd/MM/yyyy");

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Service_Request WHERE status = 'D' AND (CONVERT(DATETIME, date_and_time, 103) >= CONVERT(DATETIME, @from_date, 103) AND CONVERT(DATETIME, date_and_time, 103) < CONVERT(DATETIME, @to_date, 103) );";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@from_date", strFromDate);
                cmdSelect.Parameters.AddWithValue("@to_date", strToDateEx);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s);

                //create report
                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/SalesTransactionReport.rpt"));
                r.SetDataSource(s.Tables["table"]);
                // Assign Paramters after set datasource
                r.SetParameterValue("Username", strUsername);
                r.SetParameterValue("FromDate", strFromDate);
                r.SetParameterValue("ToDate", strToDate);
                crystalreportviewer1.ReportSource = r;
                r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Sales Transaction Report");

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
}