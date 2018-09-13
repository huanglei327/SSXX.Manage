using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Common
{
    public class TempList
    {
        public static void SendTempletMessge(string access_token, string openid,string type)
        {
            try
            {
                string url = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token=" + access_token;

                string temp = TempList.GetTmep(type, openid);
                //核心代码
                RequestHttp.HttpWxPushMsg(temp, url);
            }
            catch (Exception ex)
            {
                Log.WriterLog(ex.Source, "关注推送失败" + ex.Message);
            }
        }

        public static string GetTmep(string type,string openid)
        {
            string temp = string.Empty;
            switch (type)
            {
                case "GUANZHU":
                    temp = "{\"touser\": \"" + openid + "\"," +
                     "\"template_id\": \"fD1_IFNGALjYRMXJryX3j9t-WjLVbp28duWFPOaOxQ8\", " +
                      "\"url\": \"http://duoduoday.top\", " +
                     "\"topcolor\": \"#FF0000\", " +
                     "\"data\": " +
                     "{\"first\": {\"value\": \"感谢登陆\"}," +
                     "\"keyword1\": { \"value\": \"单位名称\"}," +
                     "\"keyword2\": { \"value\": \"日期\"}," +
                     "\"remark\": {\"value\": \"恭喜发财\" }}}";
                    break;
                default:
                    break;
            }
            return temp;
        }
    }
}
