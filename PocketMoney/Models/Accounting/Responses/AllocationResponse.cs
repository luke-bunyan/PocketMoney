namespace PocketMoney.Models.Accounting.Responses;

public class AllocationResponse(Allocation allocation)
{
    public decimal Amount { get; set; } = allocation.Amount;

    public string? ProductName { get; set; }
}