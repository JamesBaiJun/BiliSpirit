using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class UnReadInfo
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public UnReadData data { get; set; }
    }

    public class UnReadData
    {
        public int unfollow_unread { get; set; }
        public int follow_unread { get; set; }
        public int unfollow_push_msg { get; set; }
        public int dustbin_push_msg { get; set; }
        public int dustbin_unread { get; set; }
        public int biz_msg_unfollow_unread { get; set; }
        public int biz_msg_follow_unread { get; set; }
    }
}
