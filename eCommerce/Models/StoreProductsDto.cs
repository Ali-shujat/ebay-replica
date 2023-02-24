namespace eCommerce.Models;

public class StoreProductsDto
{
    public int StoreId { get; set; }
    public int UniqueStoreId { get; set; }
    public string? Name { get; set; }
    public List<Product>? ProductsList { get; set; }
}
