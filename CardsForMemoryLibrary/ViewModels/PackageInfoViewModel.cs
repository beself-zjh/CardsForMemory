using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CardsForMemoryLibrary.ViewModels {
    public class PackageInfoViewModel : ViewModelBase {
        private INavigationService navigationService;
        private IPackageService packageService;
        private IToastService toastService;

        public PackageInfoViewModel(INavigationService navigationService, IPackageService packageService, IToastService toastService) {
            this.navigationService = navigationService;
            this.packageService = packageService;
            this.toastService = toastService;
        }

        private static Action CloseWindow;
        public void InitCloseWindowAction(Action closeWindow) => CloseWindow = closeWindow;

        private string _name;
        public string Name {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private string _author;
        public string Author {
            get => _author;
            set => Set(nameof(Author), ref _author, value);
        }

        private string _description;
        public string Description {
            get => _description;
            set => Set(nameof(Description), ref _description, value);
        }

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(async () => {
            if (Name != "" && Author != "" && Description != "") {
                var status = Status.s;
                //通过status["package"]判断是Add还是Edit
                if (status["package"] is Package package) {
                    package.Name = Name;
                    package.Author = Author;
                    package.Description = Description;
                    var result = await packageService.EditPackageAsync(package);
                    if (result.Status != ServiceResultStatus.OK) {
                        toastService.Toast(result.Message,5);
                    }
                    status["package"] = package;
                } else {
                    var result = await packageService.AddPackageAsync(Name, Author, Description);
                    if (result.Result != null) {
                        status["package"] = result.Result;
                    } else {
                        toastService.Toast(result.Message, 5);
                        return;
                    }
                }
                CloseWindow?.Invoke();
                navigationService.Navigate("cards");
            }
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            CloseWindow?.Invoke();
        }));
    }
}
