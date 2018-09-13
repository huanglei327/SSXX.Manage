using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSXX.Manage.Models;
using SSXX.Manage.Common;
using System.IO;
using System.Xml;
using System.Web;

namespace SSXX.Manage.BLL
{
    public class WxLoginBLL
    {

        /// <summary>
        /// 获取全局token
        /// </summary>
        /// <returns></returns>
        public static string GetApplicationAccessToken()
        {
            string url = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + WxConfig.WxAppId + "&secret=" + WxConfig.WxSecret;
            return RequestHttp.HttpGet(url);
        }

        /// <summary>
        /// 获取微信用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static WxUserInfo GetWxUserInfo(WxAccessToken token,string code)
        {
            string url = "https://api.weixin.qq.com/sns/userinfo?access_token=" + token.access_token + "&openid=" + token.openid;
            WxUserInfo userInfo = JsonHelper.JsonStringToObj<WxUserInfo>(RequestHttp.HttpGet(url));
            return userInfo;
        }

        /// <summary>
        /// 获取用户token
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public  static WxAccessToken GetToken(string code)
        {
            string url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=" + WxConfig.WxAppId + "&secret=" + WxConfig.WxSecret + "&code=" + code + "&grant_type=authorization_code";
            string value = RequestHttp.HttpGet(url);
            return JsonHelper.JsonStringToObj<WxAccessToken>(value);
        }

        public static  ApplicationAccessToken GetApplicationToken()
        {
            string token = string.Empty;
            DateTime time;
            string path = HttpContext.Current.Server.MapPath("~/AccessToken.xml");
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);
            XmlDocument xd = new XmlDocument();
            xd.Load(sr);
            sr.Close();
            sr.Dispose();
            token = xd.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            time = Convert.ToDateTime(xd.SelectSingleNode("xml").SelectSingleNode("Access_Time").InnerText);
            ApplicationAccessToken appToken = new ApplicationAccessToken();
            appToken.access_token = token;
            return appToken;
        }

        /// <summary>
        /// 全局token保存到xml文件里
        /// </summary>
        /// <returns></returns>
        public static Boolean SaveXMLByApplactionToken()
        {
            string token = string.Empty;
            DateTime time;
            string path = HttpContext.Current.Server.MapPath("~/AccessToken.xml");
            StreamReader sr = new StreamReader(path, System.Text.Encoding.UTF8);
            XmlDocument xd = new XmlDocument();
            xd.Load(sr);
            sr.Close();
            sr.Dispose();
            token = xd.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText;
            time = Convert.ToDateTime(xd.SelectSingleNode("xml").SelectSingleNode("Access_Time").InnerText);
            if (DateTime.Now > time)
            {
                string accessToken = WxLoginBLL.GetApplicationAccessToken();
                Log.WriterLog("获取全局token"+ accessToken);
                ApplicationAccessToken acctoken = JsonHelper.JsonStringToObj<ApplicationAccessToken>(accessToken);
                if (acctoken.access_token != null && acctoken.access_token != "")
                {
                    DateTime _youxrq = DateTime.Now;
                    xd.SelectSingleNode("xml").SelectSingleNode("Access_Token").InnerText = acctoken.access_token;
                    _youxrq = _youxrq.AddSeconds(int.Parse(acctoken.expires_in));
                    xd.SelectSingleNode("xml").SelectSingleNode("Access_Time").InnerText = _youxrq.ToString();
                    try
                    {
                        xd.Save(path);
                    }catch(Exception e)
                    {
                        Log.WriterLog(e.Source, e.Message);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
