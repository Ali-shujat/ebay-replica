namespace eCommerce.Models
{
    public class PostProductDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Price { get; set; } = null!;
        public int Quantity { get; set; }
        public string Category { get; set; } = null!;
    }
}
