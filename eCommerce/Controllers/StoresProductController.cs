using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Roles = "super-admin")]
    public class StoresProductController : ControllerBase
    {
        private readonly ecommerceContext _context;
        private IStoreService _storeService;

        public StoresProductController(ecommerceContext context, IStoreService storeService)
        {
            _context = context;
            _storeService = storeService;

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
            try
            {
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


        private bool StoreExists(string name)
        {
            return _context.Stores.Any(e => e.Name == name);
        }


    }
}
