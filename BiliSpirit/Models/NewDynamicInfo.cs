using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Models
{
    public class NewDynamicInfo
    {
        public int code { get; set; }
        public string msg { get; set; }
        public string message { get; set; }
        public NewDynamicData data { get; set; }
    }

    public class NewDynamicData
    {
        public int video_num { get; set; }
        public int article_num { get; set; }
        public int alltype_num { get; set; }
        public int _gt_ { get; set; }
    }
}
