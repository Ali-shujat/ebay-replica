﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Models
{
    [Table("productdb")]
    public partial class Productdb
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("title")]
      
        public string Title { get; set; }
        [Required]
        [Column("description")]
      
        public string Description { get; set; }
        [Required]
        [Column("imageUrl")]
       
        public string ImageUrl { get; set; }
        [Column("storeId")]
        public int StoreId { get; set; }
        [Required]
        [Column("price")]
    
        public string Price { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Required]
        [Column("category")]
    
        public string Category { get; set; }
    }
}