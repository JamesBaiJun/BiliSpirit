using DevExpress.Mvvm.UI.Interactivity;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace BiliSpirit.Common
{
    public class FadeAnimateItemsBehavior : Behavior<ItemsControl>
    {
        public DoubleAnimation Animation { get; set; }

        public ThicknessAnimation TaAnimation { get; set; }

        public TimeSpan Tick { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += new System.Windows.RoutedEventHandler(AssociatedObject_Loaded);
        }

        void AssociatedObject_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            IEnumerable<ListBoxItem> items;
            if (AssociatedObject.ItemsSource == null)
            {
                items = AssociatedObject.Items.Cast<ListBoxItem>();
            }
            else
            {
                var itemsSource = AssociatedObject.ItemsSource;
                if (itemsSource is INotifyCollectionChanged)
                {
                    var collection = itemsSource as INotifyCollectionChanged;
                    collection.CollectionChanged += (s, cce) =>
                    {
                        if (cce.Action == NotifyCollectionChangedAction.Add)
                        {
                            var itemContainer = AssociatedObject.ItemContainerGenerator.ContainerFromItem(cce.NewItems[0]) as ContentPresenter;
                            if (itemContainer != null)
                            {
                                itemContainer.BeginAnimation(ContentPresenter.OpacityProperty, Animation);
                                itemContainer.BeginAnimation(ContentPresenter.MarginProperty, TaAnimation);
                            }
                        }
                    };

                }
                ListBoxItem[] itemsSub = new ListBoxItem[AssociatedObject.Items.Count];
                for (int i = 0; i < itemsSub.Length; i++)
                {
                    itemsSub[i] = AssociatedObject.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
                }
                items = itemsSub;
            }
            foreach (var item in items)
            {
                if (item!=null)
                    item.Opacity = 0;
            }
            var enumerator = items.GetEnumerator();
            if (enumerator.MoveNext())
            {
                DispatcherTimer timer = new DispatcherTimer() { Interval = Tick };
                timer.Tick += (s, timerE) =>
                {
                    var item = enumerator.Current;
                    if (item != null)
                    {
                        item.BeginAnimation(ListBoxItem.OpacityProperty, Animation);
                        item.BeginAnimation(ListBoxItem.MarginProperty, TaAnimation);
                    }

                    if (!enumerator.MoveNext())
                    {
                        timer.Stop();
                    }
                };
                timer.Start();
            }
        }
    }
}
