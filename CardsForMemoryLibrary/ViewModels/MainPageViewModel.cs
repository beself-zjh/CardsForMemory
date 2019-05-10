using CardsForMemoryLibrary.IServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class MainPageViewModel : ViewModelBase {
        private INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        private RelayCommand _onLoaded;
        public RelayCommand OnLoaded => _onLoaded ?? (_onLoaded = new RelayCommand(() => {
            navigationService.Navigate("home");
        }));

        public void Navigate(string Tag) {
            navigationService.Navigate(Tag);
        }
    }
}
