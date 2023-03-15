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
            oVM.Orders = await orderRepository.GetAllAsync();
            oVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            return View(oVM);
        }     
    }
}