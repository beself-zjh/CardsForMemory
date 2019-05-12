﻿using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class MainPage : Page {
        MainPageViewModel vm = ViewModelLocator.Instance.MainPageViewModel;

        public MainPage() {
            InitializeComponent();
            ViewModelLocator.Instance.INavigationService.Init(ContentFrame, RememberPageName);
        }

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
