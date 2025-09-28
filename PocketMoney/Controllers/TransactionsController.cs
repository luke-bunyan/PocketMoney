using Microsoft.AspNetCore.Mvc;
using PocketMoney.Interfaces;
using PocketMoney.Models.Accounting;
using PocketMoney.Models.Accounting.Requests;
using PocketMoney.Models.Accounting.Responses;

namespace PocketMoney.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionsController(ITransactionService transactionService,
    IAdapter<TransactionResponse, Transaction> transactionAdapter,
    IAllocationService allocationService) : ControllerBase
{
    [HttpGet("All")]
    public async Task<IActionResult> GetAllTransactions()
    {
        var transactions = await transactionService.GetTransactionsAsync();
        return Ok(await transactionAdapter.Adapt(transactions.OrderByDescending(x => x.CreatedDateTime)));
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] TransactionRequest transactionRequest)
    {
        try
        {
            return Ok(await transactionAdapter.Adapt(await transactionService.CreateTransactionAsync(transactionRequest)));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpGet("{TransactionId}")]
    public async Task<IActionResult> GetTransaction(int transactionId)
    {
        return Ok(await transactionService.GetTransactionAsync(transactionId));
    }

    [HttpDelete("{TransactionId}")]
    public async Task<IActionResult> RemoveAccount(int transactionId)
    {
        var transaction = await transactionService.GetTransactionAsync(transactionId);

        if (transaction == null)
        {
            return StatusCode(404, "Transaction not found");
        }

        try
        {
            await transactionService.RemoveTransaction(transaction);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(404, ex.Message);
        }
    }

    [HttpPut("{TransactionId}/Allocations")]
    public async Task<IActionResult> UpdateTransactionAllocations(int transactionId, IEnumerable<AllocationRequest>? allocations)
    {
        var transaction = await transactionService.GetTransactionAsync(transactionId);

        if (transaction == null)
        {
            return StatusCode(404, "Transaction not found");
        }

        try
        {
            return Ok(await allocationService.SetAllocationsAsync(transaction, allocations.ToList()));
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, ex.Message);
        }

    }


}