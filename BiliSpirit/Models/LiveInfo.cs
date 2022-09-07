using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class LiveInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public LiveData data { get; set; }
    }

    public class LiveData
    {
        public int results { get; set; }
        public string page { get; set; }
        public string pagesize { get; set; }
        public LiveList[] list { get; set; }
    }

    public class LiveList
    {
        public string cover { get; set; }
        public string face { get; set; }
        public string uname { get; set; }
        public string title { get; set; }
        public int roomid { get; set; }
        public string pic { get; set; }
        public int online { get; set; }
        public string link { get; set; }
        public int uid { get; set; }
        public int parent_area_id { get; set; }
        public int area_id { get; set; }
    }
}
