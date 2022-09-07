using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Common
{
    public class WebTool
    {
        public static async Task<string> GetCookie(string requestUrlString, Dictionary<string, string> para)
        {

            // 组织参数
            StringBuilder parastr = new StringBuilder("?");
            foreach (var item in para)
            {
                parastr.Append(item.Key);
                parastr.Append("=");
                parastr.Append(item.Value);
                parastr.Append("&");
            }
            string paraResult = requestUrlString + parastr.ToString().TrimEnd('&');

            return await Task.Run(() =>
            {
                //向服务端请求
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(paraResult);
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.CookieContainer = new CookieContainer();
                //将请求的结果发送给客户端(界面、应用)
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();

                CookieContainer cookie = new CookieContainer();
                cookie.Add(myResponse.Cookies);
                SoftwareCache.Cookie = cookie;

                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            });
        }

        public static async Task<string> GetHtml(string requestUrlString, CookieContainer cookie)
        {
            return await Task.Run(() =>
            {
                string ua = "Mozilla/5.0 (Linux; U; Android 2.2; en-us; Nexus One Build/FRF91) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.UserAgent = ua;
                myRequest.CookieContainer = cookie;
                myRequest.Headers.Add("Cookie", SoftwareCache.CookieString);
                HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                return reader.ReadToEnd();
            });
        }

        public static string PostLogin(string postData, string requestUrlString, ref CookieContainer cookie)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            //向服务端请求
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = new CookieContainer();
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            //将请求的结果发送给客户端(界面、应用)
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            cookie.Add(myResponse.Cookies);
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static string PostRequest(string postData, string requestUrlString, CookieContainer cookie)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(requestUrlString);
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            myRequest.CookieContainer = cookie;
            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
