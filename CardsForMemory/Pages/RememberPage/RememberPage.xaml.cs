using CardsForMemoryLibrary.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace CardsForMemory.Pages {
    public sealed partial class RememberPage : Page {

        protected override void OnNavigatedTo(NavigationEventArgs e) {

        }

        public RememberPage() {
            InitializeComponent();
            ViewModelSetup();
            TimerSetup();
        }

        #region 设置vm
        /// <summary>
        /// 此页ViewModel,在xaml里用,所以起一个vm的名字而不是viewModel
        /// </summary>
        private RememberPageViewModel vm;
        /// <summary>
        /// 设置ViewModel
        /// </summary>
        private void ViewModelSetup() {
            vm = Locator.ViewModelLocator.Instance.RememberPageViewModel;
        }
        #endregion

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
