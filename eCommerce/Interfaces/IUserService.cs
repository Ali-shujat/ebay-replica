using eCommerce.Models;

namespace eCommerce.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Product>> GetAllProduct();

    }
}
