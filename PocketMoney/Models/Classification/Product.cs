using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Classification;

[SqlData("PocketMoney", "Products", "ProductId")]
public class Product
{
    public int ProductId { get; set; }

    public string EanCode { get; set; }

    public string Title { get; set; }

    public string Manufacturer { get; set; }

    public string Brand { get; set; }

    public string Model { get; set; }

    public string Description { get; set; }
}