using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.ViewModels {
    public class QueryNewOldViewModel : ViewModelBase {
        private INavigationService navigationService;
        private ICardService cardService;
        private IToastService toastService;
        private ISettingService settingService;

        public QueryNewOldViewModel(INavigationService navigationService, ICardService cardService, IToastService toastService, ISettingService settingService) {
            this.navigationService = navigationService;
            this.cardService = cardService;
            this.toastService = toastService;
            this.settingService = settingService;
        }

        private static Action CloseWindow;
        public void InitCloseWindowAction(Action closeWindow) => CloseWindow = closeWindow;

        private string _new;
        public string New {
            get => _new;
            set => Set(nameof(New), ref _new, value);
        }

        private string _old;
        public string Old {
            get => _old;
            set => Set(nameof(Old), ref _old, value);
        }

        private string _newMax;
        public string NewMax {
            get => _newMax;
            set => Set(nameof(NewMax), ref _newMax, value);
        }

        private string _oldMax;
        public string OldMax {
            get => _oldMax;
            set => Set(nameof(OldMax), ref _oldMax, value);
        }

        private RelayCommand _loadedCommand;
        public RelayCommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(async () => {
            //处理状态中的package参数
            if (Status.s["package"] is Package package) {
                NewMax = (await cardService.GetNewCardNum(package.Id)).Result.ToString();
                OldMax = (await cardService.GetOldCardNum(package.Id)).Result.ToString();
                if (settingService["max number"] as string == "True") {
                    New = NewMax;
                    Old = OldMax;
                } else {
                    New = "";
                    Old = "";
                }
            }
        }));

        private RelayCommand _nextCommand;
        public RelayCommand NextCommand => _nextCommand ?? (_nextCommand = new RelayCommand(async () => {
            if (New != "" && Old != "") {
                int nNew = int.Parse(New);
                int nOld = int.Parse(Old);
                int nNewMax = int.Parse(NewMax);
                int nOldMax = int.Parse(OldMax);
                if (nNew > nNewMax) {
                    toastService.Toast("新卡数量超标");
                    return;
                }
                if (nOld > nOldMax) {
                    toastService.Toast("旧卡数量超标");
                    return;
                }
                List<Card> cards = (await cardService.GetCardsAsync((Status.s["package"] as Package).Id, nOld, nNew)).Result;
                Status.s["cards"] = cards;
                Status.s["cardi"] = cards.Count;
                CloseWindow?.Invoke();
                navigationService.Navigate("remember");
            }
        }));

        private RelayCommand _cancelCommand;
        public RelayCommand CancelCommand => _cancelCommand ?? (_cancelCommand = new RelayCommand(() => {
            CloseWindow?.Invoke();
        }));
    }
}
