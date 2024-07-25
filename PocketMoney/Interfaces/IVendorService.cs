using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Interfaces;

public interface IVendorService
{
    Task<IEnumerable<Vendor>> GetVendorsAsync();

    Task<Vendor> GetVendorAsync(int vendorId);

    Task<Vendor> GetVendorAsync(string name);

    Task<Vendor> CreateVendorAsync(VendorRequest request);

    Task AddVendorProductMapping(VendorProductMappingRequest mapping);
}