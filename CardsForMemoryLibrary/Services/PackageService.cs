using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CardsForMemoryLibrary.IServices;
using CardsForMemoryLibrary.Models;

namespace CardsForMemoryLibrary.Services {
    public class PackageService : IPackageService{
        public async Task<ServiceResult> InsertAsyncPackage(Package package) {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> DeleteAsyncPackage(int packageId) {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult> EditAsyncPackage(Package package) {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<List<Package>>> GetAsyncAllPackage() {
            throw new NotImplementedException();
        }

        public async Task<ServiceResult<Package>> GetAsyncPackage(int packageId) {
            throw new NotImplementedException();
        }
    }
}
