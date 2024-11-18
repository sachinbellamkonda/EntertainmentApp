using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace EntertainmentApp
{
    public class Global : HttpApplication
    {
        //void Application_Start(object sender, EventArgs e)
        //{
        //    // Code that runs on application startup
        //    RouteConfig.RegisterRoutes(RouteTable.Routes);
        //    BundleConfig.RegisterBundles(BundleTable.Bundles);
        //}
        protected void Application_Start(object sender, EventArgs e)
        {
            // Initialize the visit counter
            Application["VisitCounter"] = 0;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Increment the counter on every new request
            Application.Lock(); // Ensure thread safety
            Application["VisitCounter"] = (int)Application["VisitCounter"] + 1;
            Application.UnLock();
        }

    }
}