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
            var orderAddVM = new OrderViewModel
            {
                Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
            };
            return View("Create", orderAddVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OrderViewModel orderVM)
        {
            var order = await orderRepository.TryGetByNumberAndProviderAsync(orderVM.Number, orderVM.Provider.Id);

            if (order != null)
            {
                ModelState.AddModelError("", "Заказ с таким номером у данного поставщика уже существует! " +
                    "Измените номер заказа или выберите другого поставщика из списка");
            }

            if (ModelState.IsValid)
            {
                orderVM.Provider = await providerRepository.TryGetByIdAsync(orderVM.Provider.Id);
                await orderRepository.CreateAsync(mapper.Map<Order>(orderVM));
                var orderCreatedId = (await orderRepository.TryGetByNumberAndProviderAsync(orderVM.Number, orderVM.Provider.Id)).Id;
                return RedirectToAction("Update", new { orderId = orderCreatedId });
            }

            orderVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(orderVM);
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
        public async Task<IActionResult> SaveChangesAsync(OrderViewModel orderVM)
        {
            var orderCreated = await orderRepository.TryGetByNumberAndProviderAsync(orderVM.Number, orderVM.Provider.Id);

            if (orderCreated != null)
            {
                if (orderVM.Id != orderCreated.Id)
                {
                    ModelState.AddModelError("", "Заказ с таким номером у данного поставщика уже существует! " +
                        "Измените номер заказа или выберите другого поставщика из списка");
                }
            }

            if (ModelState.IsValid)
            {
                await orderRepository.UpdateAsync(mapper.Map<Order>(orderVM));
                return RedirectToAction("Index", "Home");
            }

            orderVM.OrderItems = await orderItemRepository.TryGetByOrderIdAsync(orderVM.Id);
            orderVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View("Update", orderVM);
        }

        public async Task<IActionResult> DetailsAsync(int orderId)
        {
            return View(mapper.Map<OrderViewModel>(await orderRepository.TryGetByIdAsync(orderId)));
        }

        public async Task<IActionResult> DeleteAsync(int orderId)
        {
            await orderRepository.DeleteAsync(orderId);
            return RedirectToAction("Index", "Home");
        }
    }
}
