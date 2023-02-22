using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models;

public partial class Store
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int UniqueStoreId { get; set; }
}
