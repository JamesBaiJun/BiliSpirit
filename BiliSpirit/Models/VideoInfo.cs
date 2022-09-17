using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class VideoInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public VideoData data { get; set; }
    }

    public class VideoData
    {
        public VideoList[] list { get; set; }
        public bool no_more { get; set; }
    }

    public class VideoList
    {
        public int aid { get; set; }
        public int videos { get; set; }
        public int tid { get; set; }
        public string tname { get; set; }
        public int copyright { get; set; }
        public string pic { get; set; }
        public string title { get; set; }
        public int pubdate { get; set; }
        public int ctime { get; set; }
        public string desc { get; set; }
        public int state { get; set; }
        public int duration { get; set; }
        public int mission_id { get; set; }
        public Rights rights { get; set; }
        public Owner owner { get; set; }
        public Stat stat { get; set; }
        public string dynamic { get; set; }
        public string cid { get; set; }
        public Dimension dimension { get; set; }
        public string short_link { get; set; }
        public string short_link_v2 { get; set; }
        public string first_frame { get; set; }
        public string pub_location { get; set; }
        public string bvid { get; set; }
        public int season_type { get; set; }
        public bool is_ogv { get; set; }
        public object ogv_info { get; set; }
        public Rcmd_Reason rcmd_reason { get; set; }
        public int up_from_v2 { get; set; }
        public Premiere premiere { get; set; }
        public int season_id { get; set; }
    }

    public class Rights
    {
        public int bp { get; set; }
        public int elec { get; set; }
        public int download { get; set; }
        public int movie { get; set; }
        public int pay { get; set; }
        public int hd5 { get; set; }
        public int no_reprint { get; set; }
        public int autoplay { get; set; }
        public int ugc_pay { get; set; }
        public int is_cooperation { get; set; }
        public int ugc_pay_preview { get; set; }
        public int no_background { get; set; }
        public int arc_pay { get; set; }
        public int pay_free_watch { get; set; }
    }

    public class Owner
    {
        public string mid { get; set; }
        public string name { get; set; }
        public string face { get; set; }
    }

    public class Stat
    {
        public int aid { get; set; }

        private float _view;
        public float view
        {
            get { return _view; }
            set { _view = (float)Math.Round(value / 10000, 1); }
        }

        public int danmaku { get; set; }
        public int reply { get; set; }
        public int favorite { get; set; }
        public int coin { get; set; }
        public int share { get; set; }
        public int now_rank { get; set; }
        public int his_rank { get; set; }
        public int like { get; set; }
        public int dislike { get; set; }
    }

    public class Dimension
    {
        public int width { get; set; }
        public int height { get; set; }
        public int rotate { get; set; }
    }

    public class Rcmd_Reason
    {
        public string content { get; set; }
        public int corner_mark { get; set; }
    }

    public class Premiere
    {
        public int state { get; set; }
        public int start_time { get; set; }
        public int room_id { get; set; }
    }

}
