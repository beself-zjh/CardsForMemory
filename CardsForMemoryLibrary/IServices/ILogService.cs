using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     日志服务
    /// </summary>
    public interface ILogService {
        /// <summary>
        ///     记录日志
        /// </summary>
        /// <param name="log"></param>
        Task Logging(Log log);
    }
}
