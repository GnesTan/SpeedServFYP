using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ServiceProvidingSystem
{
    public partial class RegisterServicer : System.Web.UI.Page
    {

        //String table = "Artist";

        //static String str = ConfigurationManager.ConnectionStrings["JojoArtworkDB"].ConnectionString;

        //SqlConnection con = new SqlConnection(str);

        protected void Page_PreRender(object sender, EventArgs e)
        {
            //DateValidator.MinimumValue = DateTime.Now.Date.AddYears(-100).ToString("yyyy/MM/dd");
            //DateValidator.MaximumValue = DateTime.Now.Date.AddYears(-18).ToString("yyyy/MM/dd");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }



    }
}