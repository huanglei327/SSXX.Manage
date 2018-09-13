using Newtonsoft.Json.Linq;
using SSXX.Manage.BLL;
using SSXX.Manage.Common;
using SSXX.Manage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SSXX.Manage.Web.Controllers
{
    public class NotiFicationController : ApiController
    {

        public object Push([FromBody]JObject obj)
        {
            //ApplicationAccessToken appToken = WxLoginBLL.GetApplicationToken();
            //TempList.SendTempletMessge(appToken.access_token, wx.openid, "guanzhu");
            return null;
        }
    }
}
