using CardsForMemory.Controls;
using CardsForMemory.Pages;
using CardsForMemoryLibrary;
using CardsForMemoryLibrary.IServices;
using System;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Services {
    public class NavigationService : INavigationService {
        public static Frame frame;
        public static NavigationViewItem navigationViewItem;

        public void Init(object Frame, object NavigationViewItem) {
            if (frame == null && navigationViewItem == null) {
                frame = Frame as Frame;
                navigationViewItem = NavigationViewItem as NavigationViewItem;
            }
        }

        public void Set_RememberPageName(string append) {
            navigationViewItem.Content = "Remember " + append;
        }

        public void Navigate(string Tag) {
            switch (Tag) {
                case "home":
                    frame.Navigate(typeof(HomePage));
                    break;
                case "package":
                    frame.Navigate(typeof(PackagePage));
                    break;
                case "remember":
                    frame.Navigate(typeof(RememberPage));
                    break;
                case "music":
                    frame.Navigate(typeof(MusicPage));
                    break;
                case "setting":
                    frame.Navigate(typeof(SettingsPage));
                    break;
                case "package info":
                    new PackageInfo();
                    break;
                case "cards":
                    frame.Navigate(typeof(EditPackagePage));
                    break;
                case "card":
                    new CardInfo();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
