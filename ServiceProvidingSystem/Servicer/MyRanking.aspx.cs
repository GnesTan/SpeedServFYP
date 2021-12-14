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
    public partial class MyRanking : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Servicer";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);




        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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


                //retrieve current login ID
                String strId = "";


                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                int intAvailable = 0;
                int intCollected = 0;
                int intToNextRank = 0;


                //check from admin database
                con.Open();
                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM " + table + " WHERE servicer_id = @servicer_id;";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@servicer_id", strId);
                    SqlDataReader dataReader;
                    dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        intAvailable = (int)dataReader["available_points"];
                        intCollected = (int)dataReader["collected_point"];
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

                if (intCollected < 2000)
                {
                    lblRank.Text = "Bronze";
                    imgIcon.ImageUrl = "~/Image/Bronze.png";
                    intToNextRank = 2000 - intCollected;
                }
                else if(intCollected >= 2000 && intCollected < 4000)
                {
                    lblRank.Text = "Silver";
                    imgIcon.ImageUrl = "~/Image/Silver.png";
                    intToNextRank = 4000 - intCollected;
                }
                else
                {
                    lblRank.Text = "Gold";
                    imgIcon.ImageUrl = "~/Image/Gold.png";
                }


                lblRetreCollectedPoint.Text = intCollected.ToString();
                lblRetreId.Text = strId;
                lblRetrePoint.Text = intAvailable.ToString();
                lblRetreNextRank.Text = intToNextRank.ToString();



            }

        }

        protected void lbCollect_Click(object sender, EventArgs e)
        {
            int intCollected = 0;

            intCollected = Convert.ToInt32(lblRetreCollectedPoint.Text);

            int intAvailable = 0;

            intAvailable = Convert.ToInt32(lblRetrePoint.Text);

            intCollected = intCollected + intAvailable;


            con.Open();

            try
            {

                //retrieve current login ID
                String strId = "";


                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }



                //update points
                String strUpdate = "UPDATE " + table + " SET available_points = 0, collected_point = @collected_point WHERE servicer_id = @servicer_id;";

                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);
                cmdUpdate.Parameters.AddWithValue("@collected_point", intCollected);
                cmdUpdate.Parameters.AddWithValue("@servicer_id", strId);

                cmdUpdate.ExecuteNonQuery();

                con.Close();

                //prompt error message
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Points have successfully collected." + "');window.location.href='MyRanking.aspx';", true);


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