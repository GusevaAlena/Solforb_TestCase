namespace OrderManager.Db.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAll();
    }
}
