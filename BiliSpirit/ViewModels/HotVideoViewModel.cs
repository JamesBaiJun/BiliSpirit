using BiliSpirit.Common;
using BiliSpirit.Models;
using BiliSpirit.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.ViewModels
{
    [POCOViewModel]
    public class HotVideoViewModel
    {

        public HotVideoViewModel()
        {
        }

        public async void Loaded()
        {
            await GetHots();
        }
        protected IDispatcherService DispatcherService { get { return this.GetService<IDispatcherService>(); } }

        public virtual ObservableCollection<VideoList> HotVideoList { get; set; } = new ObservableCollection<VideoList>();

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

        public async void JumpToVideo(VideoList video)
        {
            var stream = await WebApiRequest.GetVideoURL(video.bvid, video.cid);

            var videoStream = new FFMEMediaStream(stream);
            PlayerWindow player = new PlayerWindow()
            {
                VideoAdress = videoStream,
            };

            player.Show();
            //ExpolerHelper.OuterVisit(video.short_link);
            //byte[] bytes = new byte[stream.Length];

            //stream.Read(bytes, 0, bytes.Length);

            //// 设置当前流的位置为流的开始

            //stream.Seek(0, SeekOrigin.Begin);

            //// 把 byte[] 写入文件

            //FileStream fs = new FileStream("D:\\abc.flv", FileMode.Create);

            //BinaryWriter bw = new BinaryWriter(fs);

            //bw.Write(bytes);

            //bw.Close();

            //fs.Close();
        }
    }
}
