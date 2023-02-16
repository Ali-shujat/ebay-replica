namespace eCommerce.Models;

public partial class Store
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int UniqueStoreId { get; set; }
}
