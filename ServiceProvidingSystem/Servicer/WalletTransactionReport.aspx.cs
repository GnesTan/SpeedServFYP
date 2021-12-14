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
    public partial class WalletTransactionReport : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Payment";

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
            Double creditBalance = 0.00;

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
                    servicerName = dataReader["full_name"].ToString();
                    creditBalance = Convert.ToDouble(dataReader["credit_balance"]);
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



            con.Open();
            try
            {
                //retrieve data
                String strSelect = "SELECT payment_id, date_and_time, servicer_amount, remark FROM " + table + " WHERE servicer_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s);

                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/WalletTransactionReport.rpt"));
                r.SetDataSource(s.Tables["table"]);
                // Assign Paramters after set datasource
                r.SetParameterValue("ServicerId", strId);
                r.SetParameterValue("ServicerName", servicerName);
                r.SetParameterValue("CreditBalance", creditBalance);
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