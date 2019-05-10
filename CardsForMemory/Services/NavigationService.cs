using CardsForMemory.Pages;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Services {
    public class NavigationService : CardsForMemoryLibrary.IServices.INavigationService {
        public static Frame frame;
        public static NavigationViewItem navigationViewItem;

        public void Init(object Frame, object NavigationViewItem) {
            if (frame == null && navigationViewItem == null) {
                frame = Frame as Frame;
                navigationViewItem = NavigationViewItem as NavigationViewItem;
            }
        }

        public void Set_RememberPageName(string append) {
            navigationViewItem.Content = "Remember" + append;
        }

        public void Navigate(string Tag) {
            switch (Tag) {
                case "home":
                    frame.Navigate(typeof(HomePage));
                    break;
                case "cards":
                    frame.Navigate(typeof(CardsPage));
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
                case "add package":
                    frame.Navigate(typeof(AddPackagePage));
                    break;
                case "edit package":
                    frame.Navigate(typeof(EditCardsPage));
                    break;
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}
