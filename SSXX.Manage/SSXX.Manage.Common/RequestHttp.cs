using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSXX.Manage.Common
{
    public class RequestHttp
    {
        /// <summary>  
        /// Http同步Get同步请求  
        /// </summary>  
        /// <param name="url">Url地址</param>  
        /// <param name="encode">编码(默认UTF8)</param>  
        /// <returns></returns>  
        public static string HttpGet(string url, Encoding encode = null)
        {
            var webClient = new WebClient { Encoding = Encoding.UTF8 };
            if (encode != null)
                webClient.Encoding = encode;

            return webClient.DownloadString(url);
        }

        public string HttpPost(string url, string postData)
        {
            byte[] data = Encoding.UTF8.GetBytes(postData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "Post";
            request.ContentType = "application/json";
            request.ContentLength = data.Length;
            request.KeepAlive = true;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);

            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();

            request.Abort();
            response.Close();
            reader.Dispose();
            stream.Close();
            stream.Dispose();

            return content;
        }

        public static string HttpWxPushMsg(string JSONData, string Url)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(JSONData);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentLength = bytes.Length;
            request.ContentType = "json";
            Stream reqstream = request.GetRequestStream();
            reqstream.Write(bytes, 0, bytes.Length);
            //声明一个HttpWebRequest请求
            request.Timeout = 90000;
            //设置连接超时时间
            request.Headers.Set("Pragma", "no-cache");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream streamReceive = response.GetResponseStream();
            Encoding encoding = Encoding.UTF8;
            StreamReader streamReader = new StreamReader(streamReceive, encoding);
            string strResult = streamReader.ReadToEnd();
            streamReceive.Dispose();
            streamReader.Dispose();
            return strResult;
        }
    }
}
