using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.Services {
    /// <summary>
    ///     卡片服务
    /// </summary>
    public class CardService : ICardService {
        /// <summary>
        ///     数据库连接服务
        /// </summary>
        private ISqliteConnectionService _connectionService;

        private static List<int> SpecialPackage = new List<int>();

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionService"></param>
        public CardService(ISqliteConnectionService connectionService) {
            _connectionService = connectionService;
        }

        /// <inheritdoc />
        public async Task<ServiceResult<Card>> GetCardAsync(int cardId) {
            var connection = _connectionService.GetAsyncConnection();
            ServiceResult<Card> serviceResult = new ServiceResult<Card>();
            switch (connection.Status) {
                case ServiceResultStatus.OK:
                    serviceResult.Result =
                        await connection.Result
                            .FindAsync<Card>(cardId);
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
        public async Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId) {
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

        public async Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId, int Old, int New) {
            List<Card> cards = (await GetCardsAsync(packageId)).Result
                                .OrderByDescending
                                    (i => Math.Min(10000, 10000 - 5600 * Math.Pow((DateTime.Now - i.UpdateTime).Hours, 0.06) + i.Proficiency))
                                .ToList();

            var oldStart = cards.FindIndex(i => i.UpdateTime == DateTime.MinValue);

            cards = cards.Skip(oldStart - Old).Take(Old + New).ToList();

            //乱序
            Random random = new Random();
            var newList = new List<Card>();
            foreach (Card item in cards) {
                newList.Insert(random.Next(newList.Count), item);
            }

            return new ServiceResult<List<Card>>() {
                Result = newList,
                Message = "Success",
                Status = ServiceResultStatus.OK
            };

            ////排序
            //cards.Sort((c1, c2) => {
            //    if (c1.Proficiency == c2.Proficiency) {
            //        return 0;
            //    } else if (c1.Proficiency > c2.Proficiency) {
            //        return 1;
            //    } else {
            //        return -1;
            //    }
            //});
            ////选旧牌
            //for (int i = 0; i < Old; i++) {
            //    sorted.Add(cards[i]);
            //}
            ////选新牌
            //for (int i = 1; i <= New; i++) {
            //    sorted.Add(cards[cards.Count - i]);
            //}
        }

        public async Task<ServiceResult<int>> GetNewCardNum(int packageId) {
            List<Card> cards = (await GetCardsAsync(packageId)).Result;

            return new ServiceResult<int>() {
                Result = cards.Count(i => i.UpdateTime == DateTime.MinValue),
                Message = "Success",
                Status = ServiceResultStatus.OK
            };
        }

        public async Task<ServiceResult<int>> GetOldCardNum(int packageId) {
            List<Card> cards = (await GetCardsAsync(packageId)).Result;

            return new ServiceResult<int>() {
                Result = cards.Count(i => i.UpdateTime != DateTime.MinValue),
                Message = "Success",
                Status = ServiceResultStatus.OK
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult<List<Card>>> GetAllCardsAsync() {
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
        public async Task<ServiceResult> EditCardAsync(Card card) {
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
        public async Task<ServiceResult<Card>> AddCardAsync(int packageId, string question, string answer) {
            Card card = new Card() {
                PackageId = packageId,
                Question = question,
                Answer = answer,
                Proficiency = 0,
                UpdateTime = DateTime.MinValue
            };
            var connection = _connectionService.GetAsyncConnection();
            try {
                await connection.Result.InsertAsync(card);
            } catch (SQLiteException e) {
                return new ServiceResult<Card>() {
                    Status = ServiceResultStatusHelper.FromSQLiteResult(e.Result),
                    Message = e.Message,
                    Result = null
                };
            }
            return new ServiceResult<Card>() {
                Status = ServiceResultStatus.OK,
                Message = "Success",
                Result = card
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult> DeleteCardAsync(int cardId) {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAsync<Card>(cardId);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        /// <inheritdoc />
        public async Task<ServiceResult> DeleteCardsAsync(int packageId) {
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

        public async Task<ServiceResult> DeleteAllCardAsync() {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAllAsync<Card>();

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public ServiceResult BeOld(int cardId) {
            if (!SpecialPackage.Contains(cardId))
                SpecialPackage.Append(cardId);
            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }
    }

    public class CardServiceEx : CardService, ICardService {
        public CardServiceEx(ISqliteConnectionService connectionService) : base(connectionService) { }

        public new async Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId) {
            if (packageId == -1) {
                var connection = new SqliteConnectionService().GetAsyncConnection();
                ServiceResult<List<Card>> serviceResult = new ServiceResult<List<Card>>() {
                    Result = await connection.Result
                            .Table<Card>()
                            .Where(i => i.UpdateTime == DateTime.MinValue)
                            .ToListAsync(),
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
                return serviceResult;

            } else {
                return await base.GetCardsAsync(packageId);
            }
        }

        public new async Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId, int Old, int New) {
            if (packageId != -1) {
                return await base.GetCardsAsync(packageId, Old, New);
            } else {
                return new ServiceResult<List<Card>>() {
                    Result = (await GetCardsAsync(packageId)).Result.OrderBy(i => i.Proficiency).Take(Old).ToList(),
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
            }
        }

        public new async Task<ServiceResult<int>> GetNewCardNum(int packageId) {
            if (packageId != -1) {
                return await base.GetNewCardNum(packageId);
            } else {
                return new ServiceResult<int>() {
                    Result = 0,
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
            }
        }

        public new async Task<ServiceResult<int>> GetOldCardNum(int packageId) {
            if (packageId != -1) {
                return await base.GetOldCardNum(packageId);
            } else {
                return new ServiceResult<int>() {
                    Result = (await GetCardsAsync(packageId)).Result.Count(),
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
            }
        }

        public new async Task<ServiceResult<Card>> AddCardAsync(int packageId, string question, string answer) {
            if (packageId != -1) {
                return await base.AddCardAsync(packageId, question, answer);
            } else {
                return new ServiceResult<Card>() {
                    Status = ServiceResultStatus.Error,
                    Message = "不能向虚拟卡包中插入卡片"
                };
            }
        }

        public new async Task<ServiceResult> DeleteCardsAsync(int packageId) {
            if (packageId != -1) {
                return await base.DeleteCardsAsync(packageId);
            } else {
                return new ServiceResult() {
                    Status = ServiceResultStatus.Error,
                    Message = "没有删除权限"
                };
            }
        }
    }
}
