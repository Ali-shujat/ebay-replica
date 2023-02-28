using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

public partial class Store
{
    [Key]
    public int StoreId { get; set; }
    public string Name { get; set; } = null!;
    public List<Product>? ProductList { get; set; }

}
