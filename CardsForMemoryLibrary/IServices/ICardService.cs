using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     卡片服务
    /// </summary>
    public interface ICardService {
        /// <summary>
        ///     单卡片获取
        /// </summary>
        /// <param name="cardId">卡片ID</param>
        Task<ServiceResult<Card>> GetCardAsync(int cardId);

        /// <summary>
        ///     卡包卡片获取
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId);

        //todo Karl:根据id,旧卡数量,新卡数量,返回一个List<Card>
        //Task<ServiceResult<List<Card>>> GetCardsAsync(int packageId);
        
        //todo Karl:根据id,返回旧卡数量
        //Task<ServiceResult<int>> 

        //todo Karl:根据id,返回新卡数量
        //Task<ServiceResult<int>> 

        /// <summary>
        ///     全卡片获取
        /// </summary>
        Task<ServiceResult<List<Card>>> GetAllCardsAsync();

        /// <summary>
        ///     修改某个卡片
        /// </summary>
        /// <param name="card">
        ///     The card is required to have a primary key.
        /// </param>
        Task<ServiceResult> EditCardAsync(Card card);

        /// <summary>
        ///     插入卡片
        /// </summary>
        /// <param name="card">
        ///     The card is required to have no primary key.
        /// </param>
        Task<ServiceResult<Card>> AddCardAsync(int packageId, string question, string answer);

        /// <summary>
        ///     删除一个卡片
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteCardAsync(int cardId);

        /// <summary>
        ///     删除卡包内所有卡片
        /// </summary>
        /// <param name="packageId"></param>
        /// <returns></returns>
        Task<ServiceResult> DeleteCardsAsync(int packageId);

        /// <summary>
        ///     删除全部卡片
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult> DeleteAllCardAsync();
    }
}
