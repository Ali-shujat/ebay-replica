using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data;

public partial class ecommerceContext : DbContext
{
    public ecommerceContext()
    {
    }

    public ecommerceContext(DbContextOptions<ecommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buyer> Buyers { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Store> Stores { get; set; } = null!;
    public virtual DbSet<User> Users { get; set; } = null!;
    public DbSet<CartItem> ShoppingCartItems { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ecommerce2;");
        }
    }
}
