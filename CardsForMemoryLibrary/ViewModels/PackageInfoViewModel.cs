using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class PackageInfoViewModel : ViewModelBase {
        private INavigationService navigationService;
        private IPackageService packageService;

        public PackageInfoViewModel(INavigationService navigationService, IPackageService packageService) {
            this.navigationService = navigationService;
            this.packageService = packageService;
        }

        private static System.Action closeWindow;
        public void initCloseWindowAction(System.Action closeWindow) =>
            PackageInfoViewModel.closeWindow = closeWindow;

        private string _name = "";
        public string Name {
            get => _name;
            set => Set(nameof(Name), ref _name, value);
        }

        private string _author = "";
        public string Author {
            get => _author;
            set => Set(nameof(Author), ref _author, value);
        }

        private string _description = "";
        public string Description {
            get => _description;
            set => Set(nameof(Description), ref _description, value);
        }

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(async () => {
            //TODO
            if (Name != "" && Author != "" && Description != "") {
                var status = Status.getInstance();
                await packageService.InsertAsyncPackage(new Package {
                    Name = Name,
                    Author = Author,
                    Description = Description
                });
                var result = await packageService.GetAsyncAllPackage();
                if (result.Result != null) {
                    int id = 0;
                    result.Result.ForEach((Package p) => {
                        id = id > p.Id ? id : p.Id;
                    });
                    status["new.id"] = id;
                }
                closeWindow?.Invoke();
                navigationService.Navigate("edit package");
            }
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            closeWindow?.Invoke();
        }));
    }
}
