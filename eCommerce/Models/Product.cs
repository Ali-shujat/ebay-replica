using System;
using System.Collections.Generic;

namespace eCommerce.Models;

public partial class Product
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public int StoreId { get; set; }
    public string Price { get; set; } = null!;
    public int Quantity { get; set; }
    public string Category { get; set; } = null!;
}
