using CardsForMemory.Services;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class MusicPage : Page {
        public MusicPage() {
            InitializeComponent();
            ToastService toastService=new ToastService();
            toastService.Toast("en`Hello, welcome to use.",5);
        }
    }
}
