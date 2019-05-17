using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.ViewModels {
    public class RememberPageViewModel : ViewModelBase {
        private IFeedbackService feedbackService;
        private IToastService toastService;

        public RememberPageViewModel(IFeedbackService feedbackService, IToastService toastService) {
            this.feedbackService = feedbackService;
            this.toastService = toastService;
        }

        private bool _visibility;
        public bool Visibility {
            get => _visibility;
            set => Set(nameof(Visibility), ref _visibility, value);
        }

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

        private int _time = 0;
        public int Time {
            get => _time;
            set => Set(nameof(Time), ref _time, value);
        }

        private const int ClockWidth = 200;//时钟的宽度
        private const int RingWidth = 10;//时钟圆环的宽度 limit:0<RingWidth<100
        private int _style = ClockWidth * 100 + RingWidth;
        public int Style {
            get => _style;
            set => Set(nameof(Style), ref _style, value);
        }

        private Status status = Status.s;

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(() => {
            AnswerVis = false;
            if (status["cardi"] is int cardi) {
                if (cardi == 0) {
                    return;
                }
                if (status["cards"] is List<Card> cards) {
                    Question = cards[cardi - 1].Question;
                    Answer = cards[cardi - 1].Answer;
                    Visibility = true;
                    return;
                }
            }
            Visibility = false;
        }));
        
        private bool _answerVis;
        public bool AnswerVis {
            get => _answerVis;
            set => Set(nameof(AnswerVis), ref _answerVis, value);
        }

        private RelayCommand _easyCommand;
        public RelayCommand EasyCommand => _easyCommand ?? (_easyCommand = new RelayCommand(() => {
            if (Answer != "") {
                toastService.Toast("zh`这是不是很简单.", 3);
                if (status["cardi"] is int cardi) {
                    if (status["cards"] is List<Card> cards) {
                        feedbackService.isEasy(cards[cardi - 1]);
                    }
                }
                AnswerVis = true;
            }
        }));

        private RelayCommand _normalCommand;
        public RelayCommand NormalCommand => _normalCommand ?? (_normalCommand = new RelayCommand(() => {
            if (Answer != "") {
                toastService.Toast("zh`呀,这个一下子想不起来呢", 3);
                if (status["cardi"] is int cardi) {
                    if (status["cards"] is List<Card> cards) {
                        feedbackService.isNormal(cards[cardi - 1]);
                    }
                }
                AnswerVis = true;
            }
        }));

        private RelayCommand _diffCommand;
        public RelayCommand DiffCommand => _diffCommand ?? (_diffCommand = new RelayCommand(() => {
            if (Answer != "") {
                toastService.Toast("jp`哇,完全想不出来", 3);
                if (status["cardi"] is int cardi) {
                    if (status["cards"] is List<Card> cards) {
                        feedbackService.isDifficult(cards[cardi - 1]);
                    }
                }
                AnswerVis = true;
            }
        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(() => {
            AnswerVis = false;
            if (status["cardi"] is int cardi) {
                status["cardi"] = --cardi;
                if (cardi == 0) {
                    status["time"] = 0;
                    Question = "恭喜,题答完了";
                    Answer = "";
                    toastService.Toast("zh`恭喜,题答完了", 4);
                } else {
                    if (status["cards"] is List<Card> cards) {
                        Question = cards[cardi - 1].Question;
                        Answer = cards[cardi - 1].Answer;
                        toastService.Toast("zh`好,下个问题");
                    }
                }
            }
        }));
    }
}
