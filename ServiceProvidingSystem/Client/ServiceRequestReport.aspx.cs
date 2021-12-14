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

namespace ServiceProvidingSystem.Client
{
    public partial class ServiceRequestReport : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            String clientName = "";

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT * FROM Client WHERE client_id = @client_id;";

                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@client_id", strId);
                SqlDataReader dataReader;
                dataReader = cmdSelect.ExecuteReader();
                while (dataReader.Read())
                {
                    clientName = dataReader["full_name"].ToString();
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

            String strFromDate = Convert.ToDateTime(Session["dtFromDate"]).ToString("dd/MM/yyyy");

            DateTime dtToDate = Convert.ToDateTime(Session["dtToDate"]);

            String strToDate = dtToDate.ToString("dd/MM/yyyy");

            dtToDate = dtToDate.AddDays(1);

            String strToDateEx = dtToDate.ToString("dd/MM/yyyy");

            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT r.request_no, r.date_and_time, r.service_category, r.service_type, r.state, r.district, r.title, r.amount_charges, r.status, r.client_id, r.servicer_id, s.full_name FROM Service_Request As r LEFT JOIN Servicer As s ON r.servicer_id = s.servicer_id WHERE (r.status = 'D' OR r.status = 'C') AND (CONVERT(DATETIME, r.date_and_time, 103) >= CONVERT(DATETIME, @from_date, 103) AND CONVERT(DATETIME, r.date_and_time, 103) < CONVERT(DATETIME, @to_date, 103) ) AND r.client_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@from_date", strFromDate);
                cmdSelect.Parameters.AddWithValue("@to_date", strToDateEx);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s, "RequestReport");

                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/ServiceRequestReport.rpt"));
                r.SetDataSource(s);

                // Assign Paramters after set datasource
                r.SetParameterValue("ClientId", strId);
                r.SetParameterValue("ClientName", clientName);
                r.SetParameterValue("FromDate", strFromDate);
                r.SetParameterValue("ToDate", strToDate);
                crystalreportviewer1.ReportSource = r;
                r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Request Report");

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