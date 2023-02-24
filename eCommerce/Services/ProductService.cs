using eCommerce.Data;
using eCommerce.Interfaces;

namespace eCommerce.Services
{
    public class ProductService : IProductService
    {
        private ecommerceContext _dbContext;

        public ProductService(ecommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task Delete(int Id)
        {
            var account = _dbContext.Products.Find(Id);
            if (account != null)
            {
                _dbContext.Products.Remove(account);
                _dbContext.SaveChanges();
            }
            return Task.CompletedTask;
        }
    }
}
