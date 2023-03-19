namespace OrderManager.Db.Interfaces
{
    public interface IProviderRepository
    {
        Task<List<Provider>> GetAllAsync();
        Task<Provider> TryGetByIdAsync(int id);
    }
}
