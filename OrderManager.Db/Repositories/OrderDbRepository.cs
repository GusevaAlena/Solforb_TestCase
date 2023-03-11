using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;

namespace OrderManager.Db.Repositories
{
    public class OrderDbRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;
        public OrderDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Order>> GetAll()
        {
            return await databaseContext.Orders.ToListAsync();
        }
    }
}
