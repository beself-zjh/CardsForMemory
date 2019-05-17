using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CardsForMemoryLibrary.ViewModels {
    public class CardViewViewModel : ViewModelBase {
        private INavigationService navigationService;
        private ICardService cardService;
        private IToastService toastService;

        public CardViewViewModel(INavigationService navigationService, ICardService cardService, IToastService toastService) {
            this.navigationService = navigationService;
            this.cardService = cardService;
            this.toastService = toastService;
        }

        private static Action CloseWindow;
        public void InitCloseWindowAction(Action closeWindow) => CloseWindow = closeWindow;

        private string question;
        private string answer;

        private string _showText;
        public string ShowText {
            get => _showText;
            set => Set(nameof(ShowText), ref _showText, value);
        }

        private string _buttonText;
        public string ButtonText {
            get => _buttonText;
            set => Set(nameof(ButtonText), ref _buttonText, value);
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() => {
            Status status = Status.s;
            if (status["card"] is Card card) {
                question = card.Question;
                answer = card.Answer;
                ShowText = question;
                ButtonText = "看答案";
            } else {
                toastService.Toast("DEBUG:status[\"card\"]不是Card!!");
                CloseWindow?.Invoke();
            }
        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(() => {
            if(ButtonText=="See Answer") {
                ShowText = answer;
                ButtonText = "看问题";
            } else {
                ShowText = question;
                ButtonText = "看答案";
            }
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            CloseWindow?.Invoke();
        }));
    }
}
