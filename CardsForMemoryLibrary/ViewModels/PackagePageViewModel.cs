using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using CardsForMemoryLibrary.IServices;

namespace CardsForMemoryLibrary.ViewModels {
    public class PackagePageViewModel : ViewModelBase {
        private IPackageService packageService;
        private INavigationService navigationService;
        private IToastService toastService;

        public PackagePageViewModel(IPackageService packageService, INavigationService navigationService, IToastService toastService) {
            this.packageService = packageService;
            this.navigationService = navigationService;
            this.toastService = toastService;
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

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(async () => {
            var result = await packageService.GetAllPackageAsync();
            if (result.Result != null) {
                Packages = result.Result;
            }
        }));

        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(() => {
            var status = Status.getInstance();
            status["package"] = null;
            navigationService.Navigate("package info");
        }));

        private RelayCommand _editCommand;
        public RelayCommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(() => {
            if (SelectionPackage == null) {
                toastService.Toast("jp`先にカードのカバンを選んでください。");
                return;
            }
            var status = Status.getInstance();
            status["package"] = SelectionPackage;
            navigationService.Navigate("package info");
        }));

        private RelayCommand _playCommand;
        public RelayCommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(() => {
            if (SelectionPackage == null) {
                toastService.Toast("jp`先にカードのカバンを選んでください。");
                return;
            }
            var status = Status.getInstance();
            status["package"] = SelectionPackage;
            navigationService.Navigate("remember");
        }));

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(() => {
            if (SelectionPackage == null) {
                toastService.Toast("jp`先にカードのカバンを選んでください。");
                return;
            }
            packageService.DeletePackageAsync(SelectionPackage.Id);
            LoadedCommand.Execute(null);
            var status = Status.getInstance();
            status["package"] = null;
        }));
    }
}
