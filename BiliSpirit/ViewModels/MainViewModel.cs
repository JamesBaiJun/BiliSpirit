using BiliSpirit.Common;
using BiliSpirit.Models;
using BiliSpirit.Views;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;
using Windows.Foundation.Collections;
using static BiliSpirit.Models.VideoInfo;
using static System.Net.Mime.MediaTypeNames;

namespace BiliSpirit.ViewModels
{

    [POCOViewModel]
    public class MainViewModel
    {
        public MainViewModel()
        {
            LoginUser = SoftwareCache.LoginUser;
        }

        Frame frameContainer;
        public async void Loaded(Frame frame)
        {
            frameContainer = frame;
            await GetTopImage();
            await RefreshData();

            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);
                try
                {
                    switch (args["action"])
                    {
                        case "视频动态":
                            ViewDynamic();
                            break;
                        case "直播动态":
                            ViewLiveCenter();
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                }

            };

            Navigate("热门");

            Timer timer = new Timer();
            timer.Interval = 20000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public async Task RefreshData()
        {
            try
            {
                IsRefreshing = true;
                await Task.Delay(50);
                await GetStat();
                await GetUnReadMessage();
                await GetUnReadDynamic();
                await GetLiveDynamic();

                await GetHots();
                IsRefreshing = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "发生错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await GetStat();
            await GetUnReadMessage();
            await GetUnReadDynamic();
            await GetLiveDynamic();
        }

        public void Navigate(string target)
        {
            switch (target)
            {
                case "推荐":
                    frameContainer.Navigate(new RecommendView());
                    break;
                case "动态":
                    frameContainer.Navigate(new DynamicView());
                    break;
                case "热门":
                    frameContainer.Navigate(new HotVideoView());
                    break;
                case "直播":
                    frameContainer.Navigate(new LiveView());
                    break;
                case "历史":
                    frameContainer.Navigate(new HistoryVideo());
                    break;
                default:
                    break;
            }
        }

        #region Web请求
        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        private async Task GetStat()
        {
            string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/web-interface/nav/stat");
            var test = JsonConvert.DeserializeObject<UserStatInfo>(str);
            UserStatData = test.data;
        }

        /// <summary>
        /// 获取热门
        /// </summary>
        /// <returns></returns>
        public async Task GetHots()
        {
            HotVideoList.Clear();
            await Task.Delay(50);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["ps"] = "50";
            string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/web-interface/popular", data);
            var test = JsonConvert.DeserializeObject<VideoInfo>(str);

            await LoadHelper.DynamicLoad(DispatcherService, test.data.list, HotVideoList);
        }

        /// <summary>
        /// 获取未读消息
        /// </summary>
        /// <returns></returns>
        public async Task GetUnReadMessage()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["build"] = "0";
            data["mobi_app"] = "web";
            data["unread_type"] = "0";
            string str = await WebApiRequest.WebApiGetAsync("https://api.vc.bilibili.com/session_svr/v1/session_svr/single_unread", data);
            var unReadInfo = JsonConvert.DeserializeObject<UnReadInfo>(str);
            UnReadCount = unReadInfo.data.unfollow_unread + unReadInfo.data.follow_unread + unReadInfo.data.biz_msg_unfollow_unread + unReadInfo.data.biz_msg_follow_unread;
        }

        /// <summary>
        /// 获取未读动态
        /// </summary>
        /// <returns></returns>
        public async Task GetUnReadDynamic()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["alltype_offset"] = "0";
            data["article_offset"] = "0";
            data["video_offset"] = "703911862525231100";
            string str = await WebApiRequest.WebApiGetAsync("https://api.vc.bilibili.com/dynamic_svr/v1/dynamic_svr/web_homepage", data);
            var unReadInfo = JsonConvert.DeserializeObject<NewDynamicInfo>(str);
            UnReadDynamicCount = unReadInfo.data.video_num; // 只显示
            if (UnReadDynamicCount > 0)
            {
                new ToastContentBuilder()
                .AddArgument("action", "视频动态")
                .AddArgument("conversationId", 9813)
                .AddText("哔哩哔哩精灵")
                .AddText($"{UnReadDynamicCount} 条新视频动态！").Show();
            }
        }

        int lastLiveCount = 0;
        /// <summary>
        /// 获取直播动态数据
        /// </summary>
        /// <returns></returns>
        public async Task GetLiveDynamic()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["page"] = "1";
            string str = await WebApiRequest.WebApiGetAsync("https://api.live.bilibili.com/xlive/web-ucenter/v1/xfetter/GetWebList", data);
            var unReadInfo = JsonConvert.DeserializeObject<LiveDynamicInfo>(str);
            LiveCount = unReadInfo.data.count; // 只显示
            if (LiveCount != lastLiveCount)
            {
                lastLiveCount = LiveCount;
                new ToastContentBuilder()
                .AddArgument("action", "直播动态")
                .AddArgument("conversationId", 9814)
                .AddText("哔哩哔哩精灵")
                .AddText($"{LiveCount} 位关注的UP正在直播！").Show();
            }
        }

        /// <summary>
        /// 获取背景图片
        /// </summary>
        /// <returns></returns>
        public async Task GetTopImage()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["mid"] = LoginUser.data.mid.ToString();
            data["platform"] = "web";
            data["jsonp"] = "jsonp";
            string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/space/acc/info", data);
            var spaceInfo = JsonConvert.DeserializeObject<SpaceInfo>(str);
            TopImage = spaceInfo.data.top_photo;
        }
        #endregion

        #region 绑定数据
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }
        public virtual UserStatData UserStatData { get; set; }
        public virtual string TopImage { get; set; }

        public virtual LoginUser LoginUser { get; set; }

        public virtual int UnReadCount { get; set; }
        public virtual int UnReadDynamicCount { get; set; }
        public virtual int LiveCount { get; set; }

        public virtual ObservableCollection<VideoList> HotVideoList { get; set; } = new ObservableCollection<VideoList>();

        public virtual bool IsRefreshing { get; set; }
        #endregion

        #region 启动浏览器

        public void JumpToVideo(VideoList video)
        {
            ExpolerHelper.OuterVisit(video.short_link);
        }

        public void ViewMessage()
        {
            ExpolerHelper.OuterVisit($"https://message.bilibili.com");
        }

        public void ViewDynamic()
        {
            ExpolerHelper.OuterVisit($"https://t.bilibili.com");
        }

        public void ViewLiveCenter()
        {
            ExpolerHelper.OuterVisit($"https://link.bilibili.com/p/center/index");
        }

        public void JumpToTarget(string target)
        {
            switch (target)
            {
                case "关注":
                    ExpolerHelper.OuterVisit($"https://space.bilibili.com/{SoftwareCache.LoginUser.data.mid}/fans/follow?spm_id_from=333.999.0.0");
                    break;
                case "粉丝":
                    ExpolerHelper.OuterVisit($"https://space.bilibili.com/{SoftwareCache.LoginUser.data.mid}/fans/fans?spm_id_from=333.999.0.0");
                    break;
                case "动态":
                    ExpolerHelper.OuterVisit($"https://space.bilibili.com/{SoftwareCache.LoginUser.data.mid}/dynamic?spm_id_from=333.999.0.0");
                    break;
                case "主页":
                    ExpolerHelper.OuterVisit($"https://space.bilibili.com/{SoftwareCache.LoginUser.data.mid}");
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
