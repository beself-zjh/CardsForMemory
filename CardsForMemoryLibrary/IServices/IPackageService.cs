using CardsForMemoryLibrary.Models;
using CardsForMemoryLibrary.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.IServices {
    /// <summary>
    ///     卡包服务
    /// </summary>
    public interface IPackageService {
        /// <summary>
        ///     创建一个卡包
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult<Package>> AddPackageAsync(string name, string author, string description);

        /// <summary>
        ///     删除一个卡包及其卡片
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult> DeletePackageAsync(int packageId);

        /// <summary>
        ///     编辑修改卡包信息
        /// </summary>
        /// <param name="package"></param>
        Task<ServiceResult> EditPackageAsync(Package package);

        /// <summary>
        ///     获取全部卡包信息
        /// </summary>
        Task<ServiceResult<List<Package>>> GetAllPackageAsync();

        /// <summary>
        ///     获取某一卡包信息
        /// </summary>
        /// <param name="packageId">卡包ID</param>
        Task<ServiceResult<Package>> GetPackageAsync(int packageId);
    }
}
