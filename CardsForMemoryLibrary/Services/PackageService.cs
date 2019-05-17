using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CardsForMemoryLibrary.Services {
    public class PackageService : IPackageService {
        /// <summary>
        ///     数据库连接服务
        /// </summary>
        private ISqliteConnectionService _connectionService;

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="connectionServiece"></param>
        public PackageService(ISqliteConnectionService connectionServiece) {
            _connectionService = connectionServiece;
        }

        public async Task<ServiceResult<Package>> AddPackageAsync(string name, string author, string description) {
            var connection = _connectionService.GetAsyncConnection();
            Package newPackage = new Package() {
                Author = author,
                Description = description,
                Name = name,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            await connection.Result.InsertAsync(newPackage);

            return new ServiceResult<Package>() {
                Result = newPackage,
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult> DeletePackageAsync(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAsync<Package>(packageId);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult> EditPackageAsync(Package package) {
            var connection = _connectionService.GetAsyncConnection();
            Package newPackage = new Package() {
                Id = package.Id,
                Author = package.Author,
                Description = package.Description,
                Name = package.Name,
                CreateTime = package.CreateTime,
                UpdateTime = DateTime.Now
            };
            await connection.Result.UpdateAsync(newPackage);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult<List<Package>>> GetAllPackageAsync() {
            var connection = _connectionService.GetAsyncConnection();
            var packageList = await connection.Result.Table<Package>().ToListAsync();

            return new ServiceResult<List<Package>>() {
                Result = packageList,
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult<Package>> GetPackageAsync(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            Package package = await connection.Result.FindAsync<Package>(packageId);

            return new ServiceResult<Package>() {
                Result = package,
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }
    }

    //采用继承来处理Virtual Package这个例外
    public class PackageServiceEx : PackageService {
        public PackageServiceEx(ISqliteConnectionService connectionServiece) : base(connectionServiece) { }

        public new async Task<ServiceResult> DeletePackageAsync(int packageId) {
            if (packageId != -1) {
                return await base.DeletePackageAsync(packageId);
            } else {
                return new ServiceResult {
                    Message = "Can't Delete Virtual Package",
                    Status = ServiceResultStatus.Error
                };
            }
        }

        public new async Task<ServiceResult> EditPackageAsync(Package package) {
            if (package.Id != -1) {
                return await base.EditPackageAsync(package);
            } else {
                return new ServiceResult {
                    Message = "Can't Edit Virtual Package",
                    Status = ServiceResultStatus.Error
                };
            }
        }

        public new async Task<ServiceResult<List<Package>>> GetAllPackageAsync() {
            var list = await base.GetAllPackageAsync();
            list.Result.Insert(0, new Package {
                Name = "Virtual Package",
                Author = "System",
                Description = "All Old Card in One",
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Style = 0,
                Id = -1
            });
            return list;
        }

        public new async Task<ServiceResult<Package>> GetPackageAsync(int packageId) {
            if (packageId == -1) {
                return new ServiceResult<Package>() {
                    Result = new Package {
                        Name = "Virtual Package",
                        Author = "System",
                        Description = "All Old Card in One",
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Style = 0,
                        Id = -1
                    },
                    Status = ServiceResultStatus.OK,
                    Message = "Success"
                };
            } else {
                return await base.GetPackageAsync(packageId);
            }
        }
    }
}
