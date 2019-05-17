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
        [Test]
        public async Task TestcardProficiency()
        {
            var feedbackServic = new FeedbackService(new CardService(new SqliteConnectionService(true)));
            var card = new Card()
            {
                PackageId = 1,
                Question = "enheng?",
                Answer = "no",
                Proficiency = 0
            };

            Assert.AreEqual(0, card.Proficiency);

            await feedbackServic.isEasy(card);
            Assert.AreEqual(200, card.Proficiency);

            await feedbackServic.isNormal(card);
            Assert.AreEqual(300, card.Proficiency);

            await feedbackServic.isDifficult(card);
            Assert.AreEqual(300, card.Proficiency);

            card.Proficiency = card.Proficiency + 10000;
            await feedbackServic.isEasy(card);
            Assert.AreEqual(10000, card.Proficiency);

            card.Proficiency = card.Proficiency + 10000;
            await feedbackServic.isNormal(card);
            Assert.AreEqual(10000, card.Proficiency);

            card.Proficiency = card.Proficiency + 10000;
            await feedbackServic.isDifficult(card);
            Assert.AreEqual(10000, card.Proficiency);
        }
    }
}
