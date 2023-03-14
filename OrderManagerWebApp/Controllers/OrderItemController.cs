using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    }
}
