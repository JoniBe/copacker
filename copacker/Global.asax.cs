using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using copacker;

namespace copacker
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private CopackerSeguridad db = new CopackerSeguridad();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            

        }
        
    }
}
