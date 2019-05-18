using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest
{
    //TestFixture表示需要测试的类，也就是说测试的单元
    [TestFixture]
    public class PackageServiceTest
    {
        private PackageServiceEx ps = new PackageServiceEx(new SqliteConnectionService(true));

        //Test表示测试用例，也就是测试方法
        [Test]
        public async Task TestGetAllPackageAsync()
        {
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            //检验是否有虚拟卡包的存在
            Assert.LessOrEqual(1, list.Count);
            Assert.AreEqual(-1, list[0].Id);
        }

        [Test]
        public async Task TestAddPackageAsync()
        {
            Package package = (await ps.AddPackageAsync("0", "1", "2")).Result;
            //检验返回值
            Assert.AreEqual("0", package.Name);
            Assert.AreEqual("1", package.Author);
            Assert.AreEqual("2", package.Description);
            //找一下是不是真的加入了数据库
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            Assert.IsTrue(list.Exists(
                (Package i) => i.Id == package.Id
            ));
        }

        [Test]
        public async Task TestDeletePackageAsync()
        {
            await TestAddPackageAsync();
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            int count = list.Count;
            await ps.DeletePackageAsync(list[0].Id);
            list = (await ps.GetAllPackageAsync()).Result;
            Assert.AreEqual(count, list.Count);
            await ps.DeletePackageAsync(list[1].Id);
            list = (await ps.GetAllPackageAsync()).Result;
            Assert.AreEqual(count - 1, list.Count);
        }

        [Test]
        public async Task TestEditPackageAsync()
        {
            await TestAddPackageAsync();
            List<Package> list = (await ps.GetAllPackageAsync()).Result;
            int id = list[list.Count-1].Id;
            var package = new Package()
            {
                Id = id,
                Name = "233",
                Author = "2133",
                Description = "2133",
                CreateTime = DateTime.Today,
                UpdateTime = DateTime.Now
            };
            await ps.EditPackageAsync(package);
            list = (await ps.GetAllPackageAsync()).Result;
            Assert.AreEqual("233", list[list.Count-1].Name);
        }

        [Test]
        public async Task TestGetPackageAsync()
        {
            Package package = (await ps.GetPackageAsync(-1)).Result;
            Assert.AreEqual("虚拟卡包", package.Name);
        }

    }
}
