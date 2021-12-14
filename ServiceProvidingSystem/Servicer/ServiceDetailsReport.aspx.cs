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


namespace ServiceProvidingSystem.Servicer
{
    public partial class ServiceDetailsReport : System.Web.UI.Page
    {
        //setup SQL connection
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            String servicerName = "";

            con.Open();
            try
            {
                //retrieve data
                String strAdd = "SELECT * FROM Servicer WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);
                SqlDataReader dataReader;
                dataReader = cmdAdd.ExecuteReader();
                while (dataReader.Read())
                {
                    servicerName = dataReader["full_name"].ToString();
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
                String strSelect = "SELECT r.request_no, r.servicer_id, r.date_and_time, r.state, r.district, r.amount_charges, r.status, c.full_name FROM Service_Request As r LEFT JOIN Client As c ON r.client_id = c.client_id WHERE (r.status = 'C' OR r.status = 'D') AND (CONVERT(DATETIME, date_and_time, 103) >= CONVERT(DATETIME, @from_date, 103) AND CONVERT(DATETIME, date_and_time, 103) < CONVERT(DATETIME, @to_date, 103) ) AND r.servicer_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                cmdSelect.Parameters.AddWithValue("@from_date", strFromDate);
                cmdSelect.Parameters.AddWithValue("@to_date", strToDateEx);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s);

                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/ServiceDetailsReport.rpt"));
                r.SetDataSource(s.Tables["table"]);
                // Assign Paramters after set datasource
                r.SetParameterValue("ServicerId", strId);
                r.SetParameterValue("ServicerName", servicerName);
                r.SetParameterValue("FromDate", strFromDate);
                r.SetParameterValue("ToDate", strToDate);
                crystalreportviewer1.ReportSource = r;
                r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Service Details Report");

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