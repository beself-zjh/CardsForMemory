using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class AppointmentService : IAppointmentService {
        private SqliteConnectionService _sqliteConnectionService;

        public AppointmentService(SqliteConnectionService sqliteConnectionService) {
            _sqliteConnectionService = sqliteConnectionService;
        }

        /// <inheritdoc />
        public async Task InsertAsync(Card card) {
            await _sqliteConnectionService.GetAsyncConnection()
                .InsertAsync(card);
        }

        /// <inheritdoc />
        public async Task<List<Card>> SelectAllAsync() {
            return await _sqliteConnectionService.GetAsyncConnection()
                .Table<Card>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync() {
            await _sqliteConnectionService.GetAsyncConnection()
                .DeleteAllAsync<Card>();
        }
    }
}
