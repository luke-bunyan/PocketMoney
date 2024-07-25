using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Classification;

[SqlData("PocketMoney", "VendorProductMappings", "VendorProductMappingId")]
public class VendorProductMapping
{
    public int VendorProductMappingId { get; set; }

    public int ProductId { get; set; }

    public string TillCode { get; set; }

    public string TillDescription { get; set; }
}