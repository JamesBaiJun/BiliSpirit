using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class LoginUser
    {
        public int code { get; set; }
        public string message { get; set; }
        public int ttl { get; set; }
        public UserData data { get; set; }
    }

    public class UserData
    {
        public string mid { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string face { get; set; }
        public string sign { get; set; }
        public int rank { get; set; }
        public int level { get; set; }
        public int jointime { get; set; }
        public int moral { get; set; }
        public int silence { get; set; }
        public int email_status { get; set; }
        public int tel_status { get; set; }
        public int identification { get; set; }
        public Vip vip { get; set; }
        public Pendant pendant { get; set; }
        public Nameplate nameplate { get; set; }
        public Official official { get; set; }
        public int birthday { get; set; }
        public int is_tourist { get; set; }
        public int is_fake_account { get; set; }
        public int pin_prompting { get; set; }
        public int is_deleted { get; set; }
        public int in_reg_audit { get; set; }
        public bool is_rip_user { get; set; }
        public Profession profession { get; set; }
        public int face_nft { get; set; }
        public int face_nft_new { get; set; }
        public int is_senior_member { get; set; }
        public Level_Exp level_exp { get; set; }
        public float coins { get; set; }
        public int following { get; set; }
        public int follower { get; set; }
    }

    public class Vip
    {
        public int type { get; set; }
        public int status { get; set; }
        public long due_date { get; set; }
        public int vip_pay_type { get; set; }
        public int theme_type { get; set; }
        public Label label { get; set; }
        public int avatar_subscript { get; set; }
        public string nickname_color { get; set; }
        public int role { get; set; }
        public string avatar_subscript_url { get; set; }
        public int tv_vip_status { get; set; }
        public int tv_vip_pay_type { get; set; }
    }

    public class Label
    {
        public string path { get; set; }
        public string text { get; set; }
        public string label_theme { get; set; }
        public string text_color { get; set; }
        public int bg_style { get; set; }
        public string bg_color { get; set; }
        public string border_color { get; set; }
        public bool use_img_label { get; set; }
        public string img_label_uri_hans { get; set; }
        public string img_label_uri_hant { get; set; }
        public string img_label_uri_hans_static { get; set; }
        public string img_label_uri_hant_static { get; set; }
    }

    public class Pendant
    {
        public int pid { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public int expire { get; set; }
        public string image_enhance { get; set; }
        public string image_enhance_frame { get; set; }
    }

    public class Nameplate
    {
        public int nid { get; set; }
        public string name { get; set; }
        public string image { get; set; }
        public string image_small { get; set; }
        public string level { get; set; }
        public string condition { get; set; }
    }

    public class Official
    {
        public int role { get; set; }
        public string title { get; set; }
        public string desc { get; set; }
        public int type { get; set; }
    }

    public class Profession
    {
        public int id { get; set; }
        public string name { get; set; }
        public string show_name { get; set; }
        public int is_show { get; set; }
        public string category_one { get; set; }
        public string realname { get; set; }
        public string title { get; set; }
        public string department { get; set; }
    }

    public class Level_Exp
    {
        public int current_level { get; set; }
        public int current_min { get; set; }
        public int current_exp { get; set; }
        public int next_exp { get; set; }
    }

}
