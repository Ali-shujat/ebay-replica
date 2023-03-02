using eCommerce.Interfaces;
using eCommerce.Models;
using eCommerce.Models.UserDto;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace eCommerce.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly IJWTAuthService _authService;

    public AuthController(
        IConfiguration configuration,
        ILogger<AuthController> logger,
        IUserService userService,
        IJWTAuthService authService
        )
    {

        _configuration = configuration;
        _logger = logger;
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<User>> RegisterAsync(UserRegisterRequest request)
    {
        LoggingService.Log("Register info =>> " +
            "DATE:" + DateTime.UtcNow.ToLongTimeString() +
            "UserEmail:" + request.Email);

        _logger.LogInformation("Register page visited at {0} by user {1}",
            DateTime.UtcNow.ToLongTimeString(), request.Name);
        var result = await _userService.CreateUser(request);
        if (result)
        { return Ok("User successfully created! \n Please check your email!"); }
        return BadRequest("User already exists");
    }

    [HttpPost("login")]
    public IActionResult Login(UserLoginRequest request)
    {
        LoggingService.Log("Login info =>> "
            + "DATE:"
            + DateTime.UtcNow.ToLongTimeString()
            + "UserEmail:"
            + request.Email);
        _logger.LogInformation("login page visited at {0} by user :{1}",
            DateTime.UtcNow.ToLongTimeString(), request.Email);

        User user = _userService.GetUserByEmailAsync(request.Email);
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
        return Ok(new { Token = token, Role = user.Role });
    }

    [HttpPost("CheckTokenValidity")]
    public ActionResult<string> CheckTokenValidity(string token)
    {
        if (token == null) { return BadRequest("null value"); }
        _authService.IsTokenValid(token);
        return Ok("TOKEN is VALID!");
    }

    [HttpPost("GetClaim")]
    public ActionResult<IEnumerable<Claim>> GetClaim(string claimName)
    {
        if (claimName == null) { throw new ArgumentNullException(nameof(claimName)); }
        var tokenClaim = _authService.GetTokenClaims(claimName).ToList();
        return Ok(tokenClaim);
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

