using CardsForMemoryLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     复习服务
    /// </summary>
    public interface IReviseService {
        /// <summary>
        ///     智能复习(出题)
        /// </summary>
        /// <param name="packages">复习范围</param>
        /// <param name="num">题量</param>
        /// <returns>题目</returns>
        Task<List<Card>> SmartRevise(List<int> packages, int num);
    }
}
