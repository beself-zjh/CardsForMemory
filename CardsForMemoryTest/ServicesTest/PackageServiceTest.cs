using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest {
    //TestFixture表示需要测试的类，也就是说测试的单元
    [TestFixture]
    public class PackageServiceTest {
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        //Test表示测试用例，也就是测试方法
        [Test]
        public async Task TestGetAllPackageAsync() {
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            //检验是否有虚拟卡包的存在
            Assert.GreaterOrEqual(1, list.Count);
            Assert.AreEqual(-1, list[0].Id);
        }

        [Test]
        public async Task TestAddPackageAsync() {
            Package package = (await ps.AddPackageAsync("0", "1", "2")).Result;
            //检验返回值
            Assert.AreEqual("0", package.Name);
            Assert.AreEqual("1", package.Author);
            Assert.AreEqual("2", package.Description);
            //找一下是不是真的加入了数据库
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            Assert.IsTrue(list.Exists(
                (Package i) => i == package
            ));
        }

        [Test]
        public void Test() {

        }
    }
}
