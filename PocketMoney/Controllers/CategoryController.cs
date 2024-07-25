using Microsoft.AspNetCore.Mvc;
using PocketMoney.Interfaces;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(ICategoryService categoryService, ITransactionService transactionService) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAllAccounts()
    {
        return Ok(await categoryService.GetAllCategoriesAsync());
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetAccount(int categoryId)
    {
        try
        {
            return Ok(await categoryService.GetCategoryAsync(categoryId));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest category)
    {
        try
        {
            return Ok(await categoryService.CreateCategoryAsync(category));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> RemoveCategory(int categoryId)
    {
        try
        {
            await categoryService.DeleteCategoryAsync(categoryId);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpGet("{categoryId}/Transactions")]
    public async Task<IActionResult> GetAllTransactionsForCategory(int categoryId)
    {
        var category = await categoryService.GetCategoryAsync(categoryId);

        if (category == null)
        {
            return StatusCode(404, "Category not found");
        }

        return Ok(await transactionService.GetTransactionsAsync(category));
    }
}