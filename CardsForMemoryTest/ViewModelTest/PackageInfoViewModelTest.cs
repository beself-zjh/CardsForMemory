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
    class PackageInfoViewModelTest {
        private PackageInfoViewModel vm = new PackageInfoViewModel(Mock3.navigation,
             new PackageServiceEx(new SqliteConnectionService(true)),Mock3.toast);
        private CardServiceEx cardservice = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));


        [Test]
        public async Task LoadedCommandTest() {
            Status.s["package"] = null;
            vm.LoadedCommand.Execute(null);
            Assert.AreEqual("", vm.Name);
            Assert.AreEqual("", vm.Author);
            Assert.AreEqual("", vm.Description);
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Package pg = packagelist[packagelist.Count - 1];
            Status.s["package"] = pg;
            vm.LoadedCommand.Execute(null);
            Assert.AreEqual(pg.Name, vm.Name);
            Assert.AreEqual(pg.Author, vm.Author);
            Assert.AreEqual(pg.Description, vm.Description);
        }

        [Test]
        public async Task nextCommandTestAsync() {
            var cardList = (await cardservice.GetAllCardsAsync()).Result;
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Assert.LessOrEqual(2, packagelist.Count);
            Package pg = packagelist[packagelist.Count - 1];
            Status.s["package"] = pg;
            vm.Name = "1";
            vm.Author = "2";
            vm.Description = "3";
            vm.NextCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual((Status.s["package"] as Package).Name, vm.Name);
            Assert.AreEqual((Status.s["package"] as Package).Author, vm.Author);
            Assert.AreEqual((Status.s["package"] as Package).Description, vm.Description);

            Status.s["package"] = null;
            vm.Name = "4";
            vm.Author = "5";
            vm.Description = "6";
            vm.NextCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual((Status.s["package"] as Package).Name, vm.Name);
            Assert.AreEqual((Status.s["package"] as Package).Author, vm.Author);
            Assert.AreEqual((Status.s["package"] as Package).Description, vm.Description);
        }

    }
}
