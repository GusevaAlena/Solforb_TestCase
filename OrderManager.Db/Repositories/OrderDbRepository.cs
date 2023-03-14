using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace OrderManager.Db.Repositories
{
    public class OrderDbRepository : IOrderRepository
    {
        private readonly DatabaseContext databaseContext;
        public OrderDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders
                .Include(x => x.OrderItems).Include(x => x.Provider).ToListAsync();
        }

        public async Task<Order> TryGetByIdAsync(int id)
        {
            return await databaseContext.Orders
                .Include(x => x.OrderItems).Include(x => x.Provider)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Order> TryGetByNumberAndProviderAsync(string number, int providerId)
        {
            return await databaseContext.Orders
                .Include(x => x.OrderItems).Include(x => x.Provider)
                .FirstOrDefaultAsync(x => x.Number == number && x.Provider.Id == providerId);
        }
        public async Task CreateAsync(Order order)
        {
            await databaseContext.Orders.AddAsync(order);
            await databaseContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Order order)
        {
            var currentOrder = await TryGetByIdAsync(order.Id);
            currentOrder.Number = order.Number;
            currentOrder.Provider = order.Provider;
            currentOrder.OrderItems = order.OrderItems;
            currentOrder.Date = order.Date;
            await databaseContext.SaveChangesAsync();
        }
    }
}
