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
    public partial class CustomerReviewReport : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Client_Review";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);
        protected void Page_Load(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            //get servicer name
            String servicerName = "";

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


            //get client review for the servicer
            con.Open();

            try
            {
                //retrieve data
                String strSelect = "SELECT Client_Review.review_id, Client_Review.date_and_time, Client_Review.rating_number, Client_Review.comment, Client_Review.is_Anonymous, Client.full_name FROM Client_Review INNER JOIN Client ON Client_Review.client_id = Client.client_id WHERE servicer_id = '" + strId + "';";
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                SqlDataAdapter sd = new SqlDataAdapter(cmdSelect);


                DataSet s = new DataSet();

                sd.Fill(s, "ReviewClient");

                ReportDocument r = new ReportDocument();
                r.Load(Server.MapPath("~/Reports/CustomerReviewReport.rpt"));
                r.SetDataSource(s);

                // Assign Paramters after set datasource
                r.SetParameterValue("ServicerId", strId);
                r.SetParameterValue("ServicerName", servicerName);
                crystalreportviewer1.ReportSource = r;
                r.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, false, "Customer Review Report");

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