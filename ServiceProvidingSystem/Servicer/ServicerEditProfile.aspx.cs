using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace ServiceProvidingSystem.Servicer
{
    public partial class ServicerEditProfile : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Servicer";

        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;

        SqlConnection con = new SqlConnection(str);

        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateValidator.MinimumValue = DateTime.Now.Date.AddYears(-100).ToString("yyyy/MM/dd");
            DateValidator.MaximumValue = DateTime.Now.Date.AddYears(-18).ToString("yyyy/MM/dd");
        }
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

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                String strProfilePicture = "";
                String strName = "";
                DateTime dtDob = DateTime.Now;
                String strContact = "";
                String strGender = "";
                String strEmail = "";
                String strAddress = "";
                String strWorkingDays = "";
                String strAvailableTime = "";

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
                        if (dataReader["profile_picture"] != DBNull.Value)
                        {
                            strProfilePicture = dataReader["profile_picture"].ToString();
                        }
                        strName = dataReader["full_name"].ToString();
                        strContact = dataReader["contact_no"].ToString();
                        dtDob = DateTime.ParseExact(dataReader["date_of_birth"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        strGender = dataReader["gender"].ToString();
                        strEmail = dataReader["email_address"].ToString();
                        strAddress = dataReader["home_address"].ToString();
                        strWorkingDays = dataReader["working_days"].ToString();
                        strAvailableTime = dataReader["available_time"].ToString();

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

                //use substring to get time accurately
                String strFromHour = "";
                String strFromMin = "";
                String strToHour = "";
                String strToMin = "";

                if (!strAvailableTime.Equals(""))
                {
                    strFromHour = strAvailableTime.Substring(0, 2);
                    strFromMin = strAvailableTime.Substring(3, 2);
                    strToHour = strAvailableTime.Substring(6, 2);
                    strToMin = strAvailableTime.Substring(9, 2);

                }


                txtName.Text = strName;
                txtPhone.Text = strContact;
                txtDob.Text = dtDob.ToString("yyyy-MM-dd");
                ddlGender.SelectedValue = strGender;
                txtEmail.Text = strEmail;
                txtAddress.Text = strAddress;
                txtDays.Text = strWorkingDays;
                txtFromHour.Text = strFromHour;
                txtFromMin.Text = strFromMin;
                txtToHour.Text = strToHour;
                txtToMin.Text = strToMin;
                if (!strProfilePicture.Equals(""))
                {
                    imgProfile.ImageUrl = strProfilePicture;
                }
                else
                {
                    imgProfile.ImageUrl = "/Image/generaluser.png";
                }

                Session["strProfilePicture"] = strProfilePicture;

            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/ServicerViewProfile.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DateTime dateValue = default(DateTime);
            if (DateTime.TryParse(txtDob.Text, out dateValue))
            {

                String strId = "";

                if (Session["strId"] != null)
                {
                    strId = Session["strId"].ToString();
                }

                //check duplicate phone number
                int phoneExist = 0;

                con.Open();
                try
                {
                    //retrieve data
                    SqlCommand check_servicer_phone = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (contact_no = @contact_no) AND (servicer_id != @servicer_id)", con);
                    check_servicer_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
                    check_servicer_phone.Parameters.AddWithValue("@servicer_id", strId);
                    phoneExist += (int)check_servicer_phone.ExecuteScalar();

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
                    SqlCommand check_client_phone = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (contact_no = @contact_no)", con);
                    check_client_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
                    phoneExist += (int)check_client_phone.ExecuteScalar();

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

                if (phoneExist == 0)
                {
                    String strProfilePicture = "";
                    String strName = "";
                    String strDob = "";
                    String strContact = "";
                    String strGender = "";
                    String strEmail = "";
                    String strAddress = "";
                    String strWorkingDays = "";
                    String strAvailableTime = "";

                    strName = txtName.Text;
                    DateTime dtDob = DateTime.ParseExact(txtDob.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    strDob = dtDob.ToString("dd/MM/yyyy");
                    strContact = txtPhone.Text;
                    strGender = ddlGender.SelectedValue;
                    strEmail = txtEmail.Text;
                    strAddress = txtAddress.Text;
                    strWorkingDays = txtDays.Text;
                    if (!txtFromHour.Text.Equals("") && !txtFromMin.Text.Equals("") && !txtToHour.Text.Equals("") && !txtToMin.Text.Equals(""))
                    {
                        strAvailableTime = int.Parse(txtFromHour.Text).ToString("D" + 2) + ":" + int.Parse(txtFromMin.Text).ToString("D" + 2) + "|" + int.Parse(txtToHour.Text).ToString("D" + 2) + ":" + int.Parse(txtToMin.Text).ToString("D" + 2);
                    }
                    strProfilePicture = Session["strProfilePicture"].ToString();
                    if (ImageUpload.HasFile)
                    {
                        //Get next number for IMG naming
                        int newImgId = 0;

                        con.Open();

                        //retrieve data
                        String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        SqlDataReader dataReader;
                        dataReader = cmdSelect.ExecuteReader();
                        while (dataReader.Read())
                        {
                            newImgId = (int)dataReader["img_sequence"];
                        }

                        con.Close();

                        String strImgName = "I" + newImgId.ToString("D" + 8);


                        string folderPath = Server.MapPath("~/Image/");

                        if (ImageUpload.PostedFile.FileName.Length > 0)
                        {
                            //Check whether Directory (Folder) exists.
                            if (!Directory.Exists(folderPath))
                            {
                                //If Directory (Folder) does not exists Create it.
                                Directory.CreateDirectory(folderPath);
                            }

                            //Get extension
                            String extension = System.IO.Path.GetExtension(ImageUpload.PostedFile.FileName);

                            strImgName += extension;

                            //Save the File to the Directory (Folder).
                            ImageUpload.SaveAs(folderPath + strImgName);
                            strProfilePicture = "/Image/" + strImgName;

                            imgProfile.ImageUrl = strProfilePicture;

                            con.Open();

                            try
                            {

                                //update image sequence id
                                String strUpdate = "UPDATE Sequence SET img_sequence = @img_sequence WHERE sequence_id = 'default'";

                                SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                                cmdUpdate.Parameters.AddWithValue("@img_sequence", newImgId + 1);

                                cmdUpdate.ExecuteNonQuery();

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



                        }
                    }





                    con.Open();

                    try
                    {


                        //update data
                        String strAdd = "UPDATE " + table + " SET full_name = @full_name, date_of_birth = @date_of_birth, contact_no = @contact_no, gender = @gender, email_address = @email_address, home_address = @home_address, profile_picture = @profile_picture, working_days = @working_days, available_time = @available_time WHERE servicer_id = @servicer_id;";

                        SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                        cmdAdd.Parameters.AddWithValue("@full_name", strName);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", strDob);
                        cmdAdd.Parameters.AddWithValue("@contact_no", strContact);
                        cmdAdd.Parameters.AddWithValue("@gender", char.Parse(strGender));
                        cmdAdd.Parameters.AddWithValue("@email_address", strEmail);
                        cmdAdd.Parameters.AddWithValue("@home_address", strAddress);
                        cmdAdd.Parameters.AddWithValue("@profile_picture", strProfilePicture);
                        cmdAdd.Parameters.AddWithValue("@working_days", strWorkingDays);
                        cmdAdd.Parameters.AddWithValue("@available_time", strAvailableTime);
                        cmdAdd.Parameters.AddWithValue("@servicer_id", strId);

                        cmdAdd.ExecuteNonQuery();

                        con.Close();



                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Sucessfully Saved." + "');window.location.href='ServicerViewProfile.aspx';", true);

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
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to save! The phone number already registered by another user." + "');", true);
                }


            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Failed to save! Invalid date input for Date Of Birth, please check and try again." + "');", true);
            }







        }

    }
}