using eCommerce.Data;
using eCommerce.Models;
using eCommerce.Models.UserDto;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ecommerceContext _context;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ecommerceContext context, IConfiguration configuration, ILogger<AuthController> logger)
    {
        _context = context;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(UserRegisterRequest request)
    {
        LoggingService.Log("Register info =>> " +
            "DATE:" + DateTime.UtcNow.ToLongTimeString() +
            "UserEmail:" + request.Email);

        _logger.LogInformation("Register page visited at {0} by user {1}",
            DateTime.UtcNow.ToLongTimeString(), request.Name);

        return Ok("User successfully created!");
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginRequest request)
    {
        LoggingService.Log("Login info =>>" + "DATE:" + DateTime.UtcNow.ToLongTimeString() + "UserEmail:" + request.Email);
        _logger.LogInformation("login page visited at {0} by user :{1}",
            DateTime.UtcNow.ToLongTimeString(), request.Email);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user == null)
        {
            return BadRequest("User not found.");
        }

        if (!HashTokenService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return BadRequest("Password is incorrect.");
        }

        if (user.VerifiedAt == null)
        {
            return BadRequest("Not verified!");
        }

        //adding JWT token
        string token = CreateToken(user);


        //return Ok($"Welcome back, {user.Email}! :)");
        return Ok(new { Token = token, User = user });
    }



    private string CreateToken(User user)
    {
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

}

