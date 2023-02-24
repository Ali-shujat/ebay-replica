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


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Store>>> GetStores()
        //{
        //    return await _context.Stores.ToListAsync();
        //}
        //[HttpGet("storeadminproducts")]
        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> GetAllProductsByStoreAdmin()
        //{
        //    var identity = HttpContext..Identity as ClaimsIdentity;
        //    var userClaims = identity.Claims;
        //    var email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
        //    try
        //    {
        //        return Ok(await _storeService.GetStoreProducts(email));
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.Message.Contains("User not found"))
        //            NotFound("User does not exist");
        //        if (e.Message.Contains("Store not found"))
        //            NotFound("Store does not exist");
        //        throw new Exception(e.Message);
        //    }
        //}

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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStore(int id, string name)
        //{
        //    var store = await _context.Stores.FirstOrDefaultAsync(c => c.Id == id);
        //    if (store != null && name != null)
        //    {
        //        store.Name = name;
        //    }

        //    _context.Entry(store).State = EntityState.Modified;
        //    _context.Update(store);

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }

        //    return Ok(name);
        //}


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



        //[HttpDelete("{name}")]
        //public async Task<IActionResult> DeleteStore(string name)
        //{
        //    var store = await _context.Stores.FindAsync(name);
        //    if (store != null && name != null)
        //    {
        //        await _storeService.DeleteStore(name);
        //        _context.Stores.Remove(store);
        //    }
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool StoreExists(string name)
        {
            return _context.Stores.Any(e => e.Name == name);
        }


    }
}
