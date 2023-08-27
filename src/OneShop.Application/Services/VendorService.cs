using System;
using System.Numerics;
using OneShop.Application.Common.Interfaces;
using OneShop.Application.Interfaces;
using OneShop.Domain.Entities;

namespace OneShop.Application.Services
{
    public class VendorService : IVendorService
    {
        private readonly IRepository<Vendor> _repository;

        public VendorService(IRepository<Vendor> repository)
        {
            _repository = repository;
        }

        public async Task AddVendorAsync(Vendor vendor)
        {
            await _repository.AddAsync(vendor);
        }

        public async Task DeleteVendorAsync(Vendor vendor)
        {
            await _repository.DeleteAsync(vendor);
        }

        public async Task<IEnumerable<Vendor>> GetAllVendorsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Vendor?> GetVendorByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateVendorAsync(Vendor vendor)
        {
            await _repository.UpdateAsync(vendor);
        }
    }
}

