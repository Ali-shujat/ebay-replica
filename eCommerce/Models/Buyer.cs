﻿using System;
using System.Collections.Generic;

namespace eCommerce.Models;

public partial class Buyer
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role { get; set; } = null!;
    public int UniqueStoreId { get; set; }
}
