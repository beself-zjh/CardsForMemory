using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     卡片服务
    /// </summary>
    public interface ICardService {
        /// <summary>
        ///     单卡片获取
        /// </summary>
        /// <param name="cardId">卡片ID</param>
        Task<ServiceResult<Card>> GetAsyncCard(int cardId);

        /// <summary>
        ///     卡包卡片获取
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<List<Card>>> GetAsyncCards(int packageId);

        /// <summary>
        ///     全卡片获取
        /// </summary>
        Task<ServiceResult<List<Card>>> GetAsyncAllCards();

        /// <summary>
        ///     修改某个卡片
        /// </summary>
        /// <param name="card">
        ///     The card is required to have a primary key.
        /// </param>
        Task<ServiceResult> EditAsyncCard(Card card);

        /// <summary>
        ///     插入卡片
        /// </summary>
        /// <param name="card">
        ///     The card is required to have no primary key.
        /// </param>
        Task<ServiceResult> InsertAsyncCard(Card card);

        /// <summary>
        ///     插入一组卡片
        /// </summary>
        /// <param name="cards">卡片组</param>
        Task<ServiceResult> InsertAsyncCards(List<Card> cards);

        /// <summary>
        ///     删除一个卡片
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsyncCard(int cardId);

        /// <summary>
        ///     删除卡包内所有卡片
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsyncCards(int packageId);

        /// <summary>
        ///     删除全部卡片
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> DeleteAsyncAllCard();
    }
}
