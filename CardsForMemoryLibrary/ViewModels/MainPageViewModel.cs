using CardsForMemoryLibrary.IServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class MainPageViewModel : ViewModelBase {
        private INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService) {
            this.navigationService = navigationService;
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() => {
            navigationService.Navigate("package");
        }));

        public void Navigate(string Tag) {
            navigationService.Navigate(Tag);
        }
    }
}
