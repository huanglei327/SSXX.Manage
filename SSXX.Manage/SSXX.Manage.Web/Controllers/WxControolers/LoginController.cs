using SSXX.Manage.Common;
using SSXX.Manage.Models;
using SSXX.Manage.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using System.Web;

namespace SSXX.Manage.Web.Controllers.WxControolers
{
    public class LoginController : ApiController
    {
        [AllowAnonymousAttribute]
        public string WxLogin([FromBody]JObject obj)
        {
            HttpContext.Current.Session["userInfo"] = "123456";

            string value = HttpContext.Current.Session["userInfo"].ToString();

            string asd = "222";
            return asd;
            //Log.WriterLog(obj.ToString());
            //if (obj["code"] == null || obj["code"].ToString() == "")
            //{
            //    return ResponseStr.ToJsonError("参数错误");
            //}
            ////获取全局token
            //if (!WxLoginBLL.SaveXMLByApplactionToken())
            //{
            //    return ResponseStr.ToJsonError("登陆失败");
            //}

            //WxAccessToken wx = WxLoginBLL.GetToken(obj["code"].ToString());

            //WxUserInfo userInfo = WxLoginBLL.GetWxUserInfo(wx, obj["code"].ToString());
            //if (wx.openid != null)
            //{
            //    WxUserInfoBLL uill = new WxUserInfoBLL();
            //    uill.CheckUserInfo(userInfo);
            //    if (!uill.UserInfoExists(userInfo))
            //    {
            //        ApplicationAccessToken appToken = WxLoginBLL.GetApplicationToken();
            //        TempList.SendTempletMessge(appToken.access_token, wx.openid, "guanzhu");
            //    }
            //    return ResponseStr.ToJsonTrue(new
            //    {
            //        result =  wx.openid
            //    });
            //}
            //else
            //{
            //    Log.WriterLog("获取用户token失败");
            //    return ResponseStr.ToJsonError("服务器错误");
            //}
        }
    }
}
