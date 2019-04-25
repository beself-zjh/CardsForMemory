using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using SQLite;

namespace CardsForMemoryLibrary.Services {
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
