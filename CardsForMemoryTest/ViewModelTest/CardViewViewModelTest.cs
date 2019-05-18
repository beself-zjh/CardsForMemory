using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using NUnit.Framework;

namespace CardsForMemoryTest.ViewModelTest {
    [TestFixture]

    class CardViewViewModelTest {
        private CardViewViewModel vm = new CardViewViewModel(Mock3.navigation,
            new CardServiceEx(new SqliteConnectionService(true)), Mock3.toast);
    }
}
