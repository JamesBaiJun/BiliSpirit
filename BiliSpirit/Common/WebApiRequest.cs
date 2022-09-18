using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Security;
using System.Windows;
using BiliSpirit.Models;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Policy;

namespace BiliSpirit.Common
{
    public class WebApiRequest
    {
        static WebApiRequest()
        {
            qnList["720P"] = "64";
            qnList["720P60"] = "74";
            qnList["1080P"] = "80";
            qnList["1080P+"] = "112";
            qnList["4K"] = "120";
        }

        /// <summary>
        /// 调用webapi通用方法(带Cookie)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> WebApiGetAsync(string url)
        {
            return await Task.Run(() =>
            {
                string content = null;
                using (HttpClient httpclient = new HttpClient())
                {
                    HttpRequestMessage msg = new HttpRequestMessage();
                    msg.Method = HttpMethod.Get;
                    msg.RequestUri = new Uri(url);
                    if (SoftwareCache.CookieString != null)
                    {
                        msg.Headers.Add("Cookie", SoftwareCache.CookieString);//cookie:SESSDATA=***
                    }

                    try
                    {
                        var result = httpclient.SendAsync(msg).Result;
                        content = result.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"网络错误:{ex.Message}！", "提示");
                    }

                }
                return content;
            });
        }

        /// <summary>
        /// 调用webapi通用方法(带参数)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<string> WebApiGetAsync(string url, Dictionary<string, string> para)
        {
            return await Task.Run(() =>
            {
                string content = null;
                StringBuilder parastr = new StringBuilder("?");
                foreach (var item in para)
                {
                    parastr.Append(item.Key);
                    parastr.Append("=");
                    parastr.Append(item.Value);
                    parastr.Append("&");
                }

                string paraResult = parastr.ToString().TrimEnd('&');

                using (HttpClient httpclient = new HttpClient())
                {
                    HttpRequestMessage msg = new HttpRequestMessage();
                    msg.Method = HttpMethod.Get;
                    msg.RequestUri = new Uri(url + paraResult);
                    if (SoftwareCache.CookieString != null)
                    {
                        msg.Headers.Add("Cookie", SoftwareCache.CookieString);//cookie:SESSDATA=***
                    }

                    try
                    {
                        var result = httpclient.SendAsync(msg).Result;
                        content = result.Content.ReadAsStringAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"网络错误:{ex.Message}！", "提示");
                    }
                }
                return content;
            });
        }

        #region Get流
        /// <summary>
        /// 调用webapi通用方法(带参数)
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async static Task<Stream> WebApiGetStreamAsync(string url, Dictionary<string, string> para)
        {
            return await Task.Run(() =>
            {
                Stream content = null;
                StringBuilder parastr = new StringBuilder("?");
                foreach (var item in para)
                {
                    parastr.Append(item.Key);
                    parastr.Append("=");
                    parastr.Append(item.Value);
                    parastr.Append("&");
                }

                string paraResult = parastr.ToString().TrimEnd('&');

                using (HttpClient httpclient = new HttpClient())
                {
                    HttpRequestMessage msg = new HttpRequestMessage();
                    msg.Method = HttpMethod.Get;
                    msg.RequestUri = new Uri(url + paraResult);
                    if (SoftwareCache.CookieString != null)
                    {
                        msg.Headers.Add("Cookie", SoftwareCache.CookieString);//cookie:SESSDATA=***
                        msg.Headers.Add("referer", "https://www.bilibili.com");//cookie:SESSDATA=***
                        msg.Headers.Add("User-Agent", "Mozilla/5.0 (iPhone; CPU iPhone OS 11_0 like Mac OS X) AppleWebKit/604.1.38 (KHTML, like Gecko) Version/11.0 Mobile/15A372 Safari/604.1");//cookie:SESSDATA=***
                    }

                    try
                    {
                        var result = httpclient.SendAsync(msg, HttpCompletionOption.ResponseHeadersRead).Result;
                        content = result.Content.ReadAsStreamAsync().Result;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"网络错误:{ex.Message}！", "提示");
                    }
                }
                return content;
            });
        }

        #endregion
        public async static Task<string> PostHttpAsync(string url, string body, string contentType)
        {
            return await Task.Run(() =>
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                httpWebRequest.ContentType = contentType;
                httpWebRequest.Method = "POST";
                httpWebRequest.Timeout = 20000;

                byte[] btBodys = Encoding.UTF8.GetBytes(body);
                httpWebRequest.ContentLength = btBodys.Length;
                httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                string responseContent = streamReader.ReadToEnd();

                httpWebResponse.Close();
                streamReader.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();

                return responseContent;
            });
        }

        static Dictionary<string, string> qnList = new Dictionary<string, string>();

        /// <summary>
        /// 获取视频信息
        /// </summary>
        public static async Task<VideoUrlInfo> GetVideoURL(string bvid, string cid, string qn = null)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["bvid"] = bvid;
            data["cid"] = cid;
            if (!string.IsNullOrEmpty(qn))
            {
                data["qn"] = qnList[qn];
            }

            string str = await WebApiGetAsync("http://api.bilibili.com/x/player/playurl", data);
            var url = JsonConvert.DeserializeObject<VideoUrlInfo>(str);

            
            return url;
        }

        /// <summary>
        /// 获取视频流
        /// </summary>
        public static async Task<Stream> GetVideoStream(string url)
        {
            string urlStr = url;

            Dictionary<string, string> para = new Dictionary<string, string>();
            var stream = await WebApiGetStreamAsync(urlStr, para);

            return stream;
        }
    }
}
