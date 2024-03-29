﻿using BiliSpirit.Common;
using BiliSpirit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unosquare.FFME;

namespace BiliSpirit
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Login();
            Library.FFmpegDirectory = @".\ffmpeg";
            Library.LoadFFmpeg();
            MediaElement.FFmpegMessageLogged += (s, ev) =>
            {
                System.Diagnostics.Debug.WriteLine(ev.Message);
            };
        }

        private async void Login()
        {
            // 测试
            var info = await WebApiRequest.WebApiGetAsync("http://api.bilibili.com/x/space/myinfo");
            if (info == null)
            {
                Shutdown();
                return;
            }
            if (JsonHelper.GetJsonValue(info, "code") == "0")
            {
                SoftwareCache.LoginUser = JsonConvert.DeserializeObject<LoginUser>(info);
                ShellWindow shellWindow = new ShellWindow();
                shellWindow.Show();
                Current.MainWindow = shellWindow;
            }
            else // 登录信息获取失败
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
