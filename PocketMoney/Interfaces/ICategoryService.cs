using PocketMoney.Models.Classification;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetAllCategoriesAsync();

    Task<Category?> GetCategoryAsync(int? categoryId);

    Task<Category> CreateCategoryAsync(CategoryRequest category);

    Task DeleteCategoryAsync(int categoryId);


}