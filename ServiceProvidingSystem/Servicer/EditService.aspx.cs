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
    public partial class EditService : System.Web.UI.Page
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

                String selectedOfferNo = "";

                if (Session["selectedOfferNo"] != null)
                {
                    selectedOfferNo = Session["selectedOfferNo"].ToString();
                }


                String strCategory = "";
                String strType = "";
                String strTitle = "";
                String strRemark = "";
                String strPic = "";
                String strDeliveryFee = "";
                String strFees = "";
                String strState = "";
                String strDistrict = "";

                con.Open();

                try
                {

                    //retrieve data
                    String strSelect = "SELECT * FROM  " + table + " WHERE offer_id = @offer_id;";

                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    cmdSelect.Parameters.AddWithValue("@offer_id", selectedOfferNo);
                    SqlDataReader dataReader;
                    dataReader = cmdSelect.ExecuteReader();
                    while (dataReader.Read())
                    {
                        strCategory = dataReader["service_category"].ToString();
                        strType = dataReader["service_type"].ToString();
                        strTitle = dataReader["service_title"].ToString();
                        strRemark = dataReader["remark"].ToString();
                        strPic = dataReader["service_picture"].ToString();
                        strDeliveryFee = dataReader["delivery_fee"].ToString();
                        strFees = dataReader["fees"].ToString();
                        strState = dataReader["state"].ToString();
                        strDistrict = dataReader["district"].ToString();
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

                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(strCategory));
           
                if (ddlCategory.SelectedValue.Equals("Installation"))
                {
                    ddlInstallType.Visible = true;
                    ddlRepairType.Visible = false;
                    ddlOtherType.Visible = false;
                    ddlInstallType.SelectedIndex = ddlInstallType.Items.IndexOf(ddlInstallType.Items.FindByText(strType));
                }
                else if (ddlCategory.SelectedValue.Equals("Repairing"))
                {
                    ddlInstallType.Visible = false;
                    ddlRepairType.Visible = true;
                    ddlOtherType.Visible = false;
                    ddlRepairType.SelectedIndex = ddlRepairType.Items.IndexOf(ddlRepairType.Items.FindByText(strType));
                }
                else if (ddlCategory.SelectedValue.Equals("Others"))
                {
                    ddlInstallType.Visible = false;
                    ddlRepairType.Visible = false;
                    ddlOtherType.Visible = true;
                    ddlOtherType.SelectedIndex = ddlOtherType.Items.IndexOf(ddlOtherType.Items.FindByText(strType));
                }

                txtServiceTitle.Text = strTitle;
                txtRemarks.Text = strRemark;
                ReferencePic.ImageUrl = strPic;
                txtTransport.Text = strDeliveryFee;
                txtFees.Text = strFees;
                ddlstate.SelectedIndex = ddlstate.Items.IndexOf(ddlstate.Items.FindByText(strState));

                //bind for district dropdown list
                Bind_ddldistrict();

                if (!strDistrict.Equals(""))
                {

                    ddldistrict.SelectedIndex = ddldistrict.Items.IndexOf(ddldistrict.Items.FindByText(strDistrict));
                }
                



            }
        }

        //bind state from database
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
                con.Close();

                if (ddldistrict.Items.Count == 1)
                {
                    ddldistrict.Items.Clear();
                    ddldistrict.Items.Add(new ListItem("", ""));
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

        protected void btnSave_Click(object sender, EventArgs e)
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
                String strServicePic = ReferencePic.ImageUrl;
                if (ImageUpload.HasFile)
                {
                    //Get next number for IMG naming
                    int newImgId = 0;

                    con.Open();

                    try
                    {

                        //retrieve data
                        String strSelect = "SELECT * FROM Sequence WHERE sequence_id = 'default';";

                        SqlCommand cmdAdd = new SqlCommand(strSelect, con);
                        SqlDataReader dataReader;
                        dataReader = cmdAdd.ExecuteReader();
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

                    String selectedOfferNo = "";

                    if (Session["selectedOfferNo"] != null)
                    {
                        selectedOfferNo = Session["selectedOfferNo"].ToString();
                    }


                    con.Open();

                    try
                    {

                        //create client account
                        String strUpdate = "UPDATE " + table + " SET service_category = @service_category, service_type = @service_type, service_title = @service_title, remark = @remark, service_picture = @service_picture, delivery_fee = @delivery_fee, fees = @fees, state = @state, district = @district WHERE offer_id = @offer_id;";

                        SqlCommand cmdUpdate = new SqlCommand(strUpdate, con);


                        cmdUpdate.Parameters.AddWithValue("@service_category", strServiceCategory);
                        cmdUpdate.Parameters.AddWithValue("@service_type", strServiceType);
                        cmdUpdate.Parameters.AddWithValue("@service_title", strTitle);
                        cmdUpdate.Parameters.AddWithValue("@remark", strRemark);
                        cmdUpdate.Parameters.AddWithValue("@service_picture", strServicePic);
                        cmdUpdate.Parameters.AddWithValue("@delivery_fee", strTransport);
                        cmdUpdate.Parameters.AddWithValue("@fees", strCharges);
                        cmdUpdate.Parameters.AddWithValue("@state", strState);
                        cmdUpdate.Parameters.AddWithValue("@district", strDistrict);
                        cmdUpdate.Parameters.AddWithValue("@offer_id", selectedOfferNo);

                        cmdUpdate.ExecuteNonQuery();

                        con.Close();

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








                    //disp;ay popup message and redirect
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Service have successful saved.');window.location.href='MyService.aspx';", true);

            }
            else
            {
                lblError.Text = "*Please select State and City for your service.";
            }

        }



        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Servicer/MyService.aspx");
        }
    }
}