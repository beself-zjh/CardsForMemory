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
        [Test]
        public async Task TestDeleteAllCardAsync() {
            var cardService = new CardService(new SqliteConnectionService(true));
            await cardService.DeleteAllCardAsync();//删除CardTable全部内容


             cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);
        }

        [Test]
        public async Task TestAddCardAsync()
        {
            await TestDeleteAllCardAsync();
            var cardService = new CardService(new SqliteConnectionService(true));
            await cardService.AddCardAsync(0, "1", "1");//插入一个Card

            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(1, cardList.Result.Count);
            Assert.AreEqual(0, cardList.Result[0].PackageId);

            await cardService.DeleteCardAsync(cardList.Result[0].Id);//删除一个卡片
            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);

            await cardService.DeleteAllCardAsync();
        }

        [Test]
        public async Task TestEditCard() {
            var cardService = new CardService(new SqliteConnectionService(true));
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
            var cardService = new CardService(new SqliteConnectionService(true));
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

        [Test]
        public void TTTT() {
            var card1 = new Card() {
                Question = "1",
                UpdateTime = new DateTime(2019,5,16,0,0,0),
                Proficiency = 100
            };
            var card2 = new Card() {
                Question = "2",

                UpdateTime = new DateTime(2019, 5, 15, 0, 0, 0),
                Proficiency = 100
            };
            var card3 = new Card() {
                Question = "3",

                UpdateTime = DateTime.Now,
                Proficiency = 100
            };

            List<Card> cards = new List<Card>();
            cards.Add(card1);
            cards.Add(card2);
            cards.Add(card3);
            var sortedCards = cards.OrderByDescending
                (i => 10000 - 5600 * Math.Pow((DateTime.Now - i.UpdateTime).Hours, 0.06) + i.Proficiency)
                .ToList();
            foreach (var i in sortedCards) {
                Console.WriteLine(i.Proficiency);
            }
            Assert.AreEqual("3", sortedCards[0].Question);
            Assert.AreEqual("1", sortedCards[1].Question);
            Assert.AreEqual("2", sortedCards[2].Question);
        }
    }
}
