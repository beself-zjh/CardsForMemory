using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CardsForMemoryLibrary.ViewModels {
    public class CardInfoViewModel : ViewModelBase {
        private INavigationService navigationService;
        private IPackageService packageService;

        public CardInfoViewModel(INavigationService navigationService, IPackageService packageService) {
            this.navigationService = navigationService;
            this.packageService = packageService;
        }

        private string _question = "";
        public string Question {
            get => _question;
            set => Set(nameof(Question), ref _question, value);
        }

        private string _answer = "";
        public string Answer {
            get => _answer;
            set => Set(nameof(Answer), ref _answer, value);
        }

        public event EventHandler Next;
        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(() => {
            if (Question != "" && Answer != "") {
                //TODO card
                //var status = Status.getInstance();
                //Card card = status["card"] as Card ?? new Card();
                //card.Question = Question;
                //card.Answer = Answer;
                //status["card"] = card;
                Next?.Invoke(this, null);
            }
        }));

        public event EventHandler Cancel;
        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            Cancel?.Invoke(this, null);
        }));

        public void ClearHandler() {
            Next = Cancel = null;
        }
    }
}
