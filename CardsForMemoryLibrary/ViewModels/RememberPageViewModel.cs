using System.Collections.Generic;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.IServices;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class RememberPageViewModel : ViewModelBase {
        private IPackageService packageService;
        private IReviseService reviseService;
        private IFeedbackService feedbackService;

        private List<Card> cards;
        private int cardI;

        public RememberPageViewModel(IPackageService p, IReviseService r, IFeedbackService f) {
            packageService = p;
            reviseService = r;
            feedbackService = f;
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
        private const int RingWidth = 4;//时钟圆环的宽度 limit:0<RingWidth<100
        private int _style = ClockWidth * 100 + RingWidth;
        public int Style {
            get => _style;
            set => Set(nameof(Style), ref _style, value);
        }

        private RelayCommand _onLoaded;
        public RelayCommand OnLoaded => _onLoaded ?? (_onLoaded = new RelayCommand(() => { Time = 0; }));
    }
}
