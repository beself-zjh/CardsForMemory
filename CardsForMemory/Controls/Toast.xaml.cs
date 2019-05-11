using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CardsForMemory.Controls {
    public sealed partial class Toast : UserControl {
        public Toast(string content, int showTime = 2) {
            InitializeComponent();

            Popup popup = new Popup { Child = this };
            Width = Window.Current.Bounds.Width;
            Height = Window.Current.Bounds.Height;
            Loaded += (sender, e) => {
                ToastText.Text = content;
                fadeOut.BeginTime = System.TimeSpan.FromSeconds(showTime);
                fadeOut.Begin();
                fadeOut.Completed += (a, b) => { popup.IsOpen = false; };
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
            popup.IsOpen = true;
        }
    }
}
