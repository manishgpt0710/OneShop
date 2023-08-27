using System;
using OneShop.Domain.Entities;

namespace OneShop.Application.Interfaces
{
    public interface IVendorService
    {
        Task<IEnumerable<Vendor>> GetAllVendorsAsync();
        Task<Vendor?> GetVendorByIdAsync(long id);
        Task AddVendorAsync(Vendor vendor);
        Task UpdateVendorAsync(Vendor vendor);
        Task DeleteVendorAsync(Vendor vendor);
    }
}

