using CardsForMemory.Controls;
using CardsForMemoryLibrary.IServices;

namespace CardsForMemory.Services {
    class ToastService : IToastService {
        public void Toast(string message, int time = 2) {
            new Toast(message, time);
        }
    }
}
