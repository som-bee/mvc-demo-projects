using MVCApp_Bundling_Minification.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCApp_Bundling_Minification
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundle(BundleTable.Bundles);

        }
    }
}
