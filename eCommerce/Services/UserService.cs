using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using eCommerce.Models.UserDto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

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

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {
                Name = request.Name,
                Role = request.Role,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                VerificationToken = CreateRandomToken()

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

        public Task<IEnumerable<Product>> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUserById(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
        private string CreateToken(User user)
        {
            //var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            List<Claim> claims = new List<Claim>
            {
                  new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac
                    .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
