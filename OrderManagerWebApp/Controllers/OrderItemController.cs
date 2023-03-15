using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using OrderManager.Db.Interfaces;
using OrderManagerWebApp.Models;

namespace OrderManagerWebApp.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
        }

        public IActionResult Add(int orderId)
        {
            var orderItemVM = new OrderItemViewModel();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(OrderItemViewModel orderItemVM, int orderId)
        {
            await orderItemRepository.AddAsync(mapper.Map<OrderItem>(orderItemVM), orderId);
            return RedirectToAction("Update", "Order", new {orderId});
        }

        public async Task<IActionResult> UpdateAsync(int orderItemId)
        {
            var orderItem = await orderItemRepository.TryGetByIdAsync(orderItemId);
            return View(mapper.Map<OrderItemViewModel>(orderItem));
        }

        [HttpPost]
        public async Task<IActionResult> SaveChangesAsync(OrderItemViewModel orderItemVM)
        {
            if (ModelState.IsValid)
            {
                await orderItemRepository.UpdateAsync(mapper.Map<OrderItem>(orderItemVM));
                return RedirectToAction("Update", "Order", new {orderId=orderItemVM.OrderId});
            }
            return View(orderItemVM);
        }

        public async Task<IActionResult> DeleteAsync(int orderItemId, int orderId)
        {
            await orderItemRepository.DeleteAsync(orderItemId);
            return RedirectToAction("Update", "Order", new { orderId });
        } 
    }
}
