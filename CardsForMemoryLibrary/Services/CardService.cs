using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using CardsForMemoryLibrary.Models;
using SQLite;

namespace CardsForMemoryLibrary.Services {
    /// <summary>
    ///     卡片服务
    /// </summary>
    public class CardService : ICardService {
        /// <summary>
        ///     数据库连接服务
        /// </summary>
        private ISqliteConnectionService _connectionService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionService"></param>
        public CardService(ISqliteConnectionService connectionService) {
            _connectionService = connectionService;
        }

        /// <inheritdoc />
        public async Task<ServiceResult<Card>> GetAsyncCard(int cardId) {
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
        public async Task<ServiceResult<List<Card>>> GetAsyncCards(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            ServiceResult<List<Card>> serviceResult = new ServiceResult<List<Card>>();
            switch (connection.Status) {
                case ServiceResultStatus.OK:
                    serviceResult.Result =
                        await connection.Result
                            .Table<Card>()
                            .Where(i => i.PackageId == packageId)
                            .ToListAsync();
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
        public async Task<ServiceResult> EditAsyncCard(Card card) {
            var connection = _connectionService.GetAsyncConnection();
            var result = connection.Result.Table<Card>()
                .Where(i => i.Id == card.Id);
            if (await result.CountAsync() >= 1) {
                await connection.Result.UpdateAsync(card);
                return new ServiceResult() {
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
            } else {
                return new ServiceResult() {
                    Status = ServiceResultStatus.NotFound,
                    Message = "Not found the card with the primaryKey = " + card.Id + " in Card Table."
                };
            }
        }

        /// <inheritdoc />
        public async Task<ServiceResult> InsertAsyncCard(Card card) {
            var connection = _connectionService.GetAsyncConnection();
            try {
                await connection.Result.InsertAsync(card);
            } catch (SQLiteException e) {
                return new ServiceResult() {
                    Status = ServiceResultStatusHelper.FromSQLiteResult(e.Result),
                    Message = e.Message
                };
            }
            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult> InsertAsyncCards(List<Card> cards) {
            var connection = _connectionService.GetAsyncConnection();
            try {
                foreach (var card in cards) {
                    await connection.Result.InsertAsync(card);
                }
            } catch (SQLiteException e) {
                return new ServiceResult() {
                    Status = ServiceResultStatusHelper.FromSQLiteResult(e.Result),
                    Message = e.Message
                };
            }
            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult> DeleteAsyncCard(int cardId) {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAsync<Card>(cardId);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult> DeleteAsyncCards(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            var cardsList = await connection.Result.Table<Card>()
                .Where(i => i.PackageId == packageId)
                .ToListAsync();
            foreach (var card in cardsList) {
                await connection.Result.DeleteAsync<Card>(card.Id);
            }
            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult> DeleteAsyncAllCard() {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAllAsync<Card>();

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }


        //private ServiceResult<T> GetServiceResult<T>(T result, 
        //    ServiceResult<SqliteConnectionService> connection) {
        //    ServiceResult<T> serviceResult = new ServiceResult<T>();
        //    switch (connection.Status) {
        //        case ServiceResultStatus.OK:
        //            serviceResult.Result = result;
        //            serviceResult.Status = connection.Status;
        //            serviceResult.Message = connection.Message;
        //            break;
        //        default:
        //            serviceResult.Status = connection.Status;
        //            serviceResult.Message = connection.Message;
        //            break;
        //    }

        //    return serviceResult;
        //}
    }
}
