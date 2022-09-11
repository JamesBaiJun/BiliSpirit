using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class DynamicVideoInfo
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public DynamicVideoData data { get; set; }
    }

    public class DynamicVideoData
    {
        public bool has_more { get; set; }
        public DynamicVideoItem[] items { get; set; }
        public string offset { get; set; }
        public string update_baseline { get; set; }
        public int update_num { get; set; }
    }

    public class DynamicVideoItem
    {
        public DynamicVideoBasic basic { get; set; }
        public string id_str { get; set; }
        public DynamicVideoModules modules { get; set; }
        public string type { get; set; }
        public bool visible { get; set; }
    }

    public class DynamicVideoBasic
    {
        public string comment_id_str { get; set; }
        public int comment_type { get; set; }
        public Like_Icon like_icon { get; set; }
        public string rid_str { get; set; }
    }

    public class Like_Icon
    {
        public string action_url { get; set; }
        public string end_url { get; set; }
        public int id { get; set; }
        public string start_url { get; set; }
    }

    public class DynamicVideoModules
    {
        public Module_Author module_author { get; set; }
        public Module_Dynamic module_dynamic { get; set; }
        public Module_Interaction module_interaction { get; set; }
        public Module_More module_more { get; set; }
        public Module_Stat module_stat { get; set; }
        public Module_Fold module_fold { get; set; }
    }

    public class Module_Author
    {
        public string face { get; set; }
        public bool face_nft { get; set; }
        public bool? following { get; set; }
        public string jump_url { get; set; }
        public string label { get; set; }
        public string mid { get; set; }
        public string name { get; set; }
        public Official_Verify official_verify { get; set; }
        public DynamicVideoPendant pendant { get; set; }
        public string pub_action { get; set; }
        public string pub_location_text { get; set; }
        public string pub_time { get; set; }
        public int pub_ts { get; set; }
        public string type { get; set; }
        public DynamicVideoVip vip { get; set; }
        public Decorate decorate { get; set; }
    }

    public class Official_Verify
    {
        public string desc { get; set; }
        public int type { get; set; }
    }

    public class DynamicVideoPendant
    {
        public int expire { get; set; }
        public string image { get; set; }
        public string image_enhance { get; set; }
        public string image_enhance_frame { get; set; }
        public string name { get; set; }
        public int pid { get; set; }
    }

    public class DynamicVideoVip
    {
        public int avatar_subscript { get; set; }
        public string avatar_subscript_url { get; set; }
        public long due_date { get; set; }
        public DynamicVideoLabel label { get; set; }
        public string nickname_color { get; set; }
        public int status { get; set; }
        public int theme_type { get; set; }
        public int type { get; set; }
    }

    public class DynamicVideoLabel
    {
        public string bg_color { get; set; }
        public int bg_style { get; set; }
        public string border_color { get; set; }
        public string img_label_uri_hans { get; set; }
        public string img_label_uri_hans_static { get; set; }
        public string img_label_uri_hant { get; set; }
        public string img_label_uri_hant_static { get; set; }
        public string label_theme { get; set; }
        public string path { get; set; }
        public string text { get; set; }
        public string text_color { get; set; }
        public bool use_img_label { get; set; }
    }

    public class Decorate
    {
        public string card_url { get; set; }
        public Fan fan { get; set; }
        public int id { get; set; }
        public string jump_url { get; set; }
        public string name { get; set; }
        public int type { get; set; }
    }

    public class Fan
    {
        public string color { get; set; }
        public bool is_fan { get; set; }
        public string num_str { get; set; }
        public int number { get; set; }
    }

    public class Module_Dynamic
    {
        public object additional { get; set; }
        public Desc desc { get; set; }
        public Major major { get; set; }
        public object topic { get; set; }
    }

    public class Desc
    {
        public Rich_Text_Nodes[] rich_text_nodes { get; set; }
        public string text { get; set; }
    }

    public class Rich_Text_Nodes
    {
        public string orig_text { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

    public class Major
    {
        public Archive archive { get; set; }
        public string type { get; set; }
    }

    public class Archive
    {
        public string aid { get; set; }
        public Badge badge { get; set; }
        public string bvid { get; set; }
        public string cover { get; set; }
        public string desc { get; set; }
        public int disable_preview { get; set; }
        public string duration_text { get; set; }
        public string jump_url { get; set; }
        public DynamicVideoStat stat { get; set; }
        public string title { get; set; }
        public int type { get; set; }
    }

    public class Badge
    {
        public string bg_color { get; set; }
        public string color { get; set; }
        public string text { get; set; }
    }

    public class DynamicVideoStat
    {
        public string danmaku { get; set; }
        public string play { get; set; }
    }

    public class Module_Interaction
    {
        public Item1[] items { get; set; }
    }

    public class Item1
    {
        public Desc1 desc { get; set; }
        public int type { get; set; }
    }

    public class Desc1
    {
        public Rich_Text_Nodes1[] rich_text_nodes { get; set; }
        public string text { get; set; }
    }

    public class Rich_Text_Nodes1
    {
        public string orig_text { get; set; }
        public string rid { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public Emoji emoji { get; set; }
    }

    public class Emoji
    {
        public string icon_url { get; set; }
        public int size { get; set; }
        public string text { get; set; }
        public int type { get; set; }
    }

    public class Module_More
    {
        public Three_Point_Items[] three_point_items { get; set; }
    }

    public class Three_Point_Items
    {
        public string label { get; set; }
        public string type { get; set; }
    }

    public class Module_Stat
    {
        public Comment comment { get; set; }
        public Forward forward { get; set; }
        public Like like { get; set; }
    }

    public class Comment
    {
        public int count { get; set; }
        public bool forbidden { get; set; }
    }

    public class Forward
    {
        public int count { get; set; }
        public bool forbidden { get; set; }
    }

    public class Like
    {
        public int count { get; set; }
        public bool forbidden { get; set; }
        public bool status { get; set; }
    }

    public class Module_Fold
    {
        public string[] ids { get; set; }
        public string statement { get; set; }
        public int type { get; set; }
        public object[] users { get; set; }
    }
}
