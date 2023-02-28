using eCommerce.Data;
using eCommerce.Interfaces;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace eCommerce.Services
{
    public class StoreService : IStoreService
    {
        private ecommerceContext _dbContext;

        public StoreService(ecommerceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateStoreAsync(string storeName)
        {
            var nyaStore = new Store()
            {

                Name = storeName,
                StoreId = new int()
            };
            _dbContext.Stores.Add(nyaStore);

            _dbContext.SaveChangesAsync();


            return Task.CompletedTask;
        }

        public Task DeleteStore(Store store)
        {
            // var store = await _dbContext.Stores.FindAsync(storeName);
            if (store != null)
            {
                var productList = _dbContext.Products.Where(r => r.StoreId.Equals(store.StoreId));
                foreach (var product in productList)
                {
                    _dbContext.Products.Remove(product);
                }

                _dbContext.SaveChanges();
            }
            return Task.CompletedTask;
        }

        public async Task<StoreProductsDto> GetStoreProducts(string email)
        {
            var user = await _dbContext.Buyers.Where(x => x.Email == email).FirstOrDefaultAsync();
            if (user == null) throw new Exception("User not found");
            var store = await _dbContext.Stores.Where(x => x.StoreId == user.StoreId).FirstOrDefaultAsync();
            if (store == null) throw new Exception("Store not found");
            var products = await _dbContext.Products.Where(x => x.StoreId == user.StoreId).ToListAsync();
            return new StoreProductsDto
            {
                StoreId = store.StoreId,
                UniqueStoreId = store.StoreId,
                Name = store.Name,
                ProductsList = products
            };
        }

    }
}
