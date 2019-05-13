using CardsForMemory.Services;
using System;
using System.Linq;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {

    public sealed partial class SettingsPage : Page {
        public string Version {
            get {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return string.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public SettingsPage() {
            InitializeComponent();
            Loaded += (a, b) => {
                var currentTheme = App.RootTheme.ToString();
                (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
            };
            ToastService toastService = new ToastService();
            toastService.Toast("jp`何をしに来ましたか？お笑いをしに来ましたか？早く行ってください。まだできていません。", 10);
        }

        //private async void OnFeedbackButtonClick(object sender, RoutedEventArgs e) {
        //    await Launcher.LaunchUriAsync(new Uri("feedback-hub:"));
        //}
        private int cnt = 0;
        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e) {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
            if (selectedTheme != null) {
                App.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
                if (selectedTheme == "Dark") {
                    if (cnt++ != 0) {
                        ToastService toastService = new ToastService();
                        toastService.Toast("jp`うん、よく分かりますねー", 3);
                    }
                    titleBar.ButtonForegroundColor = Colors.White;
                } else if (selectedTheme == "Light") {
                    if (cnt++ != 0) {
                        ToastService toastService = new ToastService();
                        toastService.Toast("jp`何をしていますか？これはみっともないです。", 5);
                    }
                    titleBar.ButtonForegroundColor = Colors.Black;
                } else {
                    if (cnt++ != 0) {
                        ToastService toastService = new ToastService();
                        toastService.Toast("jp`諦めないでください。あなたはまだ黒があります。", 7);
                    }
                    if (Application.Current.RequestedTheme == ApplicationTheme.Dark) {
                        titleBar.ButtonForegroundColor = Colors.White;
                    } else {
                        titleBar.ButtonForegroundColor = Colors.Black;
                    }
                }
            }
        }

        private Random ran = new Random(123456);
        private void RichTextBlock_GettingFocus(UIElement sender, Windows.UI.Xaml.Input.GettingFocusEventArgs args) {
            ToastService toastService = new ToastService();
            int num = ran.Next(0, 60);
            if (0 < num && num < 50) {
                toastService.Toast("zh`恭喜发现彩蛋,海哥天下第一", 5);
            } else if (50 <= num && num <= 60) {
                toastService.Toast("jp`タマゴ発見おめでとうございます,海兄は天下一品です。", 5);
            } else {
                toastService.Toast("en`My God? How can you be so strong! Almost as good as SeaBrother!", 8);
            }
        }
    }
}
