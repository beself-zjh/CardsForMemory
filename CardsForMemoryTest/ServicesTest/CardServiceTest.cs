using System;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest {
    public class CardServiceTest {
        private ServiceResult<List<Card>> cardList;
        private CardServiceEx cardService = new CardServiceEx(new SqliteConnectionService(true));
        [Test]
        public async Task TestDeleteAllCardAsync() {
            await cardService.DeleteAllCardAsync();//删除CardTable全部内容
             cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);
        }

        [Test]
        public async Task TestAddCardAsync() {
            await TestDeleteAllCardAsync();
            await cardService.AddCardAsync(0, "1", "1");//插入一个Card

            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(1, cardList.Result.Count);
            Assert.AreEqual(0, cardList.Result[0].PackageId);
        }

        [Test]
        public async Task TestDeleteCardAsync() {
            await TestAddCardAsync();
            await cardService.DeleteCardAsync(cardList.Result[0].Id);//删除一个卡片
            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);
            await cardService.DeleteAllCardAsync();
        }

        [Test]
        public async Task TestEditCard() {
            await cardService.DeleteAllCardAsync();
            var card=(await cardService.AddCardAsync(0, "0", "0")).Result;
            card.PackageId = 2;
            await cardService.EditCardAsync(card);
            var cardList = await cardService.GetAllCardsAsync();

            card = (await cardService.GetCardAsync(cardList.Result[0].Id)).Result;
            Assert.AreEqual(2, card.PackageId);
            cardList = await cardService.GetAllCardsAsync();
            Assert.AreEqual(1, cardList.Result.Count);

            await cardService.DeleteAllCardAsync();
        }

        [Test]
        public async Task TestRevise() {

            await cardService.DeleteAllCardAsync();
            await cardService.AddCardAsync(0, "0", "0");
            await cardService.AddCardAsync(0, "0", "0");
            await cardService.AddCardAsync(0, "0", "0");
            var o = (await cardService.GetOldCardNum(0)).Result;
            var n = (await cardService.GetNewCardNum(0)).Result;

            Assert.AreEqual(0, o);
            Assert.AreEqual(3, n);
            Assert.AreEqual(2, (await cardService.GetCardsAsync(0,0,2)).Result.Count);
        }
        
    }
}
