using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using NUnit.Framework;

namespace CardsForMemoryTest.ServicesTest {
    public class AppointmentServiceTest {
        [Test]
        public async Task TestInsertSelectDelete()
        {
            //SqliteConnectionService sqliteConnectionService = 
            //    new SqliteConnectionService();
            //AppointmentService appointmentService = 
            //    new AppointmentService(sqliteConnectionService);

            //Card card = new Card() { Question = "math", Answer = "yes" };
            //await appointmentService.InsertAsync(card);
            //List<Card> cards = await appointmentService.SelectAllAsync();

            //Assert.AreEqual("math", cards[0].Question);
            //Assert.AreEqual("yes", cards[0].Answer);

            //await appointmentService.DeleteAllAsync();

            //cards = await appointmentService.SelectAllAsync();

            //Assert.IsEmpty(cards);
        }
    }
}
