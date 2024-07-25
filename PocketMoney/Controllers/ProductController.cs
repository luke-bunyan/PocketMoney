using Microsoft.AspNetCore.Mvc;
using PocketMoney.Interfaces;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await productService.GetAllProductsAsync());
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        try
        {
            return Ok(await productService.GetProductAsync(productId));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductRequest product)
    {
        try
        {
            return Ok(await productService.CreateAccountAsync(product));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult> RemoveProduct(int productId)
    {
        try
        {
            await productService.DeleteProductAsync(productId);
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
}