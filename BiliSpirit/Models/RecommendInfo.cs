using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class RecommendInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public RecommendData data { get; set; }
    }

    public class RecommendData
    {
        public RecommendItem[] item { get; set; }
        public Business_Card[] business_card { get; set; }
        public object floor_info { get; set; }
        public object user_feature { get; set; }
        public float preload_expose_pct { get; set; }
        public float preload_floor_expose_pct { get; set; }
    }

    public class RecommendItem
    {
        public int id { get; set; }
        public string bvid { get; set; }
        public int cid { get; set; }
        public string _goto { get; set; }
        public string uri { get; set; }
        public string pic { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int pubdate { get; set; }
        public RecommendOwner owner { get; set; }
        public RecommendStat stat { get; set; }
        public object av_feature { get; set; }
        public int is_followed { get; set; }
        public RecommendRcmd_Reason rcmd_reason { get; set; }
        public int show_info { get; set; }
        public string track_id { get; set; }
        public int pos { get; set; }
        public object room_info { get; set; }
        public object ogv_info { get; set; }
        public object business_info { get; set; }
        public int is_stock { get; set; }
    }

    public class RecommendOwner
    {
        public int mid { get; set; }
        public string name { get; set; }
        public string face { get; set; }
    }

    public class RecommendStat
    {
        private float _view { get; set; }
        public float view
        {
            get { return _view; }
            set { _view = (float)Math.Round(value / 10000, 1); }
        }
        public int like { get; set; }
        public int danmaku { get; set; }
    }

    public class RecommendRcmd_Reason
    {
        public int reason_type { get; set; }
        public string content { get; set; }
    }

    public class Business_Card
    {
        public int id { get; set; }
        public string bvid { get; set; }
        public int cid { get; set; }
        public string _goto { get; set; }
        public string uri { get; set; }
        public string pic { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public int pubdate { get; set; }
        public object owner { get; set; }
        public object stat { get; set; }
        public object av_feature { get; set; }
        public int is_followed { get; set; }
        public object rcmd_reason { get; set; }
        public int show_info { get; set; }
        public string track_id { get; set; }
        public int pos { get; set; }
        public object room_info { get; set; }
        public Ogv_Info ogv_info { get; set; }
        public object business_info { get; set; }
        public int is_stock { get; set; }
    }

    public class Ogv_Info
    {
        public int season_id { get; set; }
        public string title { get; set; }
        public string sub_title { get; set; }
        public string cover { get; set; }
        public string follows { get; set; }
        public string plays { get; set; }
        public string dms { get; set; }
        public string likes { get; set; }
        public string square_cover { get; set; }
        public string pub_index { get; set; }
    }
}
