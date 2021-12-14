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
    public partial class AnnualServiceAnalysisReport : System.Web.UI.Page
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

            String selectedYear = Session["selectedYear"].ToString();


            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT r.request_no, r.servicer_id, r.date_and_time, r.state, r.district, r.amount_charges, r.status, r.service_category, r.service_type, s.full_name FROM Service_Request As r LEFT JOIN Servicer As s ON r.servicer_id = s.servicer_id WHERE (r.status = 'C' OR r.status = 'D') AND (CONVERT(DATETIME, r.date_and_time, 103) >= CONVERT(DATETIME, '01/01/" + selectedYear + "', 103) AND CONVERT(DATETIME, r.date_and_time, 103) < CONVERT(DATETIME, '31/12/" + selectedYear + "', 103) ) AND r.servicer_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s);

                //create report
                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/AnnualServiceAnalysisReport.rpt"));
                r.SetDataSource(s.Tables["table"]);
                // Assign Paramters after set datasource
                r.SetParameterValue("ServicerId", strId);
                r.SetParameterValue("ServicerName", servicerName);
                r.SetParameterValue("SelectedYear", selectedYear);
                crystalreportviewer1.ReportSource = r;
                r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Annual Service Analysis Report");

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