﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using CardsForMemoryLibrary.IServices;

namespace CardsForMemoryLibrary.ViewModels {
    public class PackagePageViewModel : ViewModelBase {
        private IPackageService packageService;
        private ICardService cardService;
        private INavigationService navigationService;
        private IToastService toastService;

        public PackagePageViewModel(IPackageService packageService, ICardService cardService, INavigationService navigationService, IToastService toastService) {
            this.packageService = packageService;
            this.cardService = cardService;
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

        private Status status = Status.s;
        private bool IsSelected() {
            if (SelectionPackage == null) {
                toastService.Toast("jp`先にカードのカバンを選んでください。");
            }
            return SelectionPackage != null;
        }
        //是不是正在记忆卡片
        private bool IsWorked() {
            //有cardi才有可能处于工作状态
            if (status["cardi"] is int cardi) {
                if (cardi > 0) {
                    return status["cards"] == SelectionPackage;
                }
                return false;
            }
            return false;
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
            status["package"] = null;
            navigationService.Navigate("package info");
        }));

        private RelayCommand _editCommand;
        public RelayCommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(() => {
            if (!IsSelected()) { return; }
            if (IsWorked()) { return; }
            status["package"] = SelectionPackage;
            navigationService.Navigate("package info");
        }));

        private RelayCommand _editCardsCommand;
        public RelayCommand EditCardsCommand => _editCardsCommand ?? (_editCardsCommand = new RelayCommand(() => {
            if (!IsSelected()) { return; }
            if (IsWorked()) { return; }
            status["package"] = SelectionPackage;
            navigationService.Navigate("cards");
        }));

        private RelayCommand _playCommand;
        public RelayCommand PlayCommand => _playCommand ?? (_playCommand = new RelayCommand(async () => {
            if (!IsSelected()) { return; }
            if (IsWorked()) { return; }
            status["package"] = SelectionPackage;
            var cards = (await cardService.GetCardsAsync(SelectionPackage.Id)).Result;
            status["cards"] = cards;
            status["cardi"] = cards.Count;
            navigationService.Navigate("remember");
        }));

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(() => {
            if (!IsSelected()) { return; }
            if (IsWorked()) { return; }
            packageService.DeletePackageAsync(SelectionPackage.Id);
            LoadedCommand.Execute(null);
            status["package"] = null;
            status["cards"] = null;
            status["cardi"] = null;
        }));
    }
}