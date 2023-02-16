namespace eCommerce.Services
{
    public interface IStoreService
    {
        string GetStoreName();
        Task DeleteStore(string storeName);
        Task<Task> CreateStore(string storeName);


    }
}
