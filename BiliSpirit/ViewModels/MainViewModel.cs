using BiliSpirit.Common;
using BiliSpirit.Models;
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

        public async void Loaded()
        {
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
                await GetHistory();
                await GetLive();
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

            await DynamicLoad(test.data.list, HotVideoList);
        }

        /// <summary>
        /// 获取历史记录
        /// </summary>
        /// <returns></returns>
        public async Task GetHistory()
        {
            HistoryList.Clear();
            await Task.Delay(50);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["ps"] = "30";
            string str = await WebApiRequest.WebApiGetAsync("http://api.bilibili.com/x/web-interface/history/cursor", data);
            var test = JsonConvert.DeserializeObject<HistoryInfo>(str);

            await DynamicLoad(test.data.list, HistoryList);
        }

        /// <summary>
        /// 获取直播动态
        /// </summary>
        /// <returns></returns>
        public async Task GetLive()
        {
            LiveList.Clear();
            await Task.Delay(50);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["page"] = "1";
            data["pagesize"] = "10";
            string str = await WebApiRequest.WebApiGetAsync("https://api.live.bilibili.com/xlive/web-ucenter/v1/xfetter/FeedList", data);
            var test = JsonConvert.DeserializeObject<LiveInfo>(str);

            await DynamicLoad(test.data.list, LiveList);
        }

        /// <summary>
        /// 动态加载列表数据
        /// </summary>
        private async Task DynamicLoad<T>(Array array, ObservableCollection<T> target) where T : class
        {
            await DispatcherService.BeginInvoke(new Action(async () =>
            {
                target.Clear();
                foreach (var item in array)
                {
                    target.Add(item as T);
                    await Task.Delay(20);
                }
            }));
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

        public virtual ObservableCollection<HistoryList> HistoryList { get; set; } = new ObservableCollection<HistoryList>();

        public virtual bool IsRefreshing { get; set; }

        public virtual ObservableCollection<LiveList> LiveList { get; set; } = new ObservableCollection<LiveList>();
        #endregion

        #region 启动浏览器

        public void JumpToVideo(VideoList video)
        {
            ExpolerHelper.OuterVisit(video.short_link);
        }

        public void JumpToHistory(HistoryList his)
        {
            if (his.history.business == "live")
            {
                ExpolerHelper.OuterVisit($"https://live.bilibili.com/{his.history.oid}");
                return;
            }
            ExpolerHelper.OuterVisit($"https://www.bilibili.com/video/{his.history.bvid}");
        }

        public void JumpToLive(LiveList live)
        {
            ExpolerHelper.OuterVisit($"https://live.bilibili.com/{live.roomid}?broadcast_type=1");
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
