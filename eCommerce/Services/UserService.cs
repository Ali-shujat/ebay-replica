using eCommerce.Data;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class UserService : IUserService
    {
        private readonly ecommerceContext _context;
        public UserService(ecommerceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
