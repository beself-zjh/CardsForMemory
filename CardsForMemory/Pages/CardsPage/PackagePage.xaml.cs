using CardsForMemory.Locator;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class PackagePage : Page {
        private PackagePageViewModel vm = ViewModelLocator.Instance.PackagePageViewModel;

        public PackagePage() {
            InitializeComponent();
        }

        private void ListViewSelectChangeHandler(object sender, SelectionChangedEventArgs e) {
            vm.SelectionPackage = sender is ListView listView ? listView.SelectedItem is Package item ? item : null : null;
        }
    }
}
