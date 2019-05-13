using CardsForMemory.Locator;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class EditPackagePage : Page {
        private EditPackagePageViewModel vm = ViewModelLocator.Instance.EditPackagePageViewModel;

        public EditPackagePage() {
            InitializeComponent();
        }

        private void ListViewSelectionChanged(object sender, SelectionChangedEventArgs e) {
            vm.SelectionCard = sender is ListView listView ? listView.SelectedItem is Card item ? item : null : null;
        }
    }
}
