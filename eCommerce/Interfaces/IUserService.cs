using eCommerce.Models;
using eCommerce.Models.UserDto;

namespace eCommerce.Interfaces;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(Guid userId);
    Task DeleteUser(Guid userId);
    Task CreateUser(UserRegisterRequest request);
    Task UpdateUser(User user);
    User GetUserByEmailAsync(string email);
}
