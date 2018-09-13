using SSXX.Manage.BLL;
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
    public partial class SSXXRedirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int wxType = CRequest.GetInt("type");
            string wxReurl = CRequest.GetString("reurl");
            string wxCode = CRequest.GetString("code");
            if (!Page.IsPostBack)
            {
                if(wxCode != "")
                {
                    //获取全局token
                    if (!WxLoginBLL.SaveXMLByApplactionToken())
                    {
                        Log.WriterLog("获取全局token失败");
                    }
                    WxAccessToken wx = WxLoginBLL.GetToken(wxCode);
                    if (wx.openid != null)
                    {
                        WxUserInfo userInfo = WxLoginBLL.GetWxUserInfo(wx, wxCode);
                        if (userInfo.openid != null)
                        {
                            object obj;
                            WxUserInfoBLL uill = new WxUserInfoBLL();
                            uill.CheckUserInfo(userInfo);
                            List<WxUserInfo> users = uill.QueryUserInfoByID(wx.openid);
                            if (users != null && users.Count > 0)
                            {
                                string openId = CTools.SetOpenId(users[0].openid);
                                obj = new
                                {
                                    userid = users[0].u_id,
                                    openid = openId,
                                    token = CTools.GetMD5FromString(users[0].u_id + openId + "SSXXCJYXGS")
                                };
                                Session["UserInfo"] = users[0];
                                Cookies.SetCookie("UserInfo",JsonHelper.ObjToJsonString<object>(obj), 90);
                                Response.Redirect(WxConfig.WxHost);
                            }
                        }else
                        {
                            Response.Write("获取用户信息失败");
                        }
                    }
                    else
                    {
                        Response.Write("获取用户token失败");
                    }
                }
            }
        }
    }
}