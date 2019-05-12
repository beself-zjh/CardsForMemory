using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CardsForMemory.Pages {
    public sealed partial class RememberPage : Page {
        private RememberPageViewModel vm = ViewModelLocator.Instance.RememberPageViewModel;

        protected override void OnNavigatedTo(NavigationEventArgs e) {

        }

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
            if (timer == null) {
                timer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 1) };
                timer.Tick += (a, b) => { vm.Time++; };
                timer.Start();
            }
        }
        #endregion
    }
}
