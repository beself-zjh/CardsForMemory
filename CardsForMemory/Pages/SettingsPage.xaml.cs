using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace CardsForMemory.Pages {

    public sealed partial class SettingsPage : Page {
        public string Version {
            get {
                var version = Windows.ApplicationModel.Package.Current.Id.Version;
                return String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
            }
        }
        public SettingsPage() {
            this.InitializeComponent();
            Loaded += OnSettingsPageLoaded;
        }
        private async void OnFeedbackButtonClick(object sender, RoutedEventArgs e) {
            await Launcher.LaunchUriAsync(new Uri("feedback-hub:"));
        }
        private void OnSettingsPageLoaded(object sender, RoutedEventArgs e) {
            var currentTheme = App.RootTheme.ToString();
            (ThemePanel.Children.Cast<RadioButton>().FirstOrDefault(c => c?.Tag?.ToString() == currentTheme)).IsChecked = true;
        }

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

        private void OnThemeRadioButtonKeyDown(object sender, KeyRoutedEventArgs e) {
            if (e.Key == VirtualKey.Up) {
                //MainPage.Current.PageHeader.Focus(FocusState.Programmatic);
            }
        }

    }
}
