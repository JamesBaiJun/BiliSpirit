using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class HistoryInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public HistoryData data { get; set; }
    }

    public class HistoryData
    {
        public Cursor cursor { get; set; }
        public Tab[] tab { get; set; }
        public HistoryList[] list { get; set; }
    }

    public class Cursor
    {
        public int max { get; set; }
        public int view_at { get; set; }
        public string business { get; set; }
        public int ps { get; set; }
    }

    public class Tab
    {
        public string type { get; set; }
        public string name { get; set; }
    }

    public class HistoryList
    {
        public string title { get; set; }
        public string long_title { get; set; }
        public string cover { get; set; }
        public string[] covers { get; set; }
        public string uri { get; set; }
        public History history { get; set; }
        public int videos { get; set; }
        public string author_name { get; set; }
        public string author_face { get; set; }
        public string author_mid { get; set; }
        public int view_at { get; set; }
        public int progress { get; set; }
        public string badge { get; set; }
        public string show_title { get; set; }
        public int duration { get; set; }
        public string current { get; set; }
        public int total { get; set; }
        public string new_desc { get; set; }
        public int is_finish { get; set; }
        public int is_fav { get; set; }
        public int kid { get; set; }
        public string tag_name { get; set; }
        public int live_status { get; set; }
    }

    public class History
    {
        public int oid { get; set; }
        public int epid { get; set; }
        public string bvid { get; set; }
        public int page { get; set; }
        public int cid { get; set; }
        public string part { get; set; }
        public string business { get; set; }
        public int dt { get; set; }
    }
}
