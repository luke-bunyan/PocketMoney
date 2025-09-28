namespace PocketMoney.Models.Accounting.Requests;

public class TransactionRequest
{
    public int AccountId { get; set; }

    public decimal Amount { get; set; }

    public int VendorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual List<AllocationRequest> Allocations { get; set; }
}