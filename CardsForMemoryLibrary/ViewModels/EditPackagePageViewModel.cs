using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.ViewModels {
    public class EditPackagePageViewModel : ViewModelBase {
        ICardService ICardService;
        INavigationService INavigationService;

        public EditPackagePageViewModel(ICardService ICardService, INavigationService INavigationService) {
            this.ICardService = ICardService;
            this.INavigationService = INavigationService;
        }

        public async Task UpdateCards() {
            var status = Status.getInstance();
            if (status["package"] != null) {
                Cards = (await ICardService.GetAsyncCards((status["package"] as Package).Id))?.Result;
            }
        }

        private IEnumerable<Card> _cards;
        public IEnumerable<Card> Cards {
            get => _cards;
            set => Set(nameof(Cards), ref _cards, value);
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ?? (_addCommand = new RelayCommand(() => {
            var status = Status.getInstance();
            status["card action"] = new System.Action(() => {
                List<Card> NewCards = new List<Card>(Cards as List<Card>);
                NewCards.Add(status["card"] as Card);
                Cards = NewCards;
            });
            INavigationService.Navigate("card");
        }));
        
        private RelayCommand _editCommand;
        public RelayCommand EditCommand => _editCommand ?? (_editCommand = new RelayCommand(() => {
            //TODO
        }));
        
        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new RelayCommand(() => {
            //TODO
        }));
        
        private RelayCommand _previewCommand;
        public RelayCommand PreviewCommand => _previewCommand ?? (_previewCommand = new RelayCommand(() => {
            //TODO
        }));
    }
}