namespace PocketMoney.Models.Classification.Requests;

public class CategoryRequest
{
    public int? ParentCategoryId { get; set; }

    public string Name { get; set; }
}