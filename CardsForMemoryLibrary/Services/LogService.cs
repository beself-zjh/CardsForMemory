﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class LogService : ILogService {
        /// <summary>
        ///     数据库连接服务
        /// </summary>
        private ISqliteConnectionService _connectionService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionService"></param>
        public LogService(ISqliteConnectionService connectionService) {
            _connectionService = connectionService;
        }
        
        public async Task Logging(Log log) {
            var connection = _connectionService.GetAsyncConnection();

            await connection.Result.InsertAsync(log);
        }
    }
}
