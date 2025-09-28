using Microsoft.AspNetCore.Mvc;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Classification.Requests;

namespace PocketMoney.Controllers;

[ApiController]
[Route("[controller]")]
public class VendorController(IVendorService vendorService, ITransactionService transactionService, IProductService productService) : ControllerBase
{

    [HttpGet("All")]
    public async Task<IActionResult> GetAllVendors()
    {
        return Ok(await vendorService.GetVendorsAsync());
    }

    [HttpPost]
    public async Task<IActionResult> CreateVendor([FromBody] VendorRequest vendorRequest)
    {
        try
        {
            return Ok(await vendorService.CreateVendorAsync(vendorRequest));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpGet("{vendorId}")]
    public async Task<IActionResult> GetVendor(int vendorId)
    {
        var vendor = await vendorService.GetVendorAsync(vendorId);

        if (vendor == null)
        {
            return StatusCode(404, "Vendor not found");
        }

        return Ok(vendor);
    }

    [HttpGet("{vendorId}/Transactions")]
    public async Task<IActionResult> GetVendorTransactions(int vendorId)
    {
        var vendor = await vendorService.GetVendorAsync(vendorId);

        if (vendor == null)
        {
            return StatusCode(404, "Vendor not found");
        }

        return Ok(await transactionService.GetTransactionsAsync(vendor));
    }

    [HttpPost("{vendorId}/Product")]
    public async Task<IActionResult> CreateVendorTillProducts([FromBody]VendorProductMappingRequest productMapping)
    {
        try
        {
            await vendorService.AddVendorProductMapping(productMapping);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }

    [HttpGet("{vendorId}/Product/{tillCode}")]
    public async Task<IActionResult> CreateVendorTillProducts(int vendorId, string tillCode)
    {
        try
        {
            return Ok(await productService.GetProductFromVendorTillAsync(vendorId, tillCode));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }
    }
}