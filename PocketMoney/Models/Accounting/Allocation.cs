using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Accounting;

[SqlData("Allocations", "AllocationId")]
public class Allocation
{
    public int AllocationId { get; set; }

    public int? TransactionId { get; set; }

    public decimal Amount { get; set; }

    public int? ProductId { get; set; }
}