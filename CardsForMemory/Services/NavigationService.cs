using CardsForMemory.Controls;
using CardsForMemory.Pages;
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

        private string lastTag;
        private bool unique(string Tag) {
            if (lastTag == Tag) {
                return false;
            }
            lastTag = Tag;
            return true;
        }

        public void Navigate(string Tag) {
            switch (Tag) {
                case "home":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(HomePage));
                    break;
                case "package":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(PackagePage));
                    break;
                case "remember":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(RememberPage));
                    break;
                case "music":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(MusicPage));
                    break;
                case "setting":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(SettingsPage));
                    break;
                case "package info":
                    new PackageInfo();
                    break;
                case "cards":
                    if (!unique(Tag)) { return; }
                    frame.Navigate(typeof(EditPackagePage));
                    break;
                case "card":
                    new CardInfo();
                    break;
                case "card view":
                    new CardView();
                    break;
                case "query":
                    new QueryNewOld();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
