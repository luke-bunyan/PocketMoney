using System.ComponentModel.DataAnnotations.Schema;
using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Accounting;

[SqlData("Transactions", "TransactionId")]
public class Transaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public decimal Amount { get; set; }

    public int VendorId { get; set; }

    public int? CategoryId { get; set; }
}