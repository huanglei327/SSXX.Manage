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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //string url = "{\"access_token\":\"13_LqvXe2-iOC2RZ1mmtpbxJiGXYL_0gN2ujY56ZWIrwASiEqwtjN1RpmWCcbY6RraDiHPL316QNccTeRER5t5ZobX-Q3ATC-6kk-H830fO4imZAERf9lMESDH-PC0SIQhADAVLT\",\"expires_in\":7200}";
                //ApplicationAccessToken acctoken = JsonHelper.JsonStringToObj<ApplicationAccessToken>(url);
                //object obj = new
                //{
                //    userid = "1",
                //    openid = "123",
                //    token = CTools.GetMD5FromString("1" + "123" + "SSXXCJYXGS")
                //};
                //Cookies.SetCookie(JsonHelper.ObjToJsonString<object>(obj), "UserInfo", 90);
                string openId = "o1ED6w0aGe-n7UCWPdMiywT4dM5E";
                string begin = openId.Substring(0, 5);
                string end = openId.Substring(5, openId.Length-5);
                string randStr = CTools.RandLetter(5);
                Response.Write(openId +"<br/>");
                Response.Write("-------"+randStr + "-------<br/>");
                Response.Write(begin + randStr + end);
            }
        }
    }
}