using CardsForMemoryLibrary.Models;
using System.Threading.Tasks;

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
