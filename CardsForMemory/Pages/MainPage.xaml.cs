using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages
{
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// 又开始剽窃了
        /// </summary> 
        public static MainPage Current;



        //LWH

        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 当MainPage的NavView的子项被点中时触发此函数
        /// </summary>
        private void MainPageNavViewItemInvoke(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            // 先看是不是设置被点中了
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                // 找到被点中的NavigationViewItem
                // 特征是它的Content = args.InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>()
                    .First(x => (string)x.Content == (string)args.InvokedItem);
                switch (item.Tag)
                {
                    case "home":
                        ContentFrame.Navigate(typeof(HomePage));
                        break;

                    case "cards":
                        ContentFrame.Navigate(typeof(CardsPage));
                        break;

                    case "remember":
                        ContentFrame.Navigate(typeof(RememberPage));
                        break;

                    case "music":
                        ContentFrame.Navigate(typeof(MusicPage));
                        break;

                        //HXH
                    case "exam":
                        ContentFrame.Navigate(typeof(ExamPage));
                        break;
                }
            }
        }

        /// <summary>
        /// 当MainPage的NavView加载完毕后调用此函数
        /// </summary>
        private void MainPageNavViewLoaded(object sender, RoutedEventArgs e)
        {
            // 导航到主页
            ContentFrame.Navigate(typeof(HomePage));
        }
    }
}
