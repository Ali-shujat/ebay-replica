using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using eCommerce.Models.UserDto;

namespace eCommerce.Services
{
    public class UserService : IUserService
    {
        private readonly ecommerceContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ecommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Task CreateUser(UserRegisterRequest request)
        {
            if (_context.Users.Any(u => u.Email == request.Email))
            {
                return Task.FromResult("User already exists.");
            }

            HashTokenService.CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {
                Name = request.Name,
                Role = request.Role,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = HashTokenService.CreateRandomToken()

            };
            var config = _configuration["Yahoo:Password"];
            //send email method
            SendEmail.SkickaEmail(request.Email, config, user.VerificationToken);
            _context.Users.Add(user);
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }

        public Task DeleteUser(Guid userId)
        {
            var account = _context.Users.Find(userId);
            if (account != null)
            {
                _context.Users.Remove(account);
                _context.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserById(Guid userId)
        {
            User usr = await _context.Users.FindAsync(userId);
            return usr;
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmailAsync(string email)
        {
            var user = _context.Users.Where(w => w.Email == email).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            return user;

        }


    }
}
