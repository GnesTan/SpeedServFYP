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
    public partial class ServicerViewProfile : System.Web.UI.Page
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

            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }

            String strProfilePicture = "";
            String strName = "";
            String strDob = "";
            String strGender = "";
            String strContact = "";
            String strEmail = "";
            String strAddress = "";
            String strWorkingDays = "";
            String strAvailableTime = "";
            String strIsActive = "";
            String strIC = "";


            //retrieve current servicer information
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
                    strProfilePicture = dataReader["profile_picture"].ToString();
                    strName = dataReader["full_name"].ToString();
                    strDob = dataReader["date_of_birth"].ToString();
                    strGender = dataReader["gender"].ToString();
                    strContact = dataReader["contact_no"].ToString();
                    strEmail = dataReader["email_address"].ToString();
                    strAddress = dataReader["home_address"].ToString();
                    strWorkingDays = dataReader["working_days"].ToString();
                    strAvailableTime = dataReader["available_time"].ToString();
                    strIC = dataReader["identity_no"].ToString();
                    if (strGender != "" && strGender != null)
                    {
                        if (strGender.Equals("M"))
                        {
                            strGender = "Male";
                        }
                        else if (strGender.Equals("F"))
                        {
                            strGender = "Female";
                        }
                        else
                        {
                            strGender = "Other";
                        }

                    }
                    strIsActive = dataReader["isActive"].ToString();
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

            //set information to appropriate textfield
            lblDpName.Text = strName;
            lblDpDob.Text = strDob;
            lblDpGender.Text = strGender;
            lblDpPhoneNo.Text = strContact;
            lblDpEmail.Text = strEmail;
            lblDpAddress.Text = strAddress;
            lblDpDays.Text = strWorkingDays;
            lblDpIC.Text = strIC;
            if (strAvailableTime != null && !strAvailableTime.Equals("") && strAvailableTime.Length == 11)
            {
                lblDpTime.Text = "From " + strAvailableTime.Substring(0, 5) + " To " + strAvailableTime.Substring(6, 5);
            }

            if (!strProfilePicture.Equals(""))
            {
                imgProfile.ImageUrl = strProfilePicture;
            }
            else
            {
                imgProfile.ImageUrl = "/Image/generaluser.png";
            }


            if (strIsActive.Equals("D"))
            {
                lbDeactivate.Visible = false;
                lbActivate.Visible = true;
            }


        }

        //set servicer status to deactivate
        protected void lbDeactivate_Click(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }





            con.Open();

            try
            {


                //update data
                String strAdd = "UPDATE " + table + " SET isActive = 'D' WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);

                cmdAdd.ExecuteNonQuery();

                con.Close();





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

            Response.Redirect(Request.RawUrl);

        }

        //set servicer status to activate
        protected void lbActivate_Click(object sender, EventArgs e)
        {
            String strId = "";

            if (Session["strId"] != null)
            {
                strId = Session["strId"].ToString();
            }


            con.Open();

            try
            {


                //update data
                String strAdd = "UPDATE " + table + " SET isActive = 'A' WHERE servicer_id = @servicer_id;";

                SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                cmdAdd.Parameters.AddWithValue("@servicer_id", strId);

                cmdAdd.ExecuteNonQuery();

                con.Close();



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

            //refresh page
            Response.Redirect(Request.RawUrl);

        }







    }
}