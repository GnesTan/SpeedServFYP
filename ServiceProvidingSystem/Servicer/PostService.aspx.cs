using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.Servicer
{


    public partial class PostService : System.Web.UI.Page
    {
        //setup SQL connection
        String table = "Service_Offer";

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


                Bind_ddlstate();

                txtRemarks.TextMode = TextBoxMode.MultiLine;
                txtRemarks.Rows = 10;
            }
        }

        public void Bind_ddlstate()
        {
            con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("select state_name, state_id FROM state", con);
                SqlDataReader dr = cmd.ExecuteReader();
                ddlstate.DataSource = dr;
                ddlstate.Items.Clear();
                ddlstate.Items.Add("--Please Select State--");
                ddlstate.DataTextField = "state_name";
                ddlstate.DataValueField = "state_id";
                ddlstate.DataBind();


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
        public void Bind_ddldistrict()
        {
            con.Open();
            try
            {

                SqlCommand cmd = new SqlCommand("select district_name, district_id FROM district WHERE state_id ='" + ddlstate.SelectedValue + "'", con);

                SqlDataReader dr = cmd.ExecuteReader();
                ddldistrict.DataSource = dr;
                ddldistrict.Items.Clear();
                ddldistrict.Items.Add("--Please Select District--");
                ddldistrict.DataTextField = "district_name";
                ddldistrict.DataValueField = "district_id";
                ddldistrict.DataBind();
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

            if (ddldistrict.Items.Count == 1)
            {
                ddldistrict.Items.Clear();
                ddldistrict.Items.Add(new ListItem("", ""));
            }
            

        }

        protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlstate.SelectedValue.Equals("--Please Select State--"))
            {
                Bind_ddldistrict();
            }
            else
            {
                ddldistrict.Items.Clear();
                ddldistrict.Items.Add("--Please Select District--");
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCategory.SelectedValue.Equals("Installation"))
            {
                ddlInstallType.Visible = true;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Repairing"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = true;
                ddlOtherType.Visible = false;
            }
            else if (ddlCategory.SelectedValue.Equals("Others"))
            {
                ddlInstallType.Visible = false;
                ddlRepairType.Visible = false;
                ddlOtherType.Visible = true;
            }
        }

        protected void btnPost_Click(object sender, EventArgs e)
        {
            if (!ddlstate.SelectedItem.Text.Equals("--Please Select State--") && !ddldistrict.SelectedItem.Text.Equals("--Please Select District--"))
            {


                String strServiceCategory = ddlCategory.SelectedValue;
                String strServiceType = "";
                if (ddlCategory.SelectedValue.Equals("Installation"))
                {
                    strServiceType = ddlInstallType.SelectedValue;
                }
                else if (ddlCategory.SelectedValue.Equals("Repairing"))
                {
                    strServiceType = ddlRepairType.SelectedValue;
                }
                else if (ddlCategory.SelectedValue.Equals("Others"))
                {
                    strServiceType = ddlOtherType.SelectedValue;
                }
                String strTitle = txtServiceTitle.Text;
                String strRemark = txtRemarks.Text;
                String strTransport = txtTransport.Text;
                String strCharges = txtFees.Text;
                String strState = ddlstate.SelectedItem.Text;
                String strDistrict = ddldistrict.SelectedItem.Text;
                String strId = Session["strId"].ToString();
                String strServicePic = "../Image/noImage.png";
                if (ImageUpload.HasFile)
                {
                    //Get next number for IMG naming
                    int newImgId = 0;

                    con.Open();
                    try
                    {

                        //retrieve data
                        String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        SqlDataReader dataReader;
                        dataReader = cmdSelect.ExecuteReader();
                        while (dataReader.Read())
                        {
                            newImgId = (int)dataReader["img_sequence"];
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
                        strServicePic = "/Image/" + strImgName;

                        ReferencePic.ImageUrl = strServicePic;

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

                //Get next number for service offer
                int newServiceOfferId = 0;

                try
                {

                    con.Open();

                    //retrieve data
                    String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    SqlDataReader dataReader2 = cmdSelect.ExecuteReader();
                    while (dataReader2.Read())
                    {
                        newServiceOfferId = (int)dataReader2["offer_sequence"];
                    }

                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        String exMessage = ex.Message;
                        Application["ErrorMessage"] = exMessage;
                    }
                    Application["ErrorCode"] = " ";
                    Response.Redirect("~/ErrorPage.aspx");
                }
                finally
                {
                    con.Close();
                }

                //create new service offer number
                String strServiceOfferId = "SO" + newServiceOfferId.ToString("D" + 8);

                con.Open();

                try
                {

                    //create offer record
                    String strAdd = "INSERT INTO " + table + " (offer_id, service_category, service_type, service_title, remark, service_picture, delivery_fee, fees, servicer_id, state, district) VALUES (@offer_id, @service_category, @service_type, @service_title, @remark, @service_picture, @delivery_fee, @fees, @servicer_id, @state, @district);";

                    SqlCommand cmdAdd = new SqlCommand(strAdd, con);

                    cmdAdd.Parameters.AddWithValue("@offer_id", strServiceOfferId);
                    cmdAdd.Parameters.AddWithValue("@service_category", strServiceCategory);
                    cmdAdd.Parameters.AddWithValue("@service_type", strServiceType);
                    cmdAdd.Parameters.AddWithValue("@service_title", strTitle);
                    cmdAdd.Parameters.AddWithValue("@remark", strRemark);
                    cmdAdd.Parameters.AddWithValue("@service_picture", strServicePic);
                    cmdAdd.Parameters.AddWithValue("@delivery_fee", strTransport);
                    cmdAdd.Parameters.AddWithValue("@fees", strCharges);
                    cmdAdd.Parameters.AddWithValue("@servicer_id", strId);
                    cmdAdd.Parameters.AddWithValue("@state", strState);
                    cmdAdd.Parameters.AddWithValue("@district", strDistrict);

                    cmdAdd.ExecuteNonQuery();



                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        String exMessage = ex.Message;
                        Application["ErrorMessage"] = exMessage;
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

                    //update payment sequence id
                    String strUpdate = "UPDATE Sequence SET offer_sequence = @offer_sequence WHERE sequence_id = 'default'";

                    SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);

                    cmdUpdate.Parameters.AddWithValue("@offer_sequence", newServiceOfferId + 1);

                    cmdUpdate.ExecuteNonQuery();


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




                //display popup message and redirect
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Service have successful posted.');window.location.href='MyService.aspx';", true);

            }
            else
            {
                //display error message
                lblError.Text = "*Please select State and City to post your service.";
            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/MyService.aspx");
        }
    }
}