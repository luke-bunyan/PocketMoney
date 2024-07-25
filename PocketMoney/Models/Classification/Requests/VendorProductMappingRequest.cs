namespace PocketMoney.Models.Classification.Requests;

public class VendorProductMappingRequest
{
    public int ProductId { get; set; }

    public string TillCode { get; set; }

    public string TillDescription { get; set; }
}