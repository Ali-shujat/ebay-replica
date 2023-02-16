using eCommerce.Data;
using eCommerce.Models;
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

    public AuthController(ecommerceContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<ActionResult<Buyer>> Register(BuyerDto request)
    {
        if (_context.Buyers.Any(u => u.Email == request.Email))
        {
            return BadRequest("User already exists.");
        }
        Random rnd = new Random();
        var buyer = new Buyer
        {
            Id = _context.Buyers.Count() + 1,
            Email = request.Email,
            Password = request.Password,
            Role = "user"
        };

        _context.Buyers.Add(buyer);
        await _context.SaveChangesAsync();

        return Ok("User successfully created!");
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(BuyerLoginRequest request)
    {
        var buyer = await _context.Buyers.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (buyer == null)
        {
            return BadRequest("User not found.");
        }

        if (buyer.Password != request.Password)
        {
            return BadRequest("Password is incorrect.");
        }

        //adding JWT token
        string token = CreateToken(buyer);

        //return Ok($"Welcome back, {user.Email}! :)");
        return Ok(token);
    }



    private string CreateToken(Buyer buyer)
    {
        List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, buyer.Email),
                new Claim(ClaimTypes.Role, buyer.Role)
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

