using BiliSpirit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Common
{
    public class SoftwareCache
    {
        static SoftwareCache()
        {
            CookieString = File.ReadAllText(".\\Cookie.txt");
        }
        public static CookieContainer Cookie { get; set; }

        public static string CookieString { get; set; }

        public static LoginUser LoginUser { get; set; }
    }
}
