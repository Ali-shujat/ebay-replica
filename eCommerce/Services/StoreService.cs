using eCommerce.Data;
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

        public async Task<Task> CreateStore(string storeName)
        {
            var storeCount = _dbContext.Stores.FirstOrDefaultAsync(s => s.Name == storeName);
            if (storeCount == null)
            {
                Random rnd = new Random();
                var nyaStore = new Store()
                {
                    Id = rnd.Next(1, 100),
                    Name = storeName,
                    UniqueStoreId = _dbContext.Stores.Count() + 1
                };
                _dbContext.Stores.Add(nyaStore);
            }

            await _dbContext.SaveChangesAsync();


            return Task.CompletedTask;
        }

        public Task DeleteStore(string storeName)
        {
            var store = _dbContext.Stores.Find(storeName);
            if (store != null)
            {
                var productList = _dbContext.Products.Where(r => r.StoreId.Equals(store.UniqueStoreId));
                foreach (var product in productList)
                {
                    _dbContext.Products.Remove(product);
                }

                _dbContext.SaveChanges();
            }



            return Task.CompletedTask;

        }

        public string GetStoreName()
        {
            throw new NotImplementedException();
        }
    }
}
