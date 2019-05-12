using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     卡包服务
    /// </summary>
    public interface IPackageService {
        /// <summary>
        ///     创建一个卡包
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult<int>> AppendAsyncPackage(string author, string description, string name);

        /// <summary>
        ///     删除一个卡包及其卡片
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult> DeleteAsyncPackage(int packageId);

        /// <summary>
        ///     编辑修改卡包信息
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult> EditAsyncPackage(Package package);

        /// <summary>
        ///     获取全部卡包信息
        /// </summary>
        Task<ServiceResult<List<Package>>> GetAsyncAllPackage();

        /// <summary>
        ///     获取某一卡包信息
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<Package>> GetAsyncPackage(int packageId);
    }
}
