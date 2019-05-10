using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.ViewModels {
    public class EditCardsPageViewModel : ViewModelBase {

        private IEnumerable<Card> _cards;
        public IEnumerable<Card> Cards {
            get => _cards;
            set => Set(nameof(Cards), ref _cards, value);
        }

    }
}