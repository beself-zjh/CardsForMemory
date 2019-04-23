using System;
using System.Collections.Generic;
using System.Text;
using CardsForMemoryLibrary.Models;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    public class SqliteConnectionService : ISqliteConnectionService {
        /// <inheritdoc />
        public SQLiteAsyncConnection GetAsyncConnection() {
            return new SQLiteAsyncConnection("CardsForMemory");
        }

        static SqliteConnectionService() {
            var connection = new SQLiteConnection("CardsForMemory");
            connection.CreateTable<Card>();
        }
    }
}
