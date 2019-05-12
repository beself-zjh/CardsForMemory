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
                return String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }

        public SettingsPage() {
            InitializeComponent();
            Loaded += (a, b) => {
                var currentTheme = App.RootTheme.ToString();
                (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
            };
        }

        //private async void OnFeedbackButtonClick(object sender, RoutedEventArgs e) {
        //    await Launcher.LaunchUriAsync(new Uri("feedback-hub:"));
        //}

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e) {
            var selectedTheme = ((RadioButton)sender)?.Tag?.ToString();
            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            if (selectedTheme != null) {
                App.RootTheme = App.GetEnum<ElementTheme>(selectedTheme);
                if (selectedTheme == "Dark") {
                    titleBar.ButtonForegroundColor = Colors.White;
                } else if (selectedTheme == "Light") {
                    titleBar.ButtonForegroundColor = Colors.Black;
                } else {
                    if (Application.Current.RequestedTheme == ApplicationTheme.Dark) {
                        titleBar.ButtonForegroundColor = Colors.White;
                    } else {
                        titleBar.ButtonForegroundColor = Colors.Black;
                    }
                }
            }
        }
    }
}
