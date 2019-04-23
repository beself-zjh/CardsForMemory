using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    /// <summary>
    ///     数据库操作服务接口
    /// </summary>
    public interface IAppointmentService {
        /// <summary>
        ///     添加一张卡片
        /// </summary>
        /// <param name="card">卡片信息</param>
        Task InsertAsync(Card card);

        /// <summary>
        ///     查找全部卡片
        /// </summary>
        Task<List<Card>> SelectAllAsync();

        /// <summary>
        ///     删除
        /// </summary>
        Task DeleteAllAsync();
    }
}
