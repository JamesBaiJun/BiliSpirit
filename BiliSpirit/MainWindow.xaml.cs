using BiliSpirit.Common;
using BiliSpirit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Timer = System.Timers.Timer;

namespace BiliSpirit
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Timer timer = new Timer();

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await GetQrCode();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        /// <summary>
        /// 验证是否完成扫码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            bool result = await VerifyLogin();
            if (result)
            {
                var info = await WebApiRequest.WebApiGetAsync("http://api.bilibili.com/x/space/myinfo");

                await Dispatcher.BeginInvoke(() =>
                {
                    if (JsonHelper.GetJsonValue(info, "code") == "0")
                    {
                        timer.Stop();
                        SoftwareCache.LoginUser = JsonConvert.DeserializeObject<LoginUser>(info);
                        ShellWindow shellWindow = new ShellWindow();
                        shellWindow.Show();
                        Application.Current.MainWindow = shellWindow;
                        Hide();
                    }
                });

            }
        }

        string qrcode_key = string.Empty;
        private async Task GetQrCode()
        {
            string str = await WebApiRequest.WebApiGetAsync("http://passport.bilibili.com/x/passport-login/web/qrcode/generate");

            string url = JsonHelper.GetJsonValue(str, "url");
            qrcode_key = JsonHelper.GetJsonValue(str, "qrcode_key");
            Dispatcher.Invoke(() =>
            {
                QrImage.Source = QRCode.CreateQRCode(url, 150, 150);
            });
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private async void PackIconMaterial_MouseDown(object sender, MouseButtonEventArgs e)
        {
            await GetQrCode();
        }

        public async Task<bool> VerifyLogin()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["qrcode_key"] = qrcode_key;
            string str = await WebApiRequest.WebApiGetAsync("http://passport.bilibili.com/x/passport-login/web/qrcode/poll", data);
            string dataStr = Regex.Split(str, "\"data\":")[1];
            dataStr = dataStr.Remove(dataStr.Length - 1);
            string code = JsonHelper.GetJsonValue(dataStr, "code");

            switch (code)
            {
                case "0": // 扫码登录成功
                    string cookie = JsonHelper.GetJsonValue(str, "url").Split('&')[3];
                    SoftwareCache.CookieString = cookie;
                    File.WriteAllText(".\\Cookie.txt", cookie);
                    return true;
                case "86038":// 二维码已失效
                    await GetQrCode();
                    break;
                case "86090":// 二维码已扫码未确认
                    break;
                case "86101":// 未扫码
                    break;
                default:
                    break;
            }

            return false;
        }
    }
}
