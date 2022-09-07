using BiliSpirit.Common;
using BiliSpirit.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
                Application.Current.MainWindow = shellWindow;

                this.Hide();
            }
            else // 登录信息获取失败
            {
                await GetQrCode();
            }
        }

        string qrcode_key = string.Empty;
        private async Task GetQrCode()
        {
            string str = await WebApiRequest.WebApiGetAsync("http://passport.bilibili.com/x/passport-login/web/qrcode/generate");

            string url = JsonHelper.GetJsonValue(str, "url");
            qrcode_key = JsonHelper.GetJsonValue(str, "qrcode_key");

            QrImage.Source = QRCode.CreateQRCode(url, 150, 150);
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
            string code = JsonHelper.GetJsonValue(str, "code");

            switch (code)
            {
                case "0": // 扫码登录成功
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await VerifyLogin();
        }
    }
}
