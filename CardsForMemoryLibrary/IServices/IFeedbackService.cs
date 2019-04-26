using System;
using System.Collections.Generic;
using System.Text;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     反馈服务
    /// </summary>
    public interface IFeedbackService {
        /// <summary>
        ///     简单
        /// </summary>
        /// <param name="card"></param>
        void IsEasy(Card card);

        /// <summary>
        ///     困难
        /// </summary>
        /// <param name="card"></param>
        void IsDifficult(Card card);

        /// <summary>
        ///     一般
        /// </summary>
        /// <param name="card"></param>
        void IsNormal(Card card);

        /// <summary>
        ///     提交答案(针对选择题)
        /// </summary>
        /// <param name="card"></param>
        /// <param name="answer"></param>
        /// <returns>True:答对，False:答错</returns>
        bool Submit(Card card, string answer);
    }
}
