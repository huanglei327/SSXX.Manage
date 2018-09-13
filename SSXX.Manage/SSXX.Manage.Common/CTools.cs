using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace SSXX.Manage.Common
{
   public class CTools
    {

        private const string DEFSTR = "";
        private static Random randNums = new Random();

        public static long convertToTimeMillis(DateTime dt)
        {
            DateTime time2 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
            TimeSpan span = new TimeSpan(dt.ToUniversalTime().Ticks - time2.Ticks);
            return (long)span.TotalMilliseconds;
        }

        public static long getCurrentTimeMillis()
        {
            DateTime time2 = new DateTime(0x7b2, 1, 1, 0, 0, 0);
            TimeSpan span = new TimeSpan(DateTime.UtcNow.Ticks - time2.Ticks);
            return (long)span.TotalMilliseconds;
        }

        public static string SetOpenId(string openId)
        {
            string begin = openId.Substring(0, 5);
            string end = openId.Substring(5, openId.Length - 5);
            string randStr = CTools.RandLetter(5);
            return begin + randStr + end;
        }
        public static string GetOpenId(string openId)
        {
            string begin = openId.Substring(0, 5);
            string end = openId.Substring(5, openId.Length - 5);
            return begin + end;
        }
        public static T GetDefault<T>()
        {
            T local = default(T);
            if ("" is T)
            {
                //return (T)"";
            }
            return local;
        }

        public static int GetRandom()
        {
            return randNums.Next();
        }

        public static int GetRandom(int minValue, int maxValue)
        {
            return randNums.Next(minValue, maxValue);
        }

        public static string GetRandom2()
        {
            return DateTime.Now.ToString("dHms");
        }

        public static int GetStringLength(string str)
        {
            if (str == null)
            {
                return 0;
            }
            return Encoding.Default.GetBytes(str).Length;
        }

        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds).ToString();
        }



        public static string HtmlEncode(string value)
        {
            return HttpUtility.HtmlEncode(HttpUtility.UrlDecode(value));
        }

        public static bool IsNumber(string numbers)
        {
            return ((numbers != null) && Regex.IsMatch(numbers, @"^\d*$"));
        }

        public static string JoinJsonString(string key, string value)
        {
            return ("\"" + ToJsonString(key) + "\", \"" + ToJsonString(value) + "\"");
        }

        public static int Length(string str)
        {
            return Encoding.Default.GetBytes(str).Length;
        }

        public static string Substring(string content, int length)
        {
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }
            if (content.Length <= length)
            {
                return content;
            }
            return content.Substring(0, length);
        }

        public static string SubstringEnd(string content, int length)
        {
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }
            if (content.Length <= length)
            {
                return content;
            }
            return content.Substring(content.Length - length, length);
        }

        public static DateTime ToDateTime(string time)
        {
            return ToDateTime(time, DateTime.Now);
        }

        public static DateTime ToDateTime(string time, DateTime datetime)
        {
            DateTime now = DateTime.Now;
            if (DateTime.TryParse(time, out now))
            {
                return now;
            }
            return datetime;
        }

        public static decimal ToDecimal(string value)
        {
            return ToDecimal(value, 0M);
        }

        public static decimal ToDecimal(string value, decimal defvalue)
        {
            decimal result = 0M;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            return defvalue;
        }

        public static int ToInt(object numbers)
        {
            return ToInt(numbers, 0);
        }

        public static int ToInt(object numbers, int ndefault)
        {
            if (numbers == null)
                return 0;

            int result = 0;
            if (int.TryParse(numbers.ToString(), out result))
            {
                return result;
            }
            return ndefault;
        }

        public static long ToInt64(double numbers)
        {
            return ToInt64(numbers.ToString("0"), 0L);
        }

        public static long ToInt64(string numbers)
        {
            return ToInt64(numbers, 0L);
        }

        public static long ToInt64(string numbers, long lDefault)
        {
            long result = 0L;
            if (long.TryParse(numbers, out result))
            {
                return result;
            }
            return lDefault;
        }

        public static string ToJsonString(string content)
        {
            content = Regex.Replace(content, "\"", "&quot;");
            content = Regex.Replace(content, @"\\", @"\\");
            content = Regex.Replace(content, "\b", @"\b");
            content = Regex.Replace(content, "\f", @"\f");
            content = Regex.Replace(content, "\n", @"\n");
            content = Regex.Replace(content, "\r", @"\r");
            content = Regex.Replace(content, "\t", @"\t");
            content = Regex.Replace(content, "\0", "");
            return content;
        }

        public static string UnicodeToChinese(string content)
        {
            Regex regex = new Regex(@"(?i)\\u([0-9a-f]{4})");
            return regex.Replace(content, m => ((char)Convert.ToInt32(m.Groups[1].Value, 0x10)).ToString());
        }

        /// <summary>
        /// 得到访问的IP
        /// </summary>
        /// <returns></returns>
        public static string GetUserIp()
        {
            try
            {
                string userCdnIp = GetUserCdnIp();
                if (string.IsNullOrEmpty(userCdnIp) || (userCdnIp == ""))
                {
                    userCdnIp = GetUserIp2();
                    if (userCdnIp == "0.0.0.0")
                    {
                        userCdnIp = "";
                    }
                }
                if (string.IsNullOrEmpty(userCdnIp) || (userCdnIp == ""))
                {
                    userCdnIp = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    if (string.IsNullOrEmpty(userCdnIp))
                    {
                        userCdnIp = "0.0.0.0";
                    }
                }
                return userCdnIp;
            }
            catch
            {
                return "";
            }
        }

        public static string GetUserCdnIp()
        {
            try
            {
                string str = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
                if (string.IsNullOrEmpty(str))
                {
                    str = "";
                }
                return str;
            }
            catch
            {
                return "";
            }
        }



        public static string GetUserIp2()
        {
            string str = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(str))
            {
                str = "0.0.0.0";
            }
            return str;
        }




        ///// <summary>
        ///// 计算字符串的MD5值
        ///// </summary>
        ///// <param name="msg">要计算的字符串</param>
        ///// <returns></returns>
        //public static string GetMD5FromString(string msg)
        //{

        //    //1.创建一个用来计算MD5值的类的对象
        //    using (MD5 md5 = MD5.Create())
        //    {

        //        //把字符串转换为byte[]
        //        //注意：如果字符串中包含汉字，则这里会把汉字使用utf-8编码转换为byte[]，当其他地方
        //        //计算MD5值的时候，如果对汉字使用了不同的编码，则同样的汉字生成的byte[]是不一样的，所以计算出的MD5值也就不一样了。
        //        byte[] msgBuffer = Encoding.UTF8.GetBytes(msg);

        //        //2.计算给定字符串的MD5值
        //        //返回值就是就算后的MD5值,如何把一个长度为16的byte[]数组转换为一个长度为32的字符串：就是把每个byte转成16进制同时保留2位即可。
        //        byte[] md5Buffer = md5.ComputeHash(msgBuffer);
        //        md5.Clear();//释放资源

        //        StringBuilder sbMd5 = new StringBuilder();
        //        for (int i = 0; i < md5Buffer.Length; i++)
        //        {
        //            sbMd5.Append(md5Buffer[i].ToString("x2"));
        //        }
        //        return sbMd5.ToString();

        //    }

        //}

        /// <summary>
        /// 获取md5码
        /// </summary>
        /// <param name="source">待转换字符串</param>
        /// <returns>md5加密后的字符串</returns>
        public static string GetMD5FromString(string source)
        {
            string result = "";
            try
            {
                using (MD5 getmd5 = new MD5CryptoServiceProvider())
                {
                    byte[] targetStr = getmd5.ComputeHash(UnicodeEncoding.UTF8.GetBytes(source));
                    result = BitConverter.ToString(targetStr).Replace("-", "");
                    return result;
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        /// <summary>
        /// HMAC-SHA256的base64加密 和php的 hash_hmac()方法一样
        /// </summary>
        /// <param name="message">加密字符串</param>
        /// <param name="secret">加密密钥</param>
        /// <returns></returns>
        public static string HashHmac256(string message, string secret)
        {
            Encoding encoding = Encoding.UTF8;
            var keyByte = encoding.GetBytes(secret);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                hmacsha256.ComputeHash(encoding.GetBytes(message));

                byte[] buff = hmacsha256.Hash;

                string sbinary = "";
                for (int i = 0; i < buff.Length; i++)
                    sbinary += buff[i].ToString("X2"); /* hex format */
                return sbinary;
            }
        }


        /// <summary>    
        /// 生成Json格式    
        /// </summary>    
        /// <typeparam name="T"></typeparam>    
        /// <param name="obj"></param>    
        /// <returns></returns>    
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray()); return szJson;
            }
        }
        
        public static string GetConfigValue(string KeyName)
        {
            string content = System.Configuration.ConfigurationManager.AppSettings[KeyName];
            return content;
        }

        /// <summary>
        /// 将一个xml字符串序列化成一个实体类
        /// </summary>
        /// <typeparam name="T">实体类名</typeparam>
        /// <param name="input">xml字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }

        /// <summary>
        /// 将一个实体类转换成xml
        /// </summary>
        /// <typeparam name="T">实体类名</typeparam>
        /// <param name="ObjectToSerialize">实体类对象</param>
        /// <returns></returns>
        public static string Serialize<T>(T ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                return textWriter.ToString();
            }
        }


        public static string GetAbsoluteUrl()
        {
            return HttpContext.Current.Request.Url.AbsoluteUri;
        }

        /// <summary>
        /// 唯一订单号生成
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMdd");


            return strDateTimeNumber + GetString(5, null);
        }



        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="strLength">字符串长度</param>
        /// <param name="Seed">随机函数种子值</param>
        /// <returns>指定长度的随机字符串</returns>
        public static string GetString(int strLength, params int[] Seed)
        {
            string strSep = ",";
            char[] chrSep = strSep.ToCharArray();
            string strChar = "0,1,2,3,4,5,6,7,8,9,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z"
             + ",A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] aryChar = strChar.Split(chrSep, strChar.Length);
            string strRandom = string.Empty;
            Random Rnd;
            if (Seed != null && Seed.Length > 0)
            {
                Rnd = new Random(Seed[0]);
            }
            else
            {
                Rnd = new Random();
            }
            //生成随机字符串
            for (int i = 0; i < strLength; i++)
            {
                strRandom += aryChar[Rnd.Next(aryChar.Length)];
            }
            return strRandom;
        }



        /// <summary>
        /// 参考：msdn上的RNGCryptoServiceProvider例子
        /// </summary>
        /// <param name="numSeeds"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static int NextRandom(int numSeeds, int length)
        {
            // Create a byte array to hold the random value.  
            byte[] randomNumber = new byte[length];
            // Create a new instance of the RNGCryptoServiceProvider.  
            System.Security.Cryptography.RNGCryptoServiceProvider rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            // Fill the array with a random value.  
            rng.GetBytes(randomNumber);
            // Convert the byte to an uint value to make the modulus operation easier.  
            uint randomResult = 0x0;
            for (int i = 0; i < length; i++)
            {
                randomResult |= ((uint)randomNumber[i] << ((length - 1 - i) * 8));
            }

            return (int)(randomResult % numSeeds) + 1;
        }
        /// <summary>
        /// 取得nonceStr
        /// </summary>
        /// <returns></returns>
        public static string GetNonceStr()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder sbResult = new StringBuilder();
            Random random = new Random(chars.Length);
            for (int i = 0; i < 32; i++)
            {
                sbResult.Append(chars[random.Next(chars.Length)]);
            }
            return sbResult.ToString();
        }
        /// <summary>
        /// SHA1 加密，返回大写字符串
        /// </summary>
        /// <param name="content">需要加密字符串</param>
        /// <param name="encode">指定加密编码</param>
        /// <returns>返回40位大写字符串</returns>
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }


        #region 字母随机数
        /// <summary>
        /// 字母随机数
        /// </summary>
        /// <param name="n">生成长度</param>
        /// <returns></returns>
        public static string RandLetter(int n)
        {
            char[] arrChar = new char[]{
            'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
            '_',
           'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
          };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

            }

            return num.ToString();
        }
        #endregion
    }
}
