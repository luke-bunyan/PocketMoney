namespace PocketMoney.Models.Classification.Requests;

public class ProductRequest
{
    public string? EanCode { get; set; }

    public string Title { get; set; }

    public string? Manufacturer { get; set; }

    public string? Brand { get; set; }

    public string? Model { get; set; }

    public string? Description { get; set; }
}