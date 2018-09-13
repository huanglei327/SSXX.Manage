using SSXX.Manage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SSXX.Manage.BLL;
using SSXX.Manage.Models;
using System.Web;

namespace SSXX.Manage.Web.Controllers.WxControolers
{
    public class WxUserInfoController : ApiController
    {
        public object QueryById([FromBody]Newtonsoft.Json.Linq.JObject obj)
        {

            //WxUserInfo userinfo = (WxUserInfo)HttpContext.Current.Session["userInfo"];
            //Log.WriterLog(userinfo.openid+"=========12313123");
            if (obj["userid"] == null || obj["userid"].ToString() == "")
            {
                return ResponseStr.ToJsonError("参数错误");
            }
            WxUserInfoBLL bll = new WxUserInfoBLL();
            List<WxUserInfo> users =  bll.QueryUserInfoByID(int.Parse(obj["userid"].ToString()));
            if (users != null && users.Count > 0)
            {
                return ResponseStr.ToJsonTrue(users[0]);
            }
            else
            {
                return ResponseStr.ToJsonError("查不到用户信息");
            }
        }
    }
}
