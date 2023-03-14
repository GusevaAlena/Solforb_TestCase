using Microsoft.EntityFrameworkCore;
using OrderManager.Db.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Db.Repositories
{
    public class OrderItemDbRepository : IOrderItemRepository
    {
        private readonly DatabaseContext databaseContext;
        public OrderItemDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public async Task AddAsync(OrderItem orderItem, int? orderId)
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
        public async Task<List<OrderItem>> TryGetByOrderId(int orderId)
        {
            return await databaseContext.OrderItems.Where(x=>x.OrderId==orderId)
                .ToListAsync();
        }
    }
}
