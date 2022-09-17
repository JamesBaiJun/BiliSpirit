﻿using System;
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

namespace BiliSpirit.Common
{
    public class WebApiRequest
    {

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

        /// <summary>
        /// 获取视频的地址
        /// </summary>
        public static async Task<VideoUrlInfo> GetVideoURL(string bvid, string cid)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["bvid"] = bvid;
            data["cid"] = cid;
            string str = await WebApiGetAsync("http://api.bilibili.com/x/player/playurl", data);
            var url = JsonConvert.DeserializeObject<VideoUrlInfo>(str);
            return url;
        }
    }
}
