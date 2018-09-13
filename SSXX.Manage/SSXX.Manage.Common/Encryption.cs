using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace SSXX.Manage.Common
{
    public class Encryption
    {
        public static string GetEncryption(string content, string Type = "MD5")
        {
            if (Type != "MD5")
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(content, "SHA1");
            }
            return FormsAuthentication.HashPasswordForStoringInConfigFile(content, "MD5");
        }
    }
}
