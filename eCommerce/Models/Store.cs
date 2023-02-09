using System;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public partial class Store
    {
        public string Name { get; set; } = null!;
        public int UniqueStoreId { get; set; }
        public int? Id { get; set; }
    }
}
