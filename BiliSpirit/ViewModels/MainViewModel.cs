using BiliSpirit.Common;
using BiliSpirit.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Shapes;
using System.Windows.Threading;
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

            Timer timer = new Timer();
            timer.Interval = 60000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public async Task RefreshData()
        {
            IsRefreshing = true;
            await Task.Delay(50);
            await GetStat();
            await GetUnReadMessage();

            await GetHots();
            await GetHistory();
            await GetLive();
            IsRefreshing = false;
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            await GetStat();
            await GetUnReadMessage();
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
        #endregion
    }
}
