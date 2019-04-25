using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    /// <summary>
    ///     卡片编辑服务
    /// </summary>
    public interface ICardService {
        /// <summary>
        ///     单卡片获取
        /// </summary>
        /// <param name="cardId">卡片ID</param>
        Task<ServiceResult<Card>> GetAsyncSingleCard(int cardId);

        /// <summary>
        ///     卡包卡片获取
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<List<Card>>> GetAsyncCardInPackage(int packageId);

        /// <summary>
        ///     全卡片获取
        /// </summary>
        Task<ServiceResult<List<Card>>> GetAsyncAllCards();

        /// <summary>
        ///     修改某个卡片
        /// </summary>
        /// <param name="card">修改后的卡片实例</param>
        Task<ServiceResult> EditAsyncSingleCard(Card card);

        /// <summary>
        ///     插入卡片
        /// </summary>
        /// <param name="card"></param>
        Task<ServiceResult> InsertAsyncSingleCard(Card card);

        /// <summary>
        ///     插入一组卡片
        /// </summary>
        /// <param name="cards">卡片组</param>
        Task<ServiceResult> InsertAsyncPluralCard(List<Card> cards);
    }
}
