using CardsForMemoryLibrary.Services;
using SQLite;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     数据库连接服务
    /// </summary>
    public interface ISqliteConnectionService {
        /// <summary>
        ///     获得数据库连接
        /// </summary>
        /// <returns>SqliteConnection</returns>
        ServiceResult<SQLiteAsyncConnection> GetAsyncConnection();
    }
}
