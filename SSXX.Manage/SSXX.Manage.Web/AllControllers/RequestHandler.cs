using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SSXX.Manage.Web.AllControllers
{
    public class RequestHandler : DelegatingHandler
    {
        /// <summary>
        /// 拦截请求
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="cancellationToken">用于发送取消操作信号</param>
        /// <returns></returns>
        protected async override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //获取URL参数
            NameValueCollection query = HttpUtility.ParseQueryString(request.RequestUri.Query);
            //获取Post正文数据，比如json文本
            string fRequesContent = request.Content.ReadAsStringAsync().Result;

            //可以做一些其他安全验证工作，比如Token验证，签名验证。
            //可以在需要时自定义HTTP响应消息
            //return SendError("自定义的HTTP响应消息", HttpStatusCode.OK);

            //请求处理耗时跟踪
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //调用内部处理接口，并获取HTTP响应消息
            HttpResponseMessage response = await base.SendAsync(request, cancellationToken);
            //篡改HTTP响应消息正文
            response.Content = new StringContent(response.Content.ReadAsStringAsync().Result.Replace(@"\\", @"\"));
            sw.Stop();
            //记录处理耗时
            long exeMs = sw.ElapsedMilliseconds;
            return response;
        }

        /// <summary>
        /// 构造自定义HTTP响应消息
        /// </summary>
        /// <param name="error"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private HttpResponseMessage SendError(string error, HttpStatusCode code)
        {
            var response = new HttpResponseMessage();
            response.Content = new StringContent(error);
            response.StatusCode = code;
            return response;
        }

    }
}