using BiliSpirit.Common;
using BiliSpirit.Models;
using DevExpress.Mvvm.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BiliSpirit.Models.VideoInfo;

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
            await GetHots();
            await GetHistory();
            await GetLive();
        }

        /// <summary>
        /// 获取热门
        /// </summary>
        /// <returns></returns>
        private async Task GetHots()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["ps"] = "50";
            string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/web-interface/popular", data);
            var test = JsonConvert.DeserializeObject<VideoInfo>(str);
            HotVideoList = test.data.list;
        }

        /// <summary>
        /// 获取历史记录
        /// </summary>
        /// <returns></returns>
        private async Task GetHistory()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["ps"] = "30";
            string str = await WebApiRequest.WebApiGetAsync("http://api.bilibili.com/x/web-interface/history/cursor", data);
            var test = JsonConvert.DeserializeObject<HistoryInfo>(str);
            HistoryList = test.data.list;
        }

       

        /// <summary>
        /// 获取直播动态
        /// </summary>
        /// <returns></returns>
        private async Task GetLive()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["page"] = "1";
            data["pagesize"] = "10";
            string str = await WebApiRequest.WebApiGetAsync("https://api.live.bilibili.com/xlive/web-ucenter/v1/xfetter/FeedList", data);
            var test = JsonConvert.DeserializeObject<LiveInfo>(str);
            LiveList = test.data.list;
        }
        public virtual LoginUser LoginUser { get; set; }

        public virtual VideoList[] HotVideoList { get; set; }

        public virtual HistoryList[] HistoryList { get; set; }

        public virtual LiveList[] LiveList { get; set; }
        
        public async Task RefreshContent()
        {
            await GetHots();
            await GetHistory();
        }

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
    }
}
