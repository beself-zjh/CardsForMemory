using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;

namespace CardsForMemoryTest.ServicesTest {
    public class PackageServiceTest {
        [Test]
        public async Task TestCreateEditGetDelete() {
            var ss = new SqliteConnectionService().GetAsyncConnection();
            await ss.Result.DeleteAllAsync<Package>();

            var packageService = new PackageService(new SqliteConnectionService());
            await packageService.AppendAsyncPackage(new Package() {
                Name = "new",
                Author = "h",
                Description = "h"
            });
            var packageList = (await packageService.GetAsyncAllPackage()).Result;
            var ap = packageList[0];
            Assert.AreEqual(1, packageList.Count);
            Assert.AreEqual("new", packageList[0].Name);

            ap.Name = "aaa";
            await packageService.EditAsyncPackage(ap);
            packageList = (await packageService.GetAsyncAllPackage()).Result;
            Assert.AreEqual("aaa", packageList[0].Name);

            await packageService.DeleteAsyncPackage(ap.Id);

        }
    }
}
