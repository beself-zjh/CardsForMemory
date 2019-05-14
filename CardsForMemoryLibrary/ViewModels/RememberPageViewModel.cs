using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.ViewModels {
    public class RememberPageViewModel : ViewModelBase {
        private IPackageService packageService;
        private IReviseService reviseService;
        private IFeedbackService feedbackService;
        private IToastService toastService;

        public RememberPageViewModel(IPackageService packageService, IReviseService reviseService, IFeedbackService feedbackService, IToastService toastService) {
            this.packageService = packageService;
            this.reviseService = reviseService;
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

        private RelayCommand _onLoaded;
        public RelayCommand OnLoaded => _onLoaded ?? (_onLoaded = new RelayCommand(() => {
            Time = 0;
            AnswerVis = false;
            if (status["cardi"] is int cardi) {
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
                toastService.Toast("jp`これは簡単じゃないですか？", 3);
                //TODO easy
                AnswerVis = true;
            }
        }));

        private RelayCommand _normalCommand;
        public RelayCommand NormalCommand => _normalCommand ?? (_normalCommand = new RelayCommand(() => {
            if (Answer != "") {
                toastService.Toast("jp`まあ、すぐには思いつきませんね。", 3);
                //TODO normal
                AnswerVis = true;
            }
        }));

        private RelayCommand _diffCommand;
        public RelayCommand DiffCommand => _diffCommand ?? (_diffCommand = new RelayCommand(() => {
            if (Answer != "") {
                toastService.Toast("jp`うわ、全然思い出せません。", 3);
                //TODO diff
                AnswerVis = true;
            }
        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(() => {
            AnswerVis = false;
            if (status["cardi"] is int cardi) {
                status["cardi"] = --cardi;
                if (cardi == 0) {
                    Question = "恭喜,题答完了";
                    Answer = "";
                    toastService.Toast("jp`おめでとうございます。問題は全部答えました。", 4);
                } else {
                    if (status["cards"] is List<Card> cards) {
                        Question = cards[cardi - 1].Question;
                        Answer = cards[cardi - 1].Answer;
                        toastService.Toast("jp`はい、次の問題");
                    }
                }
            }
        }));
    }
}
