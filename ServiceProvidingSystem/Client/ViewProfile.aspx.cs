using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Client.ClientManagement
{
    public partial class ViewProfile : System.Web.UI.Page
    {

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!Session["userType"].ToString().Equals("Client"))
                {
                    Response.Redirect("~/Login.aspx", true);

                }
            }



            if (Session["strId"] != null)
            {
                BindGrid();

            }


        }
        private void BindGrid()
        {
            String strId = Session["strId"].ToString();
            String strProfilePicture = "";
            String strName = "";
            String strIc = "";
            String strDob = "";
            String strGender = "";
            String strContact = "";
            String strEmail = "";
            String strAddress = "";
            int countTotalPayment = 0;
            int countServiceNeeded = 0;
            int rewardPoints = 0;
            char currentRank = ' ';

            con.Open();

            //Retreive data
            String strAdd = "SELECT * FROM CLIENT WHERE CLIENT_ID = @strId;";
            SqlCommand cmdAdd = new SqlCommand(strAdd, con);
            cmdAdd.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dataReader;
            dataReader = cmdAdd.ExecuteReader();
            while (dataReader.Read())
            {
                strProfilePicture = dataReader["profile_picture"].ToString();
                strName = dataReader["full_name"].ToString();
                strIc = dataReader["identity_no"].ToString();
                strDob = dataReader["date_of_birth"].ToString();
                strGender = dataReader["gender"].ToString();
                strContact = dataReader["contact_no"].ToString();
                strEmail = dataReader["email_address"].ToString();
                strAddress = dataReader["home_address"].ToString();
                currentRank = Convert.ToChar(dataReader["client_rank"].ToString());
                rewardPoints = Convert.ToInt32(dataReader["reward_points"].ToString());
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
            }
            con.Close();


            con.Open();
            //Retreive data
            String strCount = "SELECT COUNT(*) FROM PAYMENT WHERE CLIENT_ID = @strId;";
            SqlCommand cmdCount = new SqlCommand(strCount, con);
            cmdCount.Parameters.AddWithValue("@strId", strId);
            SqlDataReader dtrCount;
            dtrCount = cmdCount.ExecuteReader();
            while (dtrCount.Read())
            {
                countTotalPayment = Convert.ToInt32(dtrCount[0].ToString());
            }
            con.Close();
            //Display Data
            lblDpName.Text = strName;
            lblDpDob.Text = strDob;
            lblDpIC.Text = strIc;
            lblDpGender.Text = strGender;
            lblDpPhoneNo.Text = strContact;
            lblDpEmail.Text = strEmail;
            lblDpAddress.Text = strAddress;
            lblDpCurrentPoints.Text = rewardPoints.ToString() + " pts";
            lblDpServComplete.Text = countTotalPayment.ToString();
            //Display Image
            if (!strProfilePicture.Equals(""))
            {
                imgProfile.ImageUrl = strProfilePicture;
            }
            else
            {
                imgProfile.ImageUrl = "/Image/generaluser.png";
            }
            //Display Current Rank
            if (currentRank == 'B')
            {
                imgRank.ImageUrl = "../Image/bronze-rank.png";
                lblDpRank.ForeColor = System.Drawing.Color.FromArgb(205, 133, 63);
                lblDpRank.Text = "Bronze";
                lblDpMaxService.Text = "2";
                countServiceNeeded = 5 - countTotalPayment;
                lblDpRequestNeeded.Text = countServiceNeeded.ToString();
            }
            else if (currentRank == 'S')
            {
                imgRank.ImageUrl = "../Image/silver-rank.png";
                lblDpRank.ForeColor = System.Drawing.Color.FromArgb(192, 192, 192);
                lblDpRank.Text = "Silver";
                lblDpMaxService.Text = "3";
                countServiceNeeded = 10 - countTotalPayment;
                lblDpRequestNeeded.Text = countServiceNeeded.ToString();
            }
            else if (currentRank == 'G')
            {
                imgRank.ImageUrl = "../Image/gold-rank.png";
                lblDpRank.ForeColor = System.Drawing.Color.FromArgb(251, 209, 0);
                lblDpRank.Text = "Gold";
                lblDpMaxService.Text = "5";
                lblDpRequestNeeded.Visible = false;
                lblDisText.Visible = false;
            }

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ResetPassword.aspx");
        }
        protected void btnEditProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/EditProfile.aspx");
        }
    }
}