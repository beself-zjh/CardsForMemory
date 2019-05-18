using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using System.Threading.Tasks;

namespace CardsForMemoryTest.ServicesTest
{
    class FeedbackServiceTest
    {
        private FeedbackService feedbackServic = new FeedbackService(new CardService(new SqliteConnectionService(true)));
        private Card card = new Card() { Proficiency = 0 };
        [Test]
        public void TestcardProficiency()
        {
            
            Assert.AreEqual(0, card.Proficiency);
        }

        [Test]
        public async Task TestisEasy()
        {
            await feedbackServic.isEasy(card);
            Assert.AreEqual(200, card.Proficiency);
        }

        public async Task TestisNoemal()
        {
            await feedbackServic.isNormal(card);
            Assert.AreEqual(100, card.Proficiency);
        }

        public async Task TestisDifficult()
        {
            await feedbackServic.isDifficult(card);
            Assert.AreEqual(100, card.Proficiency);
        }
        public async Task TestisEasymax()
        {

            card.Proficiency = card.Proficiency + 11000;
            await feedbackServic.isEasy(card);
            Assert.AreEqual(10000, card.Proficiency);
        }

        public async Task TestisNoemalmax()
        {
            card.Proficiency = card.Proficiency + 10100;
            await feedbackServic.isNormal(card);
            Assert.AreEqual(10000, card.Proficiency);
        }

        public async Task TestisDifficultmax()
        {
            card.Proficiency = card.Proficiency + 11000;
            await feedbackServic.isDifficult(card);
            Assert.AreEqual(10000, card.Proficiency);
        }
    }
}
