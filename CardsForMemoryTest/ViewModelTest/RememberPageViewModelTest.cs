using CardsForMemoryLibrary;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace CardsForMemoryTest.ViewModelTest {
    [TestFixture]
    class RememberPageViewModelTest {
        private RememberPageViewModel vm = new RememberPageViewModel(new FeedbackService
            (new CardService(new SqliteConnectionService(true))), Mock3.toast);
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        [Test]
        public async Task LoadedCommandTest() {
            List<Card> cardlist = (await cardService.GetAllCardsAsync()).Result;
            Status.s["cardi"] = cardlist.Count;
            Status.s["cards"] = cardlist;
            vm.LoadedCommand.Execute(null);
            Assert.AreEqual(vm.Question, cardlist[cardlist.Count - 1].Question);
            Assert.AreEqual(vm.Answer, cardlist[cardlist.Count - 1].Answer);
        }

        [Test]
        public async Task EasyCommandTest() {
            List<Card> cardlist = (await cardService.GetAllCardsAsync()).Result;
            Status.s["cardi"] = cardlist.Count;
            Status.s["cards"] = cardlist;
            int Proficiency = cardlist[cardlist.Count - 1].Proficiency;
            vm.EasyCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual(Proficiency + 200, cardlist[cardlist.Count - 1].Proficiency);
        }

        [Test]
        public async Task NormalCommandTest() {
            await EasyCommandTest();
            List<Card> cardlist = (await cardService.GetAllCardsAsync()).Result;
            Status.s["cardi"] = cardlist.Count;
            Status.s["cards"] = cardlist;
            int Proficiency = cardlist[cardlist.Count - 1].Proficiency;
            vm.NormalCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual(Proficiency + 100, cardlist[cardlist.Count - 1].Proficiency);
        }


        [Test]
        public async Task DiffCommandTest() {
            List<Card> cardlist = (await cardService.GetAllCardsAsync()).Result;
            Status.s["cardi"] = cardlist.Count;
            Status.s["cards"] = cardlist;
            int Proficiency = cardlist[cardlist.Count - 1].Proficiency;
            vm.DiffCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual(Proficiency, cardlist[cardlist.Count - 1].Proficiency);
        }

        [Test]
        public async Task NextCommandTest() {
            List<Card> cardlist = (await cardService.GetAllCardsAsync()).Result;
            Status.s["cardi"] = cardlist.Count;
            Status.s["cards"] = cardlist;
            Thread.Sleep(500);
            vm.NextCommand.Execute(null);
            Assert.AreEqual(vm.Question, cardlist[cardlist.Count - 2].Question);
            Assert.AreEqual(vm.Answer, cardlist[cardlist.Count - 2].Answer);
            Status.s["cardi"] = 1;
            Thread.Sleep(500);
            vm.NextCommand.Execute(null);
            Assert.AreEqual(vm.Question, "恭喜,题答完了");
            Assert.AreEqual(vm.Answer, "");
        }
    }
}
