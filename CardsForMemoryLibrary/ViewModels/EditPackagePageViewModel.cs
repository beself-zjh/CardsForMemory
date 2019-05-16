using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.ViewModels {
    public class EditPackagePageViewModel : ViewModelBase {
        ICardService cardService;
        INavigationService navigationService;
        IToastService toastService;

        public EditPackagePageViewModel(ICardService cardService, INavigationService navigationService, IToastService toastService) {
            this.cardService = cardService;
            this.navigationService = navigationService;
            this.toastService = toastService;
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(async () => {
            var status = Status.s;
            if (status["package"] is Package package) {
                int packageId = package.Id;
                Cards = (await cardService.GetCardsAsync(packageId)).Result;
            } else {
                toastService.Toast("zh`怎么回事小老弟?到了这个页面竟然package是空的???");
            }
        }));

        private IEnumerable<Card> _cards;
        public IEnumerable<Card> Cards {
            get => _cards;
            set => Set(nameof(Cards), ref _cards, value);
        }

        private Card _selectionCard;
        public Card SelectionCard {
            get => _selectionCard;
            set => Set(nameof(SelectionCard), ref _selectionCard, value);
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(() => {
            var status = Status.s;
            status["card"] = null;
            navigationService.Navigate("card");
        }));

        private RelayCommand _editCommand;
        public RelayCommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(() => {
            if (SelectionCard == null) {
                toastService.Toast("jp`まずカードを選んでください。");
                return;
            }
            var status = Status.s;
            status["card"] = SelectionCard;
            navigationService.Navigate("card");
        }));

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(() => {
            if (SelectionCard == null) {
                toastService.Toast("jp`まずカードを選んでください。");
                return;
            }
            cardService.DeleteCardAsync(SelectionCard.Id);
            LoadedCommand.Execute(null);
            var status = Status.s;
            status["card"] = null;
        }));

        private RelayCommand _previewCommand;
        public RelayCommand PreviewCommand => _previewCommand ?? (_previewCommand = new RelayCommand(() => {
            if (SelectionCard == null) {
                toastService.Toast("jp`まずカードを選んでください。");
                return;
            }
            var status = Status.s;
            status["card"] = SelectionCard;
            navigationService.Navigate("card view");
        }));
    }
}