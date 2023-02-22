using eCommerce.Data;
using eCommerce.Models;
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
                UniqueStoreId = _dbContext.Stores.Count() + 1
            };
            _dbContext.Stores.Add(nyaStore);

            _dbContext.SaveChangesAsync();


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
    }
}
