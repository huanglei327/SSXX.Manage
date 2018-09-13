using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SSXX.Manage.Common
{
   public class CRequest
    {

        public static string CheckQueryString(string query, string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return string.Empty;
            }
            if ((query.Equals("backurl") || query.Equals("saveurl")) || ((query.Equals("fw") || query.Equals("tourl")) || query.Equals("to")))
            {
                result = HttpUtility.UrlDecode(result);
                if (!CheckUrl(result))
                {
                    result = "非法的Url:" + result;
                    return result;
                }
                result = HttpUtility.UrlEncode(result);
            }
            return result;
        }

        private static string GetCommon(string query, string value, bool IsForm, bool IsDecode)
        {
            if (HttpContext.Current.Request == null)
            {
                return value;
            }
            if (IsForm && string.IsNullOrEmpty(HttpContext.Current.Request.Form[query]))
            {
                return value;
            }
            string result = "";
            if (HttpContext.Current.Request[query] == null)
            {
                if (GetUrlString(query, value, out result))
                {
                    return result;
                }
                return value;
            }
            result = HttpContext.Current.Request[query];
            if (result == "")
            {
                return value;
            }
            result = CheckQueryString(query, result);
            if (IsDecode)
            {
                result = UrlDecode(result);
            }
            return result;
        }

        private static string GetCommonValue(string query, string value, bool IsForm, bool IsDecode)
        {
            return GetValue(GetCommon(query, value, IsForm, IsDecode));
        }

        public static int GetInt(string query)
        {
            return GetInt(query, 0);
        }

        public static int GetInt(string query, int ndefault)
        {
            return CTools.ToInt(GetCommonValue(query, ndefault.ToString(), false, false));
        }

        public static long GetInt64(string query)
        {
            return GetInt64(query, 0L);
        }

        public static long GetInt64(string query, long ldefault)
        {
            return CTools.ToInt64(GetCommonValue(query, ldefault.ToString(), false, false));
        }

        public static long GetInt64Form(string query)
        {
            return GetInt64Form(query, 0L);
        }

        public static long GetInt64Form(string query, long ldefault)
        {
            return CTools.ToInt64(GetCommonValue(query, ldefault.ToString(), true, false));
        }

        public static int GetIntForm(string query)
        {
            return GetIntForm(query, 0);
        }

        public static int GetIntForm(string query, int ndefault)
        {
            return CTools.ToInt(GetCommonValue(query, ndefault.ToString(), true, false));
        }

        public static int GetInts(string query)
        {
            return CTools.ToInt(GetValue2(GetCommon(query, "", false, false)));
        }

        public static string GetString(string query)
        {
            return GetString(query, "");
        }

        public static string GetString(string query, bool IsDecode)
        {
            return GetCommon(query, "", false, IsDecode);
        }

        public static string GetString(string query, string strdefault)
        {
            return GetCommon(query, strdefault, false, true);
        }

        public static string GetStringForm(string query)
        {
            return GetStringForm(query, "");
        }

        public static string GetStringForm(string query, string strdefault)
        {
            return GetCommon(query, strdefault, true, false);
        }

        public static string GetStrings(string query)
        {
            return GetStrings(query, true);
        }

        public static string GetStrings(string query, bool IsDeCode)
        {
            return GetValue2(GetCommon(query, "", false, IsDeCode));
        }

        public static bool GetUrlString(string query, string value, out string result)
        {
            result = "";
            if (string.IsNullOrEmpty(query))
            {
                return false;
            }
            HttpRequest request = HttpContext.Current.Request;
            if (request == null)
            {
                return false;
            }
            if (request[query] != null)
            {
                return false;
            }
            Uri uri = new Uri(request.Url.AbsoluteUri);
            string s = UrlDecode(uri.Query.Replace("?", "&") + "&");
            string str3 = s.ToLower();
            string str4 = query.ToLower();
            if (!str3.Contains("&amp;"))
            {
                return false;
            }
            s = HttpUtility.HtmlDecode(s);
            if (!str3.Contains(str4))
            {
                return false;
            }
            result = Match(s, "&" + query + "=[^&]*", "(&" + query + "=|&)");
            result = CheckQueryString(query, result);
            return true;
        }

        public static bool GetUrlString(string url, string query, string value, out string result)
        {
            result = "";
            if (string.IsNullOrEmpty(query))
            {
                return false;
            }
            HttpRequest request = HttpContext.Current.Request;
            Uri uri = new Uri(url);
            string s = UrlDecode(uri.Query.Replace("?", "&") + "&");
            if (!s.Contains(query))
            {
                return false;
            }
            s = HttpUtility.HtmlDecode(s);
            result = Match(s, "&" + query + "=[^&]*", "(&" + query + "=|&)");
            result = CheckQueryString(query, result);
            return true;
        }

        private static string GetValue(string value)
        {
            return ReplaceValue(value, ",");
        }

        private static string GetValue2(string value)
        {
            value = ReplaceValue(value, ",");
            value = ReplaceValue(value, "?");
            return value;
        }

        private static string ReplaceValue(string value, string key)
        {
            int index = value.IndexOf(key, StringComparison.Ordinal);
            if (index > 0)
            {
                return value.Substring(0, index);
            }
            if (index == 0)
            {
                return "";
            }
            return value;
        }

        public static string UrlDecode(string url)
        {
            return UrlDecode(url, null);
        }

        public static string UrlDecode(string url, Encoding encoding)
        {
            if (encoding == null)
            {
                Encoding e = Encoding.UTF8;
                string str2 = HttpUtility.UrlEncode(HttpUtility.UrlDecode(url.ToUpper(), e), e).ToUpper();
                if (url == str2)
                {
                    encoding = Encoding.UTF8;
                }
                else
                {
                    encoding = Encoding.GetEncoding("gb2312");
                }
            }
            return HttpUtility.UrlDecode(url, encoding);
        }

        private static string Match(string content, string strReg, string replace)
        {
            string input = "";
            Regex regex = new Regex(strReg, RegexOptions.IgnoreCase);
            input = regex.Match(content).Value;
            if ((replace != null) && (replace != ""))
            {
                input = Regex.Replace(input, replace, "", RegexOptions.IgnoreCase);
            }
            return input;
        }
        private static bool CheckUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return true;
            }
            url = HttpUtility.UrlDecode(url);
            url = url.ToLower();
            if (url.Contains(@"\"))
            {
                url = url.Replace(@"\", "/");
            }
            if ((!url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("//", StringComparison.OrdinalIgnoreCase)) && !url.StartsWith(@"\\", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }


            return false;
        }



    }
}
