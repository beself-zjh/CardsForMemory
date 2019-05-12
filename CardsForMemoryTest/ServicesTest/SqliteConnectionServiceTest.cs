using CardsForMemoryLibrary.Services;
using NUnit.Framework;
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
 