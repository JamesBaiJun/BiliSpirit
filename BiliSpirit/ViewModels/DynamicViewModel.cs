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
using System.Timers;

namespace BiliSpirit.ViewModels
{
    [POCOViewModel]
    public class DynamicViewModel
    {
        public DynamicViewModel()
        {
        }
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }

        public virtual ObservableCollection<DynamicVideoItem> DynamicVideoItems { get; set; } = new ObservableCollection<DynamicVideoItem>();
        public async void Loaded()
        {
            await GetAllDynamic();
        }

        public async Task RefreshContent()
        {
            await GetAllDynamic();
        }

        /// <summary>
        /// 获取全部动态
        /// </summary>
        /// <returns></returns>
        private async Task GetAllDynamic()
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["timezone_offset"] = "-480";
            data["type"] = "video";
            data["page"] = "1";
            string str = await WebApiRequest.WebApiGetAsync("https://api.bilibili.com/x/polymer/web-dynamic/v1/feed/all", data);
            var test = JsonConvert.DeserializeObject<DynamicVideoInfo>(str);

            await LoadHelper.DynamicLoad(DispatcherService, test.data.items, DynamicVideoItems);
        }

        /// <summary>
        /// 跳转到视频
        /// </summary>
        /// <param name="videoItem"></param>
        public void JumpToVideo(DynamicVideoItem videoItem)
        {
            ExpolerHelper.OuterVisit("https:" + videoItem.modules.module_dynamic.major.archive.jump_url);
        }
    }
}
