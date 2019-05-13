using CardsForMemory.Locator;
using CardsForMemoryLibrary;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CardsForMemory.Controls {
    public sealed partial class PackageInfo : UserControl {
        PackageInfoViewModel vm = ViewModelLocator.Instance.PackageInfoViewModel;

        private Popup popup;
        public PackageInfo() {
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
            };
            vm.InitCloseWindowAction(() => { popup.IsOpen = false; });

            var status = Status.getInstance();
            if (status["package"] is Package package) {
                vm.Name = package.Name;
                vm.Author = package.Author;
                vm.Description = package.Description;
            } else {
                vm.Name = "";
                vm.Author = "";
                vm.Description = "";
            }

            popup.IsOpen = true;
        }
    }
}
