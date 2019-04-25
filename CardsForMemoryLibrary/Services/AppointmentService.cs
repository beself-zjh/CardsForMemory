using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class AppointmentService : IAppointmentService {
        private ISqliteConnectionService _sqliteConnectionService;

        public AppointmentService(ISqliteConnectionService sqliteConnectionService) {
            _sqliteConnectionService = sqliteConnectionService;
        }

        /// <inheritdoc />
        public async Task InsertAsync(Card card) {
            await _sqliteConnectionService.GetAsyncConnection()
                .Result.InsertAsync(card);
        }

        /// <inheritdoc />
        public async Task<List<Card>> SelectAllAsync() {
            return await _sqliteConnectionService.GetAsyncConnection()
                .Result.Table<Card>().ToListAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAllAsync() {
            await _sqliteConnectionService.GetAsyncConnection()
                .Result.DeleteAllAsync<Card>();
        }
    }
}
