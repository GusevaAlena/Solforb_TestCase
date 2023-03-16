using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManager.Db.Interfaces;
using OrderManagerWebApp.Models;
using System.Diagnostics;

namespace OrderManagerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProviderRepository providerRepository;
        public HomeController(IOrderRepository orderRepository, IProviderRepository providerRepository)
        {
            this.orderRepository = orderRepository;
            this.providerRepository = providerRepository;
        }
        public async Task<IActionResult> Index()
        {
            var oVM = new HomePageViewModel();
            var orders = await orderRepository.GetAllAsync();
            oVM.Orders = orders;
            oVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            oVM.OrderNumbers = orders.Select(x => x.Number)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x });
            oVM.OrderItemNames = orders.SelectMany(x => x.OrderItems)
                .Select(x => x.Name)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x });
            oVM.OrderItemUnits = orders.SelectMany(x => x.OrderItems)
                .Select(x => x.Unit)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x });
            return View(oVM);
        }

        [HttpPost]
        public IActionResult Filter(HomePageViewModel homePageVM)
        {
            return View("Index", homePageVM);
        }
    }
}