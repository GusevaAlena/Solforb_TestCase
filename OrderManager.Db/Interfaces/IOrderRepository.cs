namespace OrderManager.Db.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task CreateAsync(Order order);
        Task<Order> TryGetByIdAsync(int id);
        Task<Order> TryGetByNumberAndProviderAsync(string number, int providerId);
        Task UpdateAsync(Order order);
        Task DeleteAsync(int orderId);
    }
}
