using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;

namespace CardsForMemoryTest.ServicesTest {
    public class CardServiceTest {
        [Test]
        public async Task TestSingleCard() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAsyncAllCard();//删除CardTable全部内容
            var card = new Card() {
                PackageId = 1,
                Question = "enheng?",
                Answer = "no"
            };

            var cardList = await cardService.GetAsyncAllCards();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);


            await cardService.InsertAsyncCard(card);//插入一个Card

            cardList = await cardService.GetAsyncAllCards();//获取CardTable全部内容
            Assert.AreEqual(1, cardList.Result.Count);
            Assert.AreEqual(1, cardList.Result[0].PackageId);

            await cardService.DeleteAsyncCard(cardList.Result[0].Id);//删除一个卡片
            cardList = await cardService.GetAsyncAllCards();//获取CardTable全部内容
            Assert.AreEqual(0, cardList.Result.Count);

            await cardService.DeleteAsyncAllCard();
        }

        [Test]
        public async Task TestCardList() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAsyncAllCard();
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

            await cardService.InsertAsyncCards(cardList);
            cardList = (await cardService.GetAsyncAllCards()).Result;
            Assert.AreEqual(8, cardList.Count);

            cardList = (await cardService.GetAsyncCards(1)).Result;
            Assert.AreEqual(5, cardList.Count);

            await cardService.DeleteAsyncCards(1);
            cardList = (await cardService.GetAsyncCards(1)).Result;
            Assert.AreEqual(0, cardList.Count);

            await cardService.DeleteAsyncAllCard();
        }

        [Test]
        public async Task TestEditCard() {
            var cardService = new CardService(new SqliteConnectionService());
            await cardService.DeleteAsyncAllCard();
            var card = new Card() {
                PackageId = 1
            };

            await cardService.InsertAsyncCard(card);

            card.PackageId = 2;
            await cardService.EditAsyncCard(card);

            var cardList = await cardService.GetAsyncAllCards();

            card = (await cardService.GetAsyncCard(cardList.Result[0].Id)).Result;
            Assert.AreEqual(2, card.PackageId);
            cardList = await cardService.GetAsyncAllCards();
            Assert.AreEqual(1, cardList.Result.Count);

            await cardService.DeleteAsyncAllCard();
        }
    }
}
