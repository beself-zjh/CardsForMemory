using System;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     数据分析服务
    /// </summary>
    public interface IAnalysisService {
        /// <summary>
        ///     卡片记忆率分析业务
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns>近30天记忆率变化情况</returns>
        List<Item> CardAnalysis(int cardId);
    }

    public class Item {
        public DateTime Time { get; set; }
        public int MemoryRatio { get; set; }
    }
}
