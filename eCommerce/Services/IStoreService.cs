namespace eCommerce.Services
{
    public interface IStoreService
    {
        string GetStoreName();
        Task DeleteStore(string storeName);
        Task CreateStoreAsync(string storeName);


    }
}
