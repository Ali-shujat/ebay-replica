namespace eCommerce.Services
{
    public interface IStoreService
    {

        Task DeleteStore(string storeName);
        Task CreateStoreAsync(string storeName);


    }
}
