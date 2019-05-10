using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class EditCardsPage : Page {
        public EditCardsPage() {
            this.InitializeComponent();
            ViewModelSetup();
        }

        #region 设置vm
        /// <summary>
        /// 此页ViewModel,在xaml里用,所以起一个vm的名字而不是viewModel
        /// </summary>
        private CardsForMemoryLibrary.ViewModels.EditCardsPageViewModel vm;
        /// <summary>
        /// 设置ViewModel
        /// </summary>
        private void ViewModelSetup() {
            vm = Locator.ViewModelLocator.Instance.EditCardsPageViewModel;
        }
        #endregion

    }
}
