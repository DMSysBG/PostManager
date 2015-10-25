using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PostManager
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

         /// <summary>  
        /// Application AcquireRequestState  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            string cultureCode = "bg-BG";

            //Default Language/Culture for all number, Date format  
            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Globalization.CultureInfo.CreateSpecificCulture(cultureCode);

            //Ui Culture for Localized text in the UI  
            System.Threading.Thread.CurrentThread.CurrentUICulture =
                new System.Globalization.CultureInfo(cultureCode);
        }
    }
}
