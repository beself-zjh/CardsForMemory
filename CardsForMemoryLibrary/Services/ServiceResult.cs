using SQLite;
using System.Collections.Generic;

namespace CardsForMemoryLibrary.Services {
    /// <summary>
    ///     服务结果
    /// </summary>
    public class ServiceResult {
        public ServiceResultStatus Status { get; set; }

        public string Message { get; set; }
    }

    /// <summary>
    ///     服务结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResult<T> {
        public T Result { get; set; }

        public ServiceResultStatus Status { get; set; }

        public string Message { get; set; }
    }

    public enum ServiceResultStatus {
        /// <summary>
        ///     未知
        /// </summary>
        Unknown = -100,

        /// <summary>
        ///     异常
        /// </summary>
        Exception = -200,

        /// <summary>
        ///     OK
        /// </summary>
        OK = -300,

        Error = -400,
        Internal = -500,
        Busy = -800,
        Locked = -900,
        NoMem = -1000,
        ReadOnly = -1100,
        Interrupt = -1200,
        IOError = -1300,
        Corrupt = -1400,
        NotFound = -1500,
        Full = -1600,
        CannotOpen = -1700,
        LockErr = -1800,
        Empty = -1900,
        TooBig = -2000,
        Constraint = -2100,
        Mismatch = -2200
    }

    public class ServiceResultStatusHelper {
        /// <summary>
        ///     状态字典
        /// </summary>
        private static readonly Dictionary<SQLite3.Result, ServiceResultStatus>
            StatusDictionary = 
                new Dictionary<SQLite3.Result, ServiceResultStatus>();

        /// <summary>
        ///     静态构造函数
        /// </summary>
        static ServiceResultStatusHelper() {
            StatusDictionary[SQLite3.Result.OK] = 
                ServiceResultStatus.OK;
            StatusDictionary[SQLite3.Result.Error] =
                ServiceResultStatus.Error;
        }

        public static ServiceResultStatus FromSQLiteResult(
            SQLite3.Result sqliterResult) {
            return StatusDictionary.ContainsKey(sqliterResult)
                ? StatusDictionary[sqliterResult]
                : ServiceResultStatus.Unknown;
        }
    }
}
