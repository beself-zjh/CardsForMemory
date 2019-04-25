using System;
using System.Collections.Generic;
using System.Text;
using CardsForMemoryLibrary.Models;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public class SqliteConnectionService : ISqliteConnectionService {
        /// <inheritdoc />
        public ServiceResult<SQLiteAsyncConnection> GetAsyncConnection() {
            SQLiteAsyncConnection connection;
            try {
                connection = new SQLiteAsyncConnection("CardsForMemory");
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
        }
    }
}
