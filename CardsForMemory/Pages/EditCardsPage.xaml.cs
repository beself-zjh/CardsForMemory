using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class EditCardsPage : Page {
        private EditCardsPageViewModel vm = ViewModelLocator.Instance.EditCardsPageViewModel;

        public EditCardsPage() {
            InitializeComponent();
        }
    }
}
