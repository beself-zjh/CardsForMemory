using CardsForMemory.Services;
using Windows.UI.Xaml.Controls;

namespace CardsForMemory.Pages {
    public sealed partial class HomePage : Page {
        public HomePage() {
            InitializeComponent();
            ToastService toastService = new ToastService();
            toastService.Toast("jp`こんにちは、いらっしゃいませ",5);
        }
    }
}
