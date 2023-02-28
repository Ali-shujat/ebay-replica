using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "super-admin")]
    public class StoresProductController : ControllerBase
    {
        private readonly ecommerceContext _context;
        private IStoreService _storeService;
        private readonly IJWTAuthService _authService;

        public StoresProductController(ecommerceContext context, IStoreService storeService, IJWTAuthService authService)
        {
            _context = context;
            _storeService = storeService;
            _authService = authService;
        }

        [HttpGet("{email}"), Authorize(Roles = "admin")]
        public async Task<ActionResult<StoreProductsDto>> GetStoresProduct(string email)
        {
            if (email == null)
            {
                return NotFound();
            }
            try
            {
                return Ok(await _storeService.GetStoreProducts(email));
            }
            catch (Exception e)
            {
                if (e.Message.Contains("User not found"))
                    NotFound("User does not exist");
                if (e.Message.Contains("Store not found"))
                    NotFound("Store does not exist");
                throw new Exception(e.Message);
            }
        }


        [HttpPost, Authorize(Roles = "super-admin")]
        public async Task<ActionResult<Store>> PostStore(string storeName)
        {
            //_context.Stores.Add(store);
            try
            {
                // await _context.SaveChangesAsync();
                await _storeService.CreateStoreAsync(storeName);
            }
            catch (DbUpdateException)
            {
                if (StoreExists(storeName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction("GetStore", new { id = storeName }, storeName);
        }

        [HttpPost("CheckTokenValidity")]
        public ActionResult<string> CheckTokenValidity(string token)
        {
            if (token == null) { return BadRequest("null value"); }
            _authService.IsTokenValid(token); return Ok("TOKEN is VALID!");
        }

        [HttpPost("GetClaim")]
        public ActionResult<IEnumerable<Claim>> GetClaim(string claimName)
        {
            if (claimName == null) { throw new ArgumentNullException(nameof(claimName)); }
            var tokenClaim = _authService.GetTokenClaims(claimName).ToList();
            return Ok(tokenClaim);
        }

        private bool StoreExists(string name)
        {
            return _context.Stores.Any(e => e.Name == name);
        }


    }
}
