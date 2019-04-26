using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class PackageService : IPackageService {
        private ISqliteConnectionService _connectionService;

        public PackageService(ISqliteConnectionService connectionServiece) {
            _connectionService = connectionServiece;
        }

        public async Task<ServiceResult> InsertAsyncPackage(Package package) {
            var connection = _connectionService.GetAsyncConnection();
            Package newPackage = new Package() {
                Author = package.Author,
                Description = package.Description,
                Name = package.Name,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            await connection.Result.InsertAsync(newPackage);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult> DeleteAsyncPackage(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            await connection.Result.DeleteAsync<Package>(packageId);

            return new ServiceResult() {
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult> EditAsyncPackage(Package package) {
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

        public async Task<ServiceResult<List<Package>>> GetAsyncAllPackage() {
            var connection = _connectionService.GetAsyncConnection();
            var packageList = await connection.Result.Table<Package>().ToListAsync();

            return new ServiceResult<List<Package>>() {
                Result = packageList,
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }

        public async Task<ServiceResult<Package>> GetAsyncPackage(int packageId) {
            var connection = _connectionService.GetAsyncConnection();
            Package package = await connection.Result.FindAsync<Package>(packageId);

            return new ServiceResult<Package>() {
                Result = package,
                Status = ServiceResultStatus.OK,
                Message = "Success"
            };
        }
    }
}
