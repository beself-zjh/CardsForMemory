using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class MainPage : Page {
        CardsForMemoryLibrary.ViewModels.MainPageViewModel vm;

        public MainPage() {
            this.InitializeComponent();
            CardsForMemory.Locator.ViewModelLocator.Instance.INavigationService.Init(ContentFrame, RememberPageName);
            vm = CardsForMemory.Locator.ViewModelLocator.Instance.MainPageViewModel;
        }

        /// <summary>
        /// 当MainPage的NavView的子项被点中时触发此函数
        /// </summary>
        private void MainPageNavViewItemInvoke(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
            // 先看是不是设置被点中了
            if (args.IsSettingsInvoked) {
                vm.Navigate("setting");
            } else {
                var navigationViewItems = sender.MenuItems.OfType<NavigationViewItem>();
                var tag = navigationViewItems.First(x => {
                    return x.Content as string == args.InvokedItem as string;
                });
                vm.Navigate(tag.Tag as string);
            }
        }
    }
}
