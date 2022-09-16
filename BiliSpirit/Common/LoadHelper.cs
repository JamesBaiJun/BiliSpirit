using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiliSpirit.Common
{
    public class LoadHelper
    {
        /// <summary>
        /// 动态加载列表数据
        /// </summary>
        public static async Task DynamicLoad<T>(IDispatcherService service, Array array, ObservableCollection<T> target) where T : class
        {
            await service.BeginInvoke(new Action(async () =>
            {
                target.Clear();
                foreach (var item in array)
                {
                    target.Add(item as T);
                    await Task.Delay(20);
                }
            }));
        }
    }
}
