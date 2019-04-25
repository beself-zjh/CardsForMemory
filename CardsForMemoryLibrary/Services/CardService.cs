using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class CardService : ICardService {
        private ISqliteConnectionService _connectionService;

        public CardService(ISqliteConnectionService connectionService) {
            _connectionService = connectionService;
        }

        /// <inheritdoc />
        public async Task<ServiceResult<Card>> GetAsyncSingleCard(int cardId) {
            var connection = _connectionService.GetAsyncConnection();
            ServiceResult<Card> serviceResult = new ServiceResult<Card>();
            switch (connection.Status) {
                case ServiceResultStatus.OK:
                    serviceResult.Result =
                        await connection.Result
                            .Table<Card>()
                            .Where(i => i.Id == cardId)
                            .FirstAsync();
                    serviceResult.Status = connection.Status;
                    serviceResult.Message = connection.Message;
                    break;
                default:
                    serviceResult.Status = connection.Status;
                    serviceResult.Message = connection.Message;
                    break;
            }

            return serviceResult;
        }

        /// <inheritdoc />
        public async Task<ServiceResult<List<Card>>> GetAsyncCardInPackage(int packageId) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ServiceResult<List<Card>>> GetAsyncAllCards() {
            var connection = _connectionService.GetAsyncConnection();
            ServiceResult<List<Card>> serviceResult = new ServiceResult<List<Card>>();
            switch (connection.Status) {
                case ServiceResultStatus.OK:
                    serviceResult.Result =
                        await connection.Result.Table<Card>().ToListAsync();
                    serviceResult.Status = connection.Status;
                    serviceResult.Message = connection.Message;
                    break;
                default:
                    serviceResult.Status = connection.Status;
                    serviceResult.Message = connection.Message;
                    break;
            }

            return serviceResult;
        }

        /// <inheritdoc />
        public async Task<ServiceResult> EditAsyncSingleCard(Card card) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ServiceResult> InsertAsyncSingleCard(Card card) {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public async Task<ServiceResult> InsertAsyncPluralCard(List<Card> cards) {
            throw new NotImplementedException();
        }
    }
}
