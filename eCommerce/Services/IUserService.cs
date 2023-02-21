using eCommerce.Models;

namespace eCommerce.Services
{
    public interface IUserService
    {
        Task<IEnumerable<Product>> GetAllProduct();

    }
}
