using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Classification;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Services;

public class VendorService(IDataContextFactory dataContextFactory, IProductService productService) : IVendorService
{
    public async Task<IEnumerable<Vendor>> GetVendorsAsync()
    {
        return await dataContextFactory.Get<Vendor>().GetAllEntriesAsync();
    }

    public async Task<Vendor> GetVendorAsync(int vendorId)
    {
        return await dataContextFactory.Get<Vendor>().GetEntryAsync(vendorId);
    }

    public async Task<Vendor> GetVendorAsync(string name)
    {
        var vendors = await dataContextFactory.Get<Vendor>().GetAllEntriesAsync(
            new Dictionary<string, dynamic>()
            {
                {"Name", name}
            });

        return vendors.First();
    }

    public async Task<Vendor> CreateVendorAsync(VendorRequest request)
    {
        var vendorContext = dataContextFactory.Get<Vendor>();

        if ((await vendorContext.GetAllEntriesAsync(
                new Dictionary<string, dynamic>
                {
                    {"Name", request.Name}
                })).Any())
            throw new ArgumentException($"Vendor {request.Name} already exists");

        return await vendorContext.SaveEntryAsync(new Vendor
        {
            CreatedDateTime = DateTime.UtcNow,
            Name = request.Name
        });
    }

    public async Task AddVendorProductMapping(VendorProductMappingRequest mapping)
    {
        var product = await productService.GetProductAsync(mapping.ProductId);

        if (product == null) throw new ArgumentException($"Product {mapping.ProductId} not found");

        var vpmContext = dataContextFactory.Get<VendorProductMapping>();

        await vpmContext.SaveEntryAsync(new VendorProductMapping
        {
            ProductId = mapping.ProductId,
            TillCode = mapping.TillCode,
            TillDescription = mapping.TillDescription
        });
    }
}