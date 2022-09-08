using BiliSpirit.Common;
using BiliSpirit.Models;
using DevExpress.Mvvm.DataAnnotations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.ViewModels
{
    [POCOViewModel]
    public class RecommendViewModel
    {
        public RecommendViewModel()
        {

        }

        public async void Loaded()
        {
            await GetAllDynamic();
        }

        public async void RefreshContent()
        {
            await GetAllDynamic();
        }

        public virtual RecommendItem[] RecommendItems { get; set; }

        /// <summary>
        /// 获取全部动态
        /// </summary>
        /// <returns></returns>
        private async Task GetAllDynamic()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["y_num"] = "4";
            data["fresh_type"] = "3";
            data["feed_version"] = "V4";
            data["fresh_idx_1h"] = "2";
            data["fetch_row"] = "1";
            data["fresh_idx"] = "2";
            data["brush"] = "0";
            data["homepage_ver"] = "1";
            data["ps"] = "11";

            try
            {
                string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/web-interface/index/top/feed/rcmd", data);
                var test = JsonConvert.DeserializeObject<RecommendInfo>(str);
                RecommendItems = test.data.item;
            }
            catch (Exception)
            {
            }
            
        }

        /// <summary>
        /// 跳转到视频
        /// </summary>
        /// <param name="videoItem"></param>
        public void JumpToVideo(RecommendItem videoItem)
        {
            ExpolerHelper.OuterVisit(videoItem.uri);
        }
    }
}
