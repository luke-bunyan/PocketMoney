using PocketMoney.Exceptions;
using PocketMoney.Integrations.Persistance;
using PocketMoney.Interfaces;
using PocketMoney.Models.Classification;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Services;

public class CategoryService(IDataContextFactory dataContextFactory) : ICategoryService
{
    public Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return dataContextFactory.Get<Category>().GetAllEntriesAsync();
    }

    public async Task<Category?> GetCategoryAsync(int? categoryId)
    {
        if (categoryId == null) return null;
        try
        {
            return await dataContextFactory.Get<Category>().GetEntryAsync((int)categoryId);
        }
        catch(DataContextException)
        {
            return null;
        }
    }

    public async Task<Category> CreateCategoryAsync(CategoryRequest category)
    {
        if (await DoesCategoryExistAsync(new Category {Name = category.Name})) throw new ArgumentException($"{category.Name} already exists");

        return await dataContextFactory.Get<Category>().SaveEntryAsync(new Category
        {
            ParentCategoryId = category.ParentCategoryId,
            Name = category.Name
        });
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var category = await GetCategoryAsync(categoryId) ?? throw new ArgumentException($"CategoryId {categoryId} does not exist");

        await dataContextFactory.Get<Category>().RemoveEntryAsync(category.CategoryId);
    }

    private async Task<bool> DoesCategoryExistAsync(Category account)
    {
        //TODO: Update this to filter dynamically based passed object
        return (await dataContextFactory.Get<Category>().GetAllEntriesAsync(new Dictionary<string, dynamic?>()
        {
            {"Name", account.Name}
        })).Any();
    }
}