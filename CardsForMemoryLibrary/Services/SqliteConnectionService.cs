using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using GalaSoft.MvvmLight.Ioc;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public class SqliteConnectionService : ISqliteConnectionService {
        private string databaseName;

        /// <inheritdoc />
        public ServiceResult<SQLiteAsyncConnection> GetAsyncConnection() {
            SQLiteAsyncConnection connection;
            try {
                connection = new SQLiteAsyncConnection(databaseName);
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

        [PreferredConstructor]
        public SqliteConnectionService() {
            databaseName = "CardsForMemory";
        }

        public SqliteConnectionService(bool test = false) {
            databaseName = "CardsForMemory" + (test ? "Test" : "");
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
