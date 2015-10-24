using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostManager
{
    public class xConfig
    {
        public static string ConnectionString
        {
            get
            { return System.Configuration.ConfigurationManager.ConnectionStrings["posts"].ConnectionString; }
        }        
    }
}