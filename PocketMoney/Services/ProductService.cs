using PocketMoney.Exceptions;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Classification;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Services;

public class ProductService(IDataContextFactory dataContextFactory) : IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return dataContextFactory.Get<Product>().GetAllEntriesAsync();
    }

    public async Task<Product?> GetProductAsync(int? productId)
    {
        if (productId == null) return null;
        try
        {
            return await dataContextFactory.Get<Product>().GetEntryAsync((int)productId);
        }
        catch(DataContextException)
        {
            return null;
        }
    }

    public async Task<Product?> GetProductFromVendorTillAsync(int vendorId, string tillCode)
    {
        try
        {
            var mapping = (await dataContextFactory.Get<VendorProductMapping>().GetAllEntriesAsync(
                new Dictionary<string, dynamic?>()
                {
                    {"VendorId", vendorId},
                    {"TillCode", tillCode},
                })).FirstOrDefault();

            if (mapping == null) return null;

            return await GetProductAsync(mapping.ProductId);
        }
        catch(DataContextException)
        {
            return null;
        }
    }

    public async Task<Product> CreateProductAsync(ProductRequest product)
    {
        if (await DoesProductExistAsync(product)) throw new ArgumentException($"product already exists");

        AssertProductRequestIsValid(product);

        return await dataContextFactory.Get<Product>().SaveEntryAsync(new Product
        {
            EanCode = product.EanCode!,
            Title = product.Title,
            Manufacturer = product.EanCode!,
            Brand = product.Brand!,
            Model = product.Model!,
            Description = product.Description!
        });
    }

    public async Task DeleteProductAsync(int productId)
    {
        var product = await GetProductAsync(productId) ?? throw new ArgumentException($"Product {productId} does not exist");

        await dataContextFactory.Get<Product>().RemoveEntryAsync(product.ProductId);
    }

    private async Task<bool> DoesProductExistAsync(ProductRequest product)
    {
        //TODO: Update this to filter dynamically based passed object
        return (await dataContextFactory.Get<Product>().GetAllEntriesAsync(new Dictionary<string, dynamic>()
        {
            {"Title", product.Title},
            {"EanCode", product.EanCode},
        })).Any();
    }

    private void AssertProductRequestIsValid(ProductRequest product)
    {
        if (product.Title == null) throw new ArgumentException($"Minimum product data not provided");
    }
}