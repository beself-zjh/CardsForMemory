using CardsForMemory.Services;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class CardsPage : Page {
        private CardsPageViewModel vm;

        public CardsPage() {
            this.InitializeComponent();
            CardsPageEditBtn.IsEnabled = false;
            CardsPagePlayBtn.IsEnabled = false;
            DataContext = Locator.ViewModelLocator.Instance.CardsPageViewModel;
            vm = Locator.ViewModelLocator.Instance.CardsPageViewModel;
        }

        private void CardsPageListViewSelectionChangeHandler(object sender, SelectionChangedEventArgs e) {
            var item = (sender as ListView).SelectedItem as Package;
            if (item == null) {
                return;
            }
            item.Name = "Name: " + item.Name;
            item.Author = "Author: " + item.Author;
            item.Description = "Description: " + item.Description;
            (DataContext as CardsPageViewModel).SelectionPackage = item;
            CardsPageEditBtn.IsEnabled = true;
            CardsPagePlayBtn.IsEnabled = true;
        }

        private void CardsPageAddBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            if (null == CardsPageAddBtn.Flyout) {
                CardsPageAddBtn.Flyout = new Flyout { Content = new TextBlock { Text = "Not implement Button!" } };
            }
        }

        private void CardsPageEditBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e) {

        }

        private void CardsPagePlayBtnClickHandler(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            var gv = CardsForMemoryLibrary.Status.getInstance();

            this.Frame.Navigate(typeof(RememberPage), 1);
        }

        //private void StackPanel_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    (DataContext as CardsPageViewModel).StackPanelLoaded();
        //}
    }
}
