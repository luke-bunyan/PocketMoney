namespace PocketMoney.Models.Accounting;

public class Allocation
{
    public int AllocationId { get; set; }

    public int? TransactionId { get; set; }

    public decimal Amount { get; set; }

    public int? ProductId { get; set; }
}