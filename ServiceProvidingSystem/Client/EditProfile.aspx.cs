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

namespace ServiceProvidingSystem.Client.ClientManagement
{
    public partial class EditProfile : System.Web.UI.Page
    {
        static String str = ConfigurationManager.ConnectionStrings["SpeedServAzureDB"].ConnectionString;
        SqlConnection con = new SqlConnection(str);
        protected void Page_PreRender(object sender, EventArgs e)
        {
            DateValidator.MinimumValue = DateTime.Now.Date.AddYears(-100).ToString("yyyy/MM/dd");
            DateValidator.MaximumValue = DateTime.Now.Date.AddYears(-18).ToString("yyyy/MM/dd");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        private void BindGrid()
        {
            String strId = Session["strId"].ToString();
            String strProfilePicture = "";
            String strName = "";
            String strIc = "";
            DateTime dtDob = DateTime.Now;
            String strGender = "";
            String strContact = "";
            String strEmail = "";
            String strAddress = "";
            
            con.Open();
            try
            {
                //retrieve data
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
                    dtDob = DateTime.ParseExact(dataReader["date_of_birth"].ToString(), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                    strGender = dataReader["gender"].ToString();
                    strContact = dataReader["contact_no"].ToString();
                    strEmail = dataReader["email_address"].ToString();
                    strAddress = dataReader["home_address"].ToString();
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

            //Display data
            txtName.Text = strName;
            txtIc.Text = strIc;
            txtPhone.Text = strContact;
            txtDob.Text = dtDob.ToString("yyyy-MM-dd");
            ddlGender.SelectedValue = strGender;
            txtEmail.Text = strEmail;
            txtAddress.Text = strAddress;
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ViewProfile.aspx");
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
                    SqlCommand check_servicer_phone = new SqlCommand("SELECT COUNT(*) FROM Servicer WHERE (contact_no = @contact_no)", con);
                    check_servicer_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
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
                    SqlCommand check_client_phone = new SqlCommand("SELECT COUNT(*) FROM Client WHERE (contact_no = @contact_no) AND (client_id != @client_id)", con);
                    check_client_phone.Parameters.AddWithValue("@contact_no", txtPhone.Text);
                    check_client_phone.Parameters.AddWithValue("@client_id", strId);
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
                    String strIc = "";
                    String strDob = "";
                    String strGender = "";
                    String strContact = "";
                    String strEmail = "";
                    String strAddress = "";

                    strName = txtName.Text;
                    strIc = txtIc.Text;
                    DateTime dtDob = DateTime.ParseExact(txtDob.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                    strDob = dtDob.ToString("dd/MM/yyyy");
                    strContact = txtPhone.Text;
                    strGender = ddlGender.SelectedValue;
                    strEmail = txtEmail.Text;
                    strAddress = txtAddress.Text;
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
                        String strAdd = "UPDATE CLIENT SET FULL_NAME = @full_name, DATE_OF_BIRTH = @date_of_birth, CONTACT_NO = @contact_no, GENDER = @gender, EMAIL_ADDRESS = @email_address, HOME_ADDRESS = @home_address, PROFILE_PICTURE = @profile_picture WHERE CLIENT_ID = @strId;";

                        SqlCommand cmdAdd = new SqlCommand(strAdd, con);
                        cmdAdd.Parameters.AddWithValue("@full_name", strName);
                        cmdAdd.Parameters.AddWithValue("@date_of_birth", strDob);
                        cmdAdd.Parameters.AddWithValue("@contact_no", strContact);
                        cmdAdd.Parameters.AddWithValue("@gender", char.Parse(strGender));
                        cmdAdd.Parameters.AddWithValue("@email_address", strEmail);
                        cmdAdd.Parameters.AddWithValue("@home_address", strAddress);
                        cmdAdd.Parameters.AddWithValue("@profile_picture", strProfilePicture);
                        cmdAdd.Parameters.AddWithValue("@strId", strId);

                        cmdAdd.ExecuteNonQuery();

                        con.Close();

                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Sucessfully Saved." + "');window.location.href='ViewProfile.aspx';", true);

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