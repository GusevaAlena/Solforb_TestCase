using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;

namespace OrderManager.Db.Repositories
{
    public class OrderItemDbRepository : IOrderItemRepository
    {
        private readonly DatabaseContext databaseContext;
        public OrderItemDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(OrderItem orderItem, int orderId)
        {
            var currentOrder = await databaseContext.Orders
                .FirstOrDefaultAsync(x => x.Id == orderId);

            if (currentOrder == null)
            {
                currentOrder = new Order
                {
                    OrderItems = new List<OrderItem> { orderItem }
                };
                await databaseContext.Orders.AddAsync(currentOrder);
            }
            else
            {
                currentOrder.OrderItems.Add(orderItem);
            }

            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await TryGetByIdAsync(id);
            databaseContext.OrderItems.Remove(orderItem);
            await databaseContext.SaveChangesAsync();
        }

        public async Task<OrderItem> TryGetByIdAsync(int id)
        {
            return await databaseContext.OrderItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderItem>> TryGetByOrderIdAsync(int orderId)
        {
            return await databaseContext.OrderItems.Where(x => x.OrderId == orderId)
                .ToListAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            var currentOrderItem = await TryGetByIdAsync(orderItem.Id);
            currentOrderItem.Name = orderItem.Name;
            currentOrderItem.Quantity = orderItem.Quantity;
            currentOrderItem.Unit = orderItem.Unit;
            await databaseContext.SaveChangesAsync();
        }
    }
}
