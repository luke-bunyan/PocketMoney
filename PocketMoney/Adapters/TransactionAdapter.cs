using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Responses;

namespace PocketMoney.Adapters;

public class TransactionAdapter(IAccountService accountService, IVendorService vendorService, ICategoryService categoryService, IProductService productService) : IAdapter<TransactionResponse, Transaction>
{
    public async Task<TransactionResponse> Adapt(Transaction request)
    {
        var response = new TransactionResponse(request)
        {
            AccountName = (await accountService.GetAccountAsync(request.AccountId)).Name,
            VendorName = (await vendorService.GetVendorAsync(request.VendorId)).Name
        };

        var category = await categoryService.GetCategoryAsync(request.CategoryId);
        var categoryName = category?.Name;
        var parentCategoryId = category?.ParentCategoryId ?? null;

        while (parentCategoryId != null)
        {
            var parentCategory = await categoryService.GetCategoryAsync(parentCategoryId);
            categoryName = parentCategory?.Name + " > " + categoryName;
            parentCategoryId = parentCategory?.ParentCategoryId;
        }

        response.CategoryName = categoryName;

        var allocations = new List<AllocationResponse>();
        foreach (var allocation in request.Allocations)
        {
            allocations.Add(new AllocationResponse(allocation)
            {
                ProductName = (await productService.GetProductAsync(allocation.ProductId))?.Title
            });
        }

        response.Allocations = allocations;

        return response;
    }

    public async Task<IEnumerable<TransactionResponse>> Adapt(IEnumerable<Transaction> request)
    {
        var response = new List<TransactionResponse>();

        foreach (var item in request)
        {
            response.Add(await Adapt(item));
        }

        return response;
    }
}