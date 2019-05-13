using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest {
    public class CardServiceTest {
        [Test]
        public async Task TestSingleCard() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAllCardAsync();//删除CardTable全部内容
            var card = new Card() {
                PackageId = 1,
                Question = "enheng?",
                Answer = "no"
            };

            var cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);


            await cardService.AddCardAsync(0, "1", "1");//插入一个Card

            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(1, cardList.Result.Count);
            Assert.AreEqual(1, cardList.Result[0].PackageId);

            await cardService.DeleteCardAsync(cardList.Result[0].Id);//删除一个卡片
            cardList = await cardService.GetAllCardsAsync();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);

            await cardService.DeleteAllCardAsync();
        }

        [Test]
        public async Task TestCardList() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAllCardAsync();
            List<Card> cardList = new List<Card>();
            for (int i = 0; i < 5; i++) {
                cardList.Add(new Card() {
                    PackageId = 1
                });
            }
            for (int i = 0; i < 3; i++) {
                cardList.Add(new Card() {
                    PackageId = 2
                });
            }

            await cardService.AppendAsyncCards(cardList);
            cardList = (await cardService.GetAllCardsAsync()).Result;
            Assert.AreEqual(8, cardList.Count);

            cardList = (await cardService.GetCardsAsync(1)).Result;
            Assert.AreEqual(5, cardList.Count);

            await cardService.DeleteCardsAsync(1);
            cardList = (await cardService.GetCardsAsync(1)).Result;
            Assert.AreEqual(0, cardList.Count);

            await cardService.DeleteAllCardAsync();
        }

        [Test]
        public async Task TestEditCard() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAllCardAsync();
            var card = new Card() {
                PackageId = 1
            };

            await cardService.AddCardAsync(0, "0", "0");

            card.PackageId = 2;
            await cardService.EditCardAsync(card);

            var cardList = await cardService.GetAllCardsAsync();

            card = (await cardService.GetCardAsync(cardList.Result[0].Id)).Result;
            Assert.AreEqual(2, card.PackageId);
            cardList = await cardService.GetAllCardsAsync();
            Assert.AreEqual(1, cardList.Result.Count);

            await cardService.DeleteAllCardAsync();
        }
    }
}
