using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Models
{
    public class WxConfig
    {
        public static string WxAppId
        {
            get
            {
                return ConfigurationManager.AppSettings["WxAppId"];
            }
        }

        public static string WxSecret
        {
            get
            {
                return ConfigurationManager.AppSettings["WxSecret"];
            }
        }
        public static string WxHost
        {
            get
            {
                return ConfigurationManager.AppSettings["WxHost"];
            }
        }
        public static string WxGrantType
        {
            get
            {
                return ConfigurationManager.AppSettings["WxGrantType"];
            }
        }
    }
}
