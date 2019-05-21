using CardsForMemoryLibrary;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using CardsForMemoryLibrary.ViewModels;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ViewModelTest {
    [TestFixture]
    class QueryNewOldViewModelTest {
        private QueryNewOldViewModel vm = new QueryNewOldViewModel(Mock3.navigation,
            new CardServiceEx(new SqliteConnectionService(true)), Mock3.toast, Mock3.setting);
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        [Test]
        public async Task LoadedCommandTest() {
            var packagelist = (await ps.GetAllPackageAsync()).Result;
            var pg = packagelist[packagelist.Count - 1];
            Status.s["package"] = pg;
            vm.LoadedCommand.Execute(null);
            Thread.Sleep(500);
            Assert.AreEqual(vm.NewMax, (await cardService.GetNewCardNum(pg.Id)).Result.ToString());
            Assert.AreEqual(vm.OldMax, (await cardService.GetOldCardNum(pg.Id)).Result.ToString());
        }

        [Test]
        public async Task NextCommandTest() {
            await LoadedCommandTest();
            vm.NextCommand.Execute(null);
        }
    }
}
