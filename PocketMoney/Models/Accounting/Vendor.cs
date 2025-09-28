using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Accounting;

[SqlData("Vendors", "VendorId")]
public class Vendor
{
    public int VendorId { get; set; }

    public DateTime CreatedDateTime { get; set; }

    public string Name { get; set; }
}