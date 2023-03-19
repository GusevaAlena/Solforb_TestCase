namespace OrderManager.Db.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderItem, int orderId);
        Task<List<OrderItem>> TryGetByOrderIdAsync(int orderId);
        Task<OrderItem> TryGetByIdAsync(int id);
        Task UpdateAsync(OrderItem orderItem);
        Task DeleteAsync(int id);
    }
}
