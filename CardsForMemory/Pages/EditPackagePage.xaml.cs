using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class EditPackagePage : Page {
        private EditPackagePageViewModel vm = ViewModelLocator.Instance.EditPackagePageViewModel;

        public EditPackagePage() {
            InitializeComponent();
            Loaded += async (s, e) => {
                await vm.UpdateCards();
            };
        }
    }
}
