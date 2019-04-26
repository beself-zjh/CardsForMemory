using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using CardsForMemoryLibrary.IServices;

namespace CardsForMemoryLibrary.ViewModels
{
    public class CardsPageViewModel : ViewModelBase
    {
        private IPackageService _packageService;

        public CardsPageViewModel(IPackageService packageService)
        {
            _packageService = packageService;
        }

        private IEnumerable<Package> _packages;
        public IEnumerable<Package> Packages
        {
            get => _packages;
            set => Set(nameof(Packages), ref _packages, value);
        }

        private Package _selectionPackage;
        public Package SelectionPackage
        {
            get => _selectionPackage;
            set => Set(nameof(SelectionPackage), ref _selectionPackage, value);
        }

        private RelayCommand _refreshCommand;
        public RelayCommand RefreshCommand =>
            _refreshCommand ?? (_refreshCommand = new RelayCommand(async () =>
            {
                //await _packageService.InsertAsyncPackage(new Package
                //{
                //    Author = "asasdssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssd",
                //    CreatedTime = System.DateTime.Now,
                //    Description = "asasdssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssd",
                //    Name = "asasdssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssd",
                //    UpdateTime = System.DateTime.Now
                //});
                var result = await _packageService.GetAsyncAllPackage();
                if (result.Result != null)
                {
                    Packages = result.Result;
                }
            }));

        //async public void StackPanelLoaded()
        //{
        //    var result = await _packageService.GetAsyncAllPackage();
        //    if (result.Result != null)
        //    {
        //        Packages = result.Result;
        //    }
        //}

    }
}
