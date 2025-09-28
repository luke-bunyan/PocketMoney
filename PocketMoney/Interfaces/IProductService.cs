using PocketMoney.Models.Classification;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();

    Task<Product?> GetProductAsync(int? productId);

    Task<Product> CreateProductAsync(ProductRequest product);

    Task DeleteProductAsync(int productId);

    Task<Product?> GetProductFromVendorTillAsync(int vendorId, string tillCode);
}