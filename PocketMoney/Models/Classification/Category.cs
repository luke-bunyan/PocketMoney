using PocketMoney.Integrations.Persistance;

namespace PocketMoney.Models.Classification;

[SqlData("Categories", "CategoryId")]
public class Category
{
    public int CategoryId { get; set; }

    public int? ParentCategoryId { get; set; }

    public string? Name { get; set; }
}