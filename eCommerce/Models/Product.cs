using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

public partial class Product
{
    [Key]
    public int ProductId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string Price { get; set; } = null!;
    public int Quantity { get; set; }
    public string Category { get; set; } = null!;

    public int StoreId { get; set; }
    public Store? Store { get; set; }
}
