using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BiliSpirit.Common
{
    public class ExpolerHelper
    {
        public static void OuterVisit(string url)
        {
            dynamic? kstr;
            string s;
            try
            {
                // 从注册表中读取默认浏览器可执行文件路径
                RegistryKey? key2 = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
                RegistryKey? key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice\");
                if (key != null)
                {
                    kstr = key.GetValue("ProgId");
                    if (kstr != null)
                    {
                        s = kstr.ToString();
                        // "ChromeHTML","MSEdgeHTM" etc.
                        if (Registry.GetValue(@"HKEY_CLASSES_ROOT\" + s + @"\shell\open\command", null, null) is string path)
                        {
                            var split = path.Split('"');
                            path = split.Length >= 2 ? split[1] : "";
                            if (path != "")
                            {
                                Process.Start(path, url);
                                return;
                            }
                        }
                    }
                }
                if (key2 != null)
                {
                    kstr = key2.GetValue("");
                    if (kstr != null)
                    {
                        s = kstr.ToString();
                        var lastIndex = s.IndexOf(".exe", StringComparison.Ordinal);
                        if (lastIndex == -1)
                        {
                            lastIndex = s.IndexOf(".EXE", StringComparison.Ordinal);
                        }
                        var path = s.Substring(1, lastIndex + 3);
                        var result1 = Process.Start(path, url);
                        if (result1 == null)
                        {
                            var result2 = Process.Start("explorer.exe", url);
                            if (result2 == null)
                            {
                                Process.Start(url);
                            }
                        }
                    }
                }
                else
                {
                    var result2 = Process.Start("explorer.exe", url);
                    if (result2 == null)
                    {
                        Process.Start(url);
                    }
                }
            }
            catch
            {
                Clipboard.SetText(url);
            }
        }
    }
}
