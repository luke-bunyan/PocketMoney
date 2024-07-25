namespace PocketMoney.Models.Accounting.Requests;

public class AllocationRequest
{
    public decimal Amount { get; set; }

    public int? ProductId { get; set; }
}