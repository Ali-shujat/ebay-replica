using eCommerce.Models;
using eCommerce.Models.UserDto;

namespace eCommerce.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Product>> GetAllUsers();
        Task<IEnumerable<User>> GetUserById(Guid userId);
        Task DeleteUser(Guid userId);
        Task CreateUser(UserRegisterRequest request);
        Task UpdateUser(User user);


    }
}
