using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using eCommerce.Models.UserDto;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : ControllerBase
    {
        public IUserService _userService { get; }
        public ecommerceContext _context { get; }

        public SuperAdminController(IUserService userService, ecommerceContext context)
        {
            _userService = userService;
            _context = context;
        }
        // GET: api/<SuperAdminController>
        [HttpGet]
        public async Task<IEnumerable<User>> GetAsync()
        {
            return (IEnumerable<User>)await _userService.GetAllUsers();
        }

        // GET api/<SuperAdminController>/5
        [HttpGet("{id}")]
        public async Task<User> GetAsync(Guid id)
        {
            return (User)await _userService.GetUserById(id);
        }



        // PUT api/<SuperAdminController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SuperAdminController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await _userService.DeleteUser(id);
        }
        [HttpPost("registerSuperAdmin")]
        public async Task<ActionResult<User>> SuperAdminRegisterAsync(UserRegisterRequest request)
        {
            LoggingService.Log("Register info =>> " +
                "DATE:" + DateTime.UtcNow.ToLongTimeString() +
            "UserEmail:" + request.Email);

            request.Role = "super-admin";
            await _userService.CreateUser(request);

            return Ok("User successfully created! \n Please check your email!");
        }
        [HttpPost("registerStoreAdmin")]
        public async Task<ActionResult<User>> StoreAdminRegisterAsync(UserRegisterRequest request)
        {
            LoggingService.Log("Register info =>> " +
                "DATE:" + DateTime.UtcNow.ToLongTimeString() +
                "UserEmail:" + request.Email);

            request.Role = "admin";
            await _userService.CreateUser(request);

            return Ok("User successfully created! \n Please check your email!");
        }
    }
}
