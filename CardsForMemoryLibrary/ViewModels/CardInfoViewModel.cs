using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace CardsForMemoryLibrary.ViewModels {
    public class CardInfoViewModel : ViewModelBase {
        private INavigationService navigationService;
        private IPackageService packageService;

        public CardInfoViewModel(INavigationService navigationService, IPackageService packageService) {
            this.navigationService = navigationService;
            this.packageService = packageService;
        }

        private static System.Action closeWindow;
        public void initCloseWindowAction(System.Action closeWindow) => CardInfoViewModel.closeWindow = closeWindow;

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

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(async () => {
            //TODO
            if (Question != "" && Answer != "") {
                var status = Status.getInstance();
                Card card = new Card {
                    Question = Question,
                    Answer = Answer
                };
                var result = await packageService.GetAsyncAllPackage();
                if (result.Result != null) {
                    int id = 0;
                    result.Result.ForEach((Package p) => {
                        id = id > p.Id ? id : p.Id;
                    });
                    status["new.id"] = id;
                }
                closeWindow?.Invoke();
                navigationService.Navigate("edit package");
            }
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            closeWindow?.Invoke();
        }));
    }
}
