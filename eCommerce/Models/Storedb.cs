﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Models
{
    [Table("storedb")]
    public partial class Storedb
    {
        [Key]
        [Column("name")]
    
        public string Name { get; set; }
        [Column("uniqueStoreId")]
        public int UniqueStoreId { get; set; }
        [Column("id")]
        public int? Id { get; set; }
    }
}