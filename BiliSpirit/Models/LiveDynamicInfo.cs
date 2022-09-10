using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{

    public class LiveDynamicInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public LiveDynamicData data { get; set; }
    }

    public class LiveDynamicData
    {
        public Room[] rooms { get; set; }
        public LiveDynamicList[] list { get; set; }
        public int count { get; set; }
    }

    public class Room
    {
        public string title { get; set; }
        public int room_id { get; set; }
        public int uid { get; set; }
        public int online { get; set; }
        public int live_time { get; set; }
        public int live_status { get; set; }
        public int short_id { get; set; }
        public int area { get; set; }
        public string area_name { get; set; }
        public int area_v2_id { get; set; }
        public string area_v2_name { get; set; }
        public string area_v2_parent_name { get; set; }
        public int area_v2_parent_id { get; set; }
        public string uname { get; set; }
        public string face { get; set; }
        public string tag_name { get; set; }
        public string tags { get; set; }
        public string cover_from_user { get; set; }
        public string keyframe { get; set; }
        public string lock_till { get; set; }
        public string hidden_till { get; set; }
        public int broadcast_type { get; set; }
        public bool is_encrypt { get; set; }
        public string link { get; set; }
        public string nickname { get; set; }
        public string roomname { get; set; }
        public int roomid { get; set; }
        public int liveTime { get; set; }
    }

    public class LiveDynamicList
    {
        public string title { get; set; }
        public int room_id { get; set; }
        public int uid { get; set; }
        public int online { get; set; }
        public int live_time { get; set; }
        public int live_status { get; set; }
        public int short_id { get; set; }
        public int area { get; set; }
        public string area_name { get; set; }
        public int area_v2_id { get; set; }
        public string area_v2_name { get; set; }
        public string area_v2_parent_name { get; set; }
        public int area_v2_parent_id { get; set; }
        public string uname { get; set; }
        public string face { get; set; }
        public string tag_name { get; set; }
        public string tags { get; set; }
        public string cover_from_user { get; set; }
        public string keyframe { get; set; }
        public string lock_till { get; set; }
        public string hidden_till { get; set; }
        public int broadcast_type { get; set; }
        public bool is_encrypt { get; set; }
        public string link { get; set; }
        public string nickname { get; set; }
        public string roomname { get; set; }
        public int roomid { get; set; }
        public int liveTime { get; set; }
    }
}
