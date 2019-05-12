using CardsForMemory.Locator;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class CardsPage : Page {
        private CardsPageViewModel vm = ViewModelLocator.Instance.CardsPageViewModel;

        public CardsPage() {
            InitializeComponent();
        }

        private void ListViewSelectChangeHandler(object sender, SelectionChangedEventArgs e) {
            vm.SelectionPackage = sender is ListView listView ? listView.SelectedItem is Package item ? item : null : null;
        }
    }
}
