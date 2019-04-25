using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public interface IPackageService {
        /// <summary>
        ///     创建一个卡包
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult> InsertAsyncPackage(Package package);

        /// <summary>
        ///     删除一个卡包及其卡片
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult> DeleteAsyncPackage(int packageId);

        /// <summary>
        ///     编辑修改卡片信息
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult> EditAsyncPackage(Package package);

        /// <summary>
        ///     获取全部卡包信息
        /// </summary>
        Task<ServiceResult<List<Package>>> GetAsyncAllPackage();

        /// <summary>
        ///     获取某一卡片信息
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<Package>> GetAsyncSinglePackage(int packageId);
    }
}
