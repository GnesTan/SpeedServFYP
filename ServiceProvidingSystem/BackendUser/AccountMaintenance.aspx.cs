using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ServiceProvidingSystem.BackendUser
{
    public partial class AccountMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {

                String userType = "";

                if (Session["userType"] != null)
                {
                    userType = Session["userType"].ToString();
                }


                if (!userType.Equals("Backend"))
                {
                    Response.Redirect("~/StaffLogin.aspx");
                }

                this.BindGrid();
            }


            
        }
        private void BindGrid()
        {
            SqlConnection con;
            String str = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;
            con = new SqlConnection(str);
            con.Open();
            //retrieve data
            String strSelect = "SELECT * FROM Back_end_User";
            SqlCommand cmdSelect = new SqlCommand(strSelect, con);

            SqlDataReader dtrProd = cmdSelect.ExecuteReader();


            Repeater1.DataSource = dtrProd;
            Repeater1.DataBind();
            
            con.Close();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedUsername = (Label)item.FindControl("lblUsername");

            //save the selectedUsername to redirect to the edit page
            Session["selectedUsername"] = selectedUsername.Text;

            Response.Redirect("~/BackendUser/EditAccount.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //Get the reference of the clicked button.
            Button button = (sender as Button);


            //Get the Repeater Item reference
            RepeaterItem item = button.NamingContainer as RepeaterItem;


            //Find the label control
            Label selectedUsername = (Label)item.FindControl("lblUsername");

            //declare String for the select account's username
            String username = selectedUsername.Text;

            String strUsername = "";

            //get currently login username
            if (Session["strUsername"] != null)
            {
                strUsername = Session["strUsername"].ToString();
            }

            //check whether the deleting account is same with the currently login account
            if(username.Equals(strUsername))
            {
                //prompt error message
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unable to delete account which currently login." + "');", true);
            }
            else
            {
                //check whether user try to delete admin account
                if (username.Equals("admin"))
                {
                    //prompt error message
                    ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + "Unable to delete Admin account." + "');", true);
                }
                else
                {
                    //execute delete query to delete selected account
                    string query = "DELETE FROM Back_end_User WHERE USERNAME = @USERNAME";
                    string constr = ConfigurationManager.ConnectionStrings["SpeedServDB"].ConnectionString;
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        using (SqlCommand cmd = new SqlCommand(query))
                        {
                            cmd.Parameters.AddWithValue("@USERNAME", username);
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }

                    this.BindGrid();
                }
 
            }

        }


    }
}