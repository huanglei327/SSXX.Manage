using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SSXX.Manage.Common
{
    public class ResponseStr
    {

        public static HttpResponseMessage ToJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                str = serializer.Serialize(obj);
            }
            return new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
        }

        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">返回提示的文案</param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonError(string msg)
        {
            return ResponseStr.ToJson(new
            {
                errorCode = 1,
                errorMessage = msg,
            });
        }
        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="msg">返回提示的文案</param>
        /// <param name="e">记录日志</param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonError(string msg, Exception e)
        {
            return ResponseStr.ToJson(new
            {
                errorCode = 1,
                errorMessage = msg,
            });
        }
        /// <summary>
        /// 操作失败
        /// </summary>
        /// <param name="code">返回Code</param>
        /// <param name="msg">返回文案</param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonError(string code, string msg)
        {
            return ResponseStr.ToJson(new
            {
                errorCode = code,
                errorMessage = msg,
            });
        }
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <param name="obj">返回的数据对象</param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonTrue(object obj)
        {
            return ResponseStr.ToJson(new
            {
                errorCode = 0,
                errorMessage = "操作成功",
                data = obj
            });
        }
        /// <summary>
        /// 操作成功
        /// </summary>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonTrue()
        {
            return ResponseStr.ToJson(new
            {
                errorCode = 0,
                errorMessage = "操作成功"
            });
        }
        /// <summary>
        /// 特殊返回
        /// </summary>
        /// <param name="msg">返回提示的文案</param>
        /// <returns></returns>
        public static HttpResponseMessage ToJsonTrue(string code, string msg)
        {
            return ResponseStr.ToJson(new
            {
                errorCode = code,
                errorMessage = msg
            });
        }
    }
}
