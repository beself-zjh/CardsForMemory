using CardsForMemoryLibrary;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using Moq;
using NUnit.Framework;

namespace CardsForMemoryTest.ViewModelTest {
    internal class Mock3 {
        public static INavigationService navigation;
        public static ISettingService setting;
        public static IToastService toast;
        static Mock3() {
            var navigationMock = new Mock<INavigationService>();
            navigationMock.Setup(s => s.Navigate(It.IsAny<string>()));
            navigation = navigationMock.Object;
            var settingMock = new Mock<ISettingService>();
            settingMock.Setup(s => s[It.IsAny<string>()]).Returns(null);
            setting = settingMock.Object;
            var toastMock = new Mock<IToastService>();
            toastMock.Setup(s => s.Toast(It.IsAny<string>(), It.IsAny<int>()));
            toast = toastMock.Object;
        }
    }

    [TestFixture]
    internal class CardInfoViewModelTest {
        private CardInfoViewModel vm = new CardInfoViewModel(Mock3.navigation,
            new CardServiceEx(new SqliteConnectionService(true)), Mock3.toast);

        [Test]
        public void LoadedCommandTest() {
            Status.s["card"] = null;
            vm.LoadedCommand.Execute(null);
            Assert.AreEqual(vm.Question, "");
            Assert.AreEqual(vm.Answer, "");
            Status.s["card"] = new Card { Question = "1", Answer = "1" };
            vm.LoadedCommand.Execute(null);
            Assert.AreEqual(vm.Question, "1");
            Assert.AreEqual(vm.Answer, "1");
        }
    }
}
