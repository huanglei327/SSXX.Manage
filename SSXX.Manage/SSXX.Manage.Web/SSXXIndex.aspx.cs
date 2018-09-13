using SSXX.Manage.Common;
using SSXX.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSXX.Manage.Web
{
    public partial class SSXXIndex : System.Web.UI.Page
    {
        public string appid = WxConfig.WxAppId;  //公众微信平台下可以找到  
        public string host = WxConfig.WxHost;
        protected void Page_Load(object sender, EventArgs e)
        {
            string auth = CRequest.GetString("auth");
            int type = CRequest.GetInt("type");
            if (!IsPostBack && auth == "1")
            {
                string reurl = "";
                //传递参数，获取用户信息后，可跳转到自己定义的页面，想怎么处理就怎么处理  
                if (Request.QueryString["reurl"] != null && Request.QueryString["reurl"] != "")
                {
                    reurl = Request.QueryString["reurl"].ToString();
                }
                else
                {
                    reurl = host + "/html/custom/custom_join.html";
                }

                //弹出授权页面(如在不弹出授权页面基础下未获得openid，则弹出授权页面，提示用户授权)  
                if (Request.QueryString["auth"] != null && Request.QueryString["auth"] != "" && Request.QueryString["auth"] == "1")
                {
                    Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + Server.UrlEncode(host + "/SSXXRedirect.aspx?type=" + type + "&reurl=" + reurl) + "&response_type=code&scope=snsapi_userinfo&state=1#wechat_redirect");
                }
                else
                {
                    //不弹出授权页面  
                    Response.Redirect("https://open.weixin.qq.com/connect/oauth2/authorize?appid=" + appid + "&redirect_uri=" + Server.UrlEncode(host + "/wechat/SSXXRedirect.aspx?type=" + type + "&reurl=" + reurl) + "&response_type=code&scope=snsapi_base&state=1#wechat_redirect");
                }
            }
        }
    }
}