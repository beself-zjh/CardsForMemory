using CardsForMemoryLibrary;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ViewModelTest {
    [TestFixture]
    class CardViewViewModelTest {
        private CardViewViewModel vm = new CardViewViewModel(Mock3.navigation,
            new CardServiceEx(new SqliteConnectionService(true)), Mock3.toast);
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));


        [Test]
        public async  Task LoadedCommandTest() {
            var cardList = (await cardService.GetAllCardsAsync()).Result;
            Card card = cardList[cardList.Count - 1];
            Status.s["card"] = card;
            vm.LoadedCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual((Status.s["card"] as Card).Question, vm.ShowText);
            Assert.AreEqual("看答案", vm.ButtonText);
        }


        [Test]
        public void NextCommandTestAsync() {
            vm.ButtonText = "See Answer";
            vm.NextCommand.Execute(null);
            Assert.AreEqual("看问题", vm.ButtonText);
            vm.ButtonText = "123";
            vm.NextCommand.Execute(null);
            Assert.AreEqual("看答案", vm.ButtonText);
        }
    }
}
