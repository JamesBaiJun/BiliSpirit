using BiliSpirit.Common;
using BiliSpirit.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.UI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.ViewModels
{
    [POCOViewModel]
    public class SideViewModel
    {
        public SideViewModel()
        {

        }

        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }

        public virtual ObservableCollection<VideoList> HotVideoList { get; set; } = new ObservableCollection<VideoList>();

        public async void Loaded()
        {
            await GetHots();
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

        public void JumpToVideo(VideoList video)
        {
            ExpolerHelper.OuterVisit(video.short_link);
        }
    }
}
