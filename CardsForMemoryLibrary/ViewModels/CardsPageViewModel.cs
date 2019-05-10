using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using CardsForMemoryLibrary.IServices;

namespace CardsForMemoryLibrary.ViewModels {
    public class CardsPageViewModel : ViewModelBase {
        private IPackageService packageService;
        private INavigationService navigationService;

        public CardsPageViewModel(IPackageService packageService, INavigationService navigationService) {
            this.packageService = packageService;
            this.navigationService = navigationService;
        }

        private IEnumerable<Package> _packages;
        public IEnumerable<Package> Packages {
            get => _packages;
            set => Set(nameof(Packages), ref _packages, value);
        }

        private Package _selectionPackage;
        public Package SelectionPackage {
            get => _selectionPackage;
            set => Set(nameof(SelectionPackage), ref _selectionPackage, value);
        }

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand => _refreshCommand ?? (_refreshCommand = new RelayCommand(async () => {
            var result = await packageService.GetAsyncAllPackage();
            if (result.Result != null) {
                Packages = result.Result;
            }
        }));

        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(() => {
            navigationService.Navigate("add package");
        }));

        private RelayCommand _playCommand;
        public RelayCommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(async () => {
            var status = Status.getInstance();
            var result = await packageService.GetAsyncPackage(_selectionPackage.Id);
            if (result.Result != null) {
                status["package"] = result.Result;
                status["package.i"] = 0;
            }
        }));
    }
}
