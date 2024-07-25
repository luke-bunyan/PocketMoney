using System.ComponentModel.DataAnnotations.Schema;

namespace PocketMoney.Models.Accounting;

public class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public decimal Amount { get; set; }

    public int VendorId { get; set; }

    public int? CategoryId { get; set; }

    [NotMapped]
    public virtual IEnumerable<Allocation> Allocations { get; set; }
}