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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ecommerce;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.ToTable("Buyer");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Email)
                .HasMaxLength(31)
                .IsUnicode(false)
                .HasColumnName("email");

            entity.Property(e => e.Password)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.Property(e => e.Role)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("role");

            entity.Property(e => e.UniqueStoreId).HasColumnName("uniqueStoreId");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");

            entity.Property(e => e.Category)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("category");

            entity.Property(e => e.Description)
                .HasMaxLength(552)
                .IsUnicode(false)
                .HasColumnName("description");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(47)
                .IsUnicode(false)
                .HasColumnName("imageUrl");

            entity.Property(e => e.Price)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("price");

            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.Property(e => e.StoreId).HasColumnName("storeId");

            entity.Property(e => e.Title)
                .HasMaxLength(62)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Name)
                .HasName("PK__Store__72E12F1AA1577EED");

            entity.ToTable("Store");

            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.UniqueStoreId).HasColumnName("uniqueStoreId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
