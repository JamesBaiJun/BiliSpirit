using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class UserStatInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public UserStatData data { get; set; }
    }

    public class UserStatData
    {
        public int following { get; set; }
        public int follower { get; set; }
        public int dynamic_count { get; set; }
    }
}
