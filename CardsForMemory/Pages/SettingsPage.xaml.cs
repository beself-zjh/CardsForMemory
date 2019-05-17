using CardsForMemory.Services;
using System.Linq;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {

    public sealed partial class SettingsPage : Page {
        public SettingsPage() {
            InitializeComponent();
            Loaded += (a, b) => {
                var currentTheme = App.RootTheme.ToString();
                var rbs = ThemePanel.Children.OfType<RadioButton>();
                rbs.FirstOrDefault(c => c.Tag.ToString() == currentTheme).IsChecked = true;
            };
        }

        private bool SoundSwitch {
            get => new SettingService()["sound"] as string == "True";
            set {
                new SettingService()["sound"] = value.ToString();
            }
        }

        private bool MaxNumberSwitch {
            get => new SettingService()["max number"] as string == "True";
            set {
                new SettingService()["max number"] = value.ToString();
            }
        }

        private void OnThemeRadioButtonChecked(object sender, RoutedEventArgs e) {
            var selectedTheme = (sender as RadioButton).Tag.ToString();
            if (selectedTheme == null) {
                return;
            }
            var titleBar = ApplicationView.GetForCurrentView().TitleBar;
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
