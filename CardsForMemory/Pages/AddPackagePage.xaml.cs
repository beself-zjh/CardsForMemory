using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class AddPackagePage : Page {
        CardsForMemoryLibrary.ViewModels.AddPackagePageViewModel vm;

        public AddPackagePage() {
            this.InitializeComponent();
            vm = CardsForMemory.Locator.ViewModelLocator.Instance.AddPackagePageViewModel;
        }
    }
}
