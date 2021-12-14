using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ServiceProvidingSystem
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["NoOfErrors"] = 0;
            Application["ErrorMessage"] = "";
            Application["ErrorCode"] = "";
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Application.Lock();
            Application["NoOfErrors"] = (int)Application["NoOfErrors"] + 1;
            Exception ex = Server.GetLastError();
            HttpException httpException = (HttpException)ex;
            int httpCode = 404;
            if (httpException != null)
            {
                httpCode = httpException.GetHttpCode();
            }

            if (ex != null)
            {
                String message = ex.Message;
                Application["ErrorMessage"] = message;
                Application["ErrorCode"] = httpCode;
            }
            Application.UnLock();

            Server.ClearError();

            Response.Redirect("~/ErrorPage.aspx");
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}