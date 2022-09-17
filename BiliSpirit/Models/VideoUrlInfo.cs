using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class VideoUrlInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public VideoUrlData data { get; set; }
    }

    public class VideoUrlData
    {
        public string from { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public int quality { get; set; }
        public string format { get; set; }
        public int timelength { get; set; }
        public string accept_format { get; set; }
        public string[] accept_description { get; set; }
        public int[] accept_quality { get; set; }
        public int video_codecid { get; set; }
        public string seek_param { get; set; }
        public string seek_type { get; set; }
        public Durl[] durl { get; set; }
        public Support_Formats[] support_formats { get; set; }
        public object high_format { get; set; }
        public int last_play_time { get; set; }
        public int last_play_cid { get; set; }
    }

    public class Durl
    {
        public int order { get; set; }
        public int length { get; set; }
        public int size { get; set; }
        public string ahead { get; set; }
        public string vhead { get; set; }
        public string url { get; set; }
        public string[] backup_url { get; set; }
    }

    public class Support_Formats
    {
        public int quality { get; set; }
        public string format { get; set; }
        public string new_description { get; set; }
        public string display_desc { get; set; }
        public string superscript { get; set; }
        public object codecs { get; set; }
    }
}
