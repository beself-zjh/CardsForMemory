using CardsForMemoryLibrary;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using Moq;
using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

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

        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

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

        [Test]
        public async Task nextCommandTestAsync() {
            var cardList = (await cardService.GetAllCardsAsync()).Result;
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Assert.LessOrEqual(2, packagelist.Count);
            Status.s["card"] = null;
            Package pg = packagelist[packagelist.Count - 1];
            Status.s["package"] = pg;
            vm.Question = "1";
            vm.Answer = "2";
            vm.NextCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual((Status.s["card"] as Card).Question, vm.Question);
            Assert.AreEqual((Status.s["card"] as Card).Answer, vm.Answer);

            Card card = cardList[cardList.Count - 1];
            vm.Question = "3";
            vm.Answer = "4";
            Status.s["card"] = card;
            vm.NextCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual((Status.s["card"] as Card).Question, vm.Question);
            Assert.AreEqual((Status.s["card"] as Card).Answer, vm.Answer);
        }
    }
}
