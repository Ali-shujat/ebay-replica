using eCommerce.Models;

namespace eCommerce.Interfaces
{
    public interface IStoreService
    {

        void DeleteStore(Store store);
        Task CreateStoreAsync(string storeNam);
        Task<StoreProductsDto> GetStoreProducts(string email);


    }
}
