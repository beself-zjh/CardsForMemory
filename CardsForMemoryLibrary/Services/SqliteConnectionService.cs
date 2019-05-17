using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public class SqliteConnectionService : ISqliteConnectionService {
        /// <inheritdoc />
        public ServiceResult<SQLiteAsyncConnection> GetAsyncConnection(bool test = false) {
            SQLiteAsyncConnection connection;
            try {
                connection = new SQLiteAsyncConnection("CardsForMemory" + (test ? "Test" : ""));
            } catch (SQLiteException e) {
                return new ServiceResult<SQLiteAsyncConnection> {
                    Status = ServiceResultStatusHelper.FromSQLiteResult(e.Result),
                    Message = e.Message
                };
            }

            return new ServiceResult<SQLiteAsyncConnection> {
                Result = connection,
                Status = ServiceResultStatus.OK
            };
        }

        static SqliteConnectionService() {
            var connection = new SQLiteConnection("CardsForMemory");
            connection.CreateTable<Card>();
            connection.CreateTable<Package>();
            connection.CreateTable<Log>();
            connection.Close();
            connection = new SQLiteConnection("CardsForMemoryTest");
            connection.CreateTable<Card>();
            connection.CreateTable<Package>();
            connection.CreateTable<Log>();
            connection.Close();
        }
    }
}
