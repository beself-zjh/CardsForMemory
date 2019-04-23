using System;
using System.Collections.Generic;
using System.Text;
using CardsForMemoryLibrary.Models;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public class SqliteConnectionService : iSqliteConnectionService {
        /// <inheritdoc />
        public SQLiteAsyncConnection GetAsyncConnection() {
            return new SQLiteAsyncConnection("CardsForMemory");
        }

        static SqliteConnectionService() {
            var connection = new SQLiteConnection("CardsForMemory");
            connection.CreateTable<Cards>();
        }
    }
}
