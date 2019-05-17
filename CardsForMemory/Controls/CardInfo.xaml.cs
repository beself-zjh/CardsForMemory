using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CardsForMemory.Controls {
    public sealed partial class CardInfo : UserControl {
        CardInfoViewModel vm = ViewModelLocator.Instance.CardInfoViewModel;

        public CardInfo() {
            RequestedTheme = App.RootTheme;
            InitializeComponent();
            Popup popup = new Popup { Child = this };
            Width = Window.Current.Bounds.Width;
            Height = Window.Current.Bounds.Height;
            Loaded += (sender, e) => {
                Window.Current.SizeChanged += (a, b) => {
                    Width = b.Size.Width;
                    Height = b.Size.Height;
                };
            };
            Unloaded += (sender, e) => {
                Window.Current.SizeChanged -= (a, b) => {
                    Width = b.Size.Width;
                    Height = b.Size.Height;
                };
            };
            vm.InitCloseWindowAction(() => { popup.IsOpen = false; });
            
            popup.IsOpen = true;
        }
    }
}
