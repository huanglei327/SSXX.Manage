using SSXX.Manage.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SSXX.Manage.Web.AllControllers
{
    public class TokenProjectorAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //如果用户方位的Action带有AllowAnonymousAttribute，则不进行授权验证
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }
            string userid =  CRequest.GetString("userid");
            string token = CRequest.GetString("token");
            string openid = CRequest.GetString("openid");

            if(userid == "" || token =="" || openid == "")
            {
                actionContext.Response = ResponseStr.ToJsonError("9", "未登陆");
            }
            else
            {
                Log.WriterLog(userid + "--" + token + "--" + openid);
                openid = CTools.GetOpenId(openid);
                
                if (!token.Equals(CTools.GetMD5FromString(userid + openid + "SSXXCJYXGS")))
                {
                    actionContext.Response = ResponseStr.ToJsonError("9", "非法请求");
                }
            }
        }
    }
}