using BiliSpirit.Common;
using BiliSpirit.Models;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.ViewModels
{
    public class HistoryVideoViewModel
    {
        public HistoryVideoViewModel()
        {
        }

        public async void Loaded()
        {
            await GetHistory();
        }
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }

        public virtual ObservableCollection<HistoryList> HistoryList { get; set; } = new ObservableCollection<HistoryList>();

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

            await LoadHelper.DynamicLoad(DispatcherService, test.data.list, HistoryList);
        }
    }
}
