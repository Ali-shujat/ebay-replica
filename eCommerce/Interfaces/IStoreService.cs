using eCommerce.Models;

namespace eCommerce.Interfaces
{
    public interface IStoreService
    {

        Task DeleteStore(Store store);
        Task CreateStoreAsync(string storeNam);
        Task<StoreProductsDto> GetStoreProducts(string email);


    }
}
