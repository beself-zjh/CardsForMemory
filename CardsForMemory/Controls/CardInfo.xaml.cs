using CardsForMemory.Locator;
using CardsForMemoryLibrary.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CardsForMemory.Controls {
    public sealed partial class CardInfo : UserControl {
        CardInfoViewModel vm = ViewModelLocator.Instance.CardInfoViewModel;

        private Popup popup;
        public CardInfo(Action action) {
            RequestedTheme = App.RootTheme;
            InitializeComponent();
            popup = new Popup { Child = this };
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
                vm.ClearHandler();
            };
            vm.Next += (a, b) => { popup.IsOpen = false; action(); };
            vm.Cancel += (a, b) => { popup.IsOpen = false; };
            popup.IsOpen = true;
        }
    }
}
