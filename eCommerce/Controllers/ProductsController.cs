using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        private readonly ecommerceContext _context;

        public ProductsController(ecommerceContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        [HttpPut("{id}"), Authorize(Roles = "admin,super-admin")]
        public async Task<IActionResult> PutProduct(int id, PostProductDto productDto)
        {
            if (!ProductExists(id))
            {
                return BadRequest();
            }
            var product = _context.Products.Find(id);

            product.Title = productDto.Title;
            product.Description = productDto.Description;
            product.Category = productDto.Category;
            // product.Price = productDto.Price;
            product.Quantity = productDto.Quantity;
            product.Description = productDto.Description;
            product.ImageUrl = productDto.ImageUrl;



            _context.Entry(product).State = EntityState.Modified;
            _context.Update(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost, Authorize(Roles = "admin,super-admin")]
        public async Task<ActionResult<Product>> PostProduct(PostProductDto postProductDto, int uniqueId)
        {
            var newProduct = new Product()
            {
                ProductId = new int(),
                Title = postProductDto.Title,
                Category = postProductDto.Category,
                Description = postProductDto.Description,
                Price = postProductDto.Price,
                Quantity = postProductDto.Quantity,
                ImageUrl = postProductDto.ImageUrl,
                StoreId = uniqueId
            };

            _context.Products.Add(newProduct);
            var product_status = _context.Products.FirstOrDefaultAsync(p => p.Title == postProductDto.Title);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product_status.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProduct", new { id = newProduct.ProductId }, newProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}"), Authorize(Roles = "admin,super-admin")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
