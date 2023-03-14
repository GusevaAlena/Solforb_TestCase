using Microsoft.AspNetCore.Mvc;
using OrderManager.Db.Interfaces;
using OrderManagerWebApp.Models;
using System.Diagnostics;

namespace OrderManagerWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOrderRepository orderRepository;
        public HomeController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<IActionResult> Index()
        {
            var oVM = new OrdersViewModel();
            oVM.Orders = await orderRepository.GetAllAsync();
            return View(oVM);
        }     
    }
}