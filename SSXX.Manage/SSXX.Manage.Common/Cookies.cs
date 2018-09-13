using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSXX.Manage.Common
{
    public class Cookies
    {
        /// <summary>
        /// 获取cookie值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string getCookie(string key, string value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            try
            {
                if (cookie != null)
                {
                    return HttpUtility.UrlDecode(cookie.Values[value]);
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string getCookie(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            if (cookie != null)
            {
                return cookie.Value.ToString();
            }
            return "";
        }


        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strValue"></param>
        /// <param name="strDay"></param>
        /// <returns></returns>
        public static bool SetCookie(string strName, string strValue, int strDay)
        {
            try
            {
                DelCookie(strName);
                HttpCookie cookie = new HttpCookie(strName)
                {
                    Expires = DateTime.Now.AddDays((double)strDay),
                    Value = strValue
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
            }
            return false;
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static bool DelCookie(string strName)
        {
            try
            {
                HttpCookie cookie = new HttpCookie(strName)
                {
                    Expires = DateTime.Now.AddDays(-1.0)
                };
                HttpContext.Current.Response.Cookies.Add(cookie);
                return true;
            }
            catch
            {
            }
            return false;
        }
    }
}
