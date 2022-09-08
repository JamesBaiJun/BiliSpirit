using BiliSpirit.Common;
using BiliSpirit.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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
        }

        private async void Login()
        {
            var info = await WebApiRequest.WebApiGetAsync("http://api.bilibili.com/x/space/myinfo");
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
