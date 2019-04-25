using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using CardsForMemoryLibrary.Services;
using SQLite;

namespace CardsForMemoryTest.ServicesTest {
    public class SqliteConnectionServiceTest {
        [Test]
        public void TestGetAsyncConnection() {
            SqliteConnectionService sqliteConnectionService = 
                new SqliteConnectionService();
            ServiceResult<SQLiteAsyncConnection> connection = 
                sqliteConnectionService.GetAsyncConnection();
            Assert.IsNotNull(connection);
        }
    }
}
 