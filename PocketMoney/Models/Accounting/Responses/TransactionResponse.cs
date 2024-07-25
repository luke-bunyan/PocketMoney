namespace PocketMoney.Models.Accounting.Responses;

public class TransactionResponse(Transaction transaction)
{
    public int TransactionId { get; } = transaction.TransactionId;

    public DateTime CreatedDateTime { get; } = transaction.CreatedDateTime;

    public decimal Amount { get; } = transaction.Amount;

    public string AccountName { get; set; }

    public string VendorName { get; set; }

    public string? CategoryName { get; set; }

    public IEnumerable<AllocationResponse> Allocations { get; set; }
}
