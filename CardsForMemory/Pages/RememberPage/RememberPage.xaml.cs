using CardsForMemory.Locator;
using CardsForMemory.Services;
using CardsForMemoryLibrary;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class RememberPage : Page {
        private RememberPageViewModel vm = ViewModelLocator.Instance.RememberPageViewModel;

        public RememberPage() {
            InitializeComponent();
            TimerSetup();
        }

        #region 设置timer
        /// <summary>
        /// vm.Time每秒自增器
        /// </summary>
        private static DispatcherTimer timer = null;
        /// <summary>
        /// 设置自增器,间隔为一秒
        /// </summary>
        private void TimerSetup() {
            Loaded += (a, b) => {
                timer.Start();
            };
            Unloaded += (a, b) => {
                timer.Stop();
            };
            if (timer == null) {
                timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                timer.Tick += (a, b) => {
                    Status s = Status.s;
                    if (s["time"] == null) {
                        s["time"] = 0;
                    }
                    if (s["cardi"] == null) {
                        return;
                    }
                    int cardi = (int)s["cardi"];
                    int cardSize = ((List<Card>)s["cards"]).Count;

                    if (cardi == 0) {
                        return;
                    }

                    s["time"] = (int)s["time"] + 1;
                    vm.Time = (int)s["time"];
                    int minute = vm.Time / 60;
                    int second = vm.Time % 60;
                    var smin = minute.ToString();
                    var ssec = second < 10 ? "0" + second.ToString() : second.ToString();

                    NavigationService navigationService = new NavigationService();
                    navigationService.Set_RememberPageName($"{smin}:{ssec} ({cardSize - cardi + 1}/{cardSize})");
                };
            }
        }
        #endregion
    }
}
