using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManager.Db.Interfaces;
using OrderManagerWebApp.Models;

namespace OrderManagerWebApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly IProviderRepository providerRepository;
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(IProviderRepository providerRepository, IOrderItemRepository orderItemRepository, IOrderRepository orderRepository, IMapper mapper)
        {
            this.providerRepository = providerRepository;
            this.orderItemRepository = orderItemRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        public async Task<IActionResult> AddAsync()
        {
            var orderAddVM = new OrderViewModel();
            orderAddVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View("Create",orderAddVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderViewModel orderAddVM)
        {
            if (ModelState.IsValid)
            {
                orderAddVM.Provider = await providerRepository.TryGetByIdAsync(orderAddVM.Provider.Id);
                await orderRepository.CreateAsync(mapper.Map<Order>(orderAddVM));
                var orderCreatedId = (await orderRepository.TryGetByNumberAndProviderAsync(orderAddVM.Number,orderAddVM.Provider.Id)).Id;
                return RedirectToAction("Update", new {orderId = orderCreatedId});
            }
                
            orderAddVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(orderAddVM);
        }
        public async Task<IActionResult> UpdateAsync(int orderId)
        {
            var orderVM = new OrderViewModel();
            orderVM = mapper.Map<OrderViewModel>(await orderRepository.TryGetByIdAsync(orderId));
            orderVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(orderVM);
        }

        [HttpPost]
        public async Task<IActionResult> SaveChangesAsync(OrderViewModel orderVM, int orderId)
        {
            if (ModelState.IsValid)
            {
                await orderRepository.UpdateAsync(mapper.Map<Order>(orderVM));
                return RedirectToAction("Index", "Home");
            }

            orderVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(orderVM);
        }
    }
}
