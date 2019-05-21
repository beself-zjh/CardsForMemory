using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;

namespace CardsForMemoryLibrary.ViewModels {
    public class CardInfoViewModel : ViewModelBase {
        private INavigationService navigationService;
        private ICardService cardService;
        private IToastService toastService;

        public CardInfoViewModel(INavigationService navigationService, ICardService cardService, IToastService toastService) {
            this.navigationService = navigationService;
            this.cardService = cardService;
            this.toastService = toastService;
        }

        private static Action CloseWindow;
        public void InitCloseWindowAction(Action closeWindow) => CloseWindow = closeWindow;

        private string _question;
        public string Question {
            get => _question;
            set => Set(nameof(Question), ref _question, value);
        }

        private string _answer;
        public string Answer {
            get => _answer;
            set => Set(nameof(Answer), ref _answer, value);
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() => {
            //处理状态中的card参数
            if (Status.s["card"] is Card card) {
                Question = card.Question;
                Answer = card.Answer;
            } else {
                Question = "";
                Answer = "";
            }

        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(async () => {
            if (Question != "" && Answer != "") {
                var status = Status.s;
                //通过status["card"]判断是Add还是Edit
                if (status["card"] is Card card) {
                    card.Question = Question;
                    card.Answer = Answer;
                    var result = await cardService.EditCardAsync(card);
                    if (result.Status != ServiceResultStatus.OK) {
                        toastService.Toast(result.Message, 5);
                    }
                    status["card"] = card;
                } else {
                    if (status["package"] is Package package) {
                        var result = await cardService.AddCardAsync(package.Id, Question, Answer);
                        if (result.Result != null) {
                            status["card"] = result.Result;
                        } else {
                            toastService.Toast(result.Message, 5);
                            return;
                        }
                    } else {
                        toastService.Toast("zh`你他娘的怎么回事?小老弟,都到了这个页面怎么Package是空的?");
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
