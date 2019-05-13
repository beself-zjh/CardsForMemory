using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest {
    public class PackageServiceTest {
        [Test]
        public async Task TestCreateEditGetDelete() {
            var ss = new SqliteConnectionService().GetAsyncConnection();
            await ss.Result.DeleteAllAsync<Package>();

            var packageService = new PackageService(new SqliteConnectionService());
            await packageService.AddPackageAsync("new", "h", "h");
            var packageList = (await packageService.GetAllPackageAsync()).Result;
            var ap = packageList[0];
            Assert.AreEqual(1, packageList.Count);
            Assert.AreEqual("new", packageList[0].Name);

            ap.Name = "aaa";
            await packageService.EditPackageAsync(ap);
            packageList = (await packageService.GetAllPackageAsync()).Result;
            Assert.AreEqual("aaa", packageList[0].Name);

            await packageService.DeletePackageAsync(ap.Id);

        }
    }
}
