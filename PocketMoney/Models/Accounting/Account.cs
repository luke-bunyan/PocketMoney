using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Accounting;

[SqlData("Accounts", "AccountId")]
public class Account
{
    public int AccountId { get; set; }

    public string Name { get; set; }
}