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
    class EditPackagePageViewModelTest {
        private EditPackagePageViewModel vm = new EditPackagePageViewModel(new CardServiceEx(new SqliteConnectionService(true)),
            Mock3.navigation, Mock3.toast);
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        [Test]
        public async Task LoadedCommandTest() {
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Package pg = packagelist[packagelist.Count - 1];
            Status.s["package"] = pg;
            vm.LoadedCommand.Execute(null);
            Thread.Sleep(500);
            var list = (await cardService.GetCardsAsync(pg.Id)).Result;
            Assert.AreEqual((vm.Cards as List<Card>).Count, list.Count);
        }

        [Test]
        public void AddCommandTest() {
            vm.AddCommand.Execute(null);
            Assert.AreEqual(null, Status.s["card"]);
        }

        [Test]
        public void EditCommandTest() {
            vm.SelectionCard = null;
            vm.EditCommand.Execute(null);
            Assert.AreEqual(null, Status.s["card"]);
        }


        [Test]
        public async Task DeleteCommandTest() {
            var cardlist = (await cardService.GetAllCardsAsync()).Result;
            int num = cardlist.Count;
            Card card = cardlist[cardlist.Count - 1];
            vm.SelectionCard = card;
            vm.DeleteCommand.Execute(null);
            cardlist = (await cardService.GetAllCardsAsync()).Result;
            Assert.AreEqual(num - 1, cardlist.Count);
            Assert.AreEqual(null, Status.s["card"]);
        }

        [Test]
        public void PreviewCommanTest() {
            vm.SelectionCard = null;
            vm.PreviewCommand.Execute(null);
            Assert.AreEqual(null, Status.s["card"]);
        }
    }
}
