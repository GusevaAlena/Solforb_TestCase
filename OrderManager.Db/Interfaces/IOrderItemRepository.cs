using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Db.Interfaces
{
    public interface IOrderItemRepository
    {
        Task AddAsync(OrderItem orderItem, int orderId);
        Task<List<OrderItem>> TryGetByOrderId(int orderId);
        Task<OrderItem> TryGetByIdAsync(int id);
        Task UpdateAsync (OrderItem orderItem);
        Task DeleteAsync (int id);
    }
}
