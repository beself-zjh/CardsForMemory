using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     反馈服务
    /// </summary>
    public interface IFeedbackService {
        /// <summary>
        ///     复习反馈
        /// </summary>
        /// <param name="card"></param>
        Task Utility(Card card, Level level);

        /// <summary>
        ///     提交答案(针对选择题)
        /// </summary>
        /// <param name="card"></param>
        /// <param name="answer"></param>
        /// <returns>True:答对，False:答错</returns>
        bool Submit(Card card, string answer);
    }

    /// <summary>
    ///     反馈情况
    /// </summary>
    public enum Level {
        Easy = 10,
        Normal = 20,
        Difficult = 30
    }
}
