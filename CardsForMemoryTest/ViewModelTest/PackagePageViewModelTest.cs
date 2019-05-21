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
    class PackagePageViewModelTest {
        private PackagePageViewModel vm = new PackagePageViewModel(new PackageServiceEx(new SqliteConnectionService(true)),
            new CardServiceEx(new SqliteConnectionService(true)),Mock3.navigation, Mock3.toast);
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        [Test]
        public async Task LoadedCommandTest() {
            vm.LoadedCommand.Execute(null);
            Thread.Sleep(500);
            var list = (await ps.GetAllPackageAsync()).Result;
            Assert.AreEqual((vm.Packages as List<Package>).Count, list.Count);
        }

        [Test]
        public void AddCommandTest() {
            vm.AddCommand.Execute(null);
            Assert.AreEqual(Status.s["package"], null);
        }

        [Test]
        public async Task PlayCommandTest() {
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Package pg=  packagelist[packagelist.Count - 1];
            vm.SelectionPackage = pg;
            vm.PlayCommand.Execute(null);
            Assert.AreEqual((Status.s["package"] as Package).Name, pg.Name);
            Assert.AreEqual((Status.s["package"] as Package).Author, pg.Author);
            Assert.AreEqual((Status.s["package"] as Package).Description, pg.Description);
        }

        [Test]
        public async Task EditCardsCommandTest() {
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Package pg = packagelist[packagelist.Count - 1];
            vm.SelectionPackage = pg;
            vm.EditCardsCommand.Execute(null);
            Assert.AreEqual((Status.s["package"] as Package).Name, pg.Name);
            Assert.AreEqual((Status.s["package"] as Package).Author, pg.Author);
            Assert.AreEqual((Status.s["package"] as Package).Description, pg.Description);
        }

        [Test]
        public async Task DeleteCommandTest() {
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            Package pg = packagelist[packagelist.Count - 1];
            vm.SelectionPackage = pg;
            vm.DeleteCommand.Execute(null);
            Thread.Sleep(500);
            var packagelist2 = (await ps.GetAllPackageAsync()).Result;
            Assert.AreEqual(packagelist.Count, packagelist2.Count+1);
        }
    }
}
