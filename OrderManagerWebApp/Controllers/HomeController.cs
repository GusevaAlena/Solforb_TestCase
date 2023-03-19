using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OrderManager.Db.Interfaces;
using OrderManagerWebApp.Models;

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
            var homePageVM = new HomePageViewModel();
            await GetDataAsync(homePageVM);
            return View(homePageVM);
        }

        [HttpPost]
        public async Task<IActionResult> FilterAsync(HomePageViewModel homePageVM)
        {
            await GetDataAsync(homePageVM);
            var result = homePageVM.Orders
                .Where(x => x.Date >= homePageVM.MonthAgo && x.Date <= homePageVM.Today);

            if (homePageVM.Filter != null)
            {
                result = result
                    .Where(x => FilterHelper.IsContainValueInFilter(x.Provider.Id.ToString(), homePageVM.Filter.Providers))
                    .Where(x => FilterHelper.IsContainValueInFilter(x.Number, homePageVM.Filter.OrderNumbers))
                    .Where(x => x.OrderItems.Any(y => FilterHelper.IsContainValueInFilter(y.Name, homePageVM.Filter.OrderItemNames)))
                    .Where(x => x.OrderItems.Any(y => FilterHelper.IsContainValueInFilter(y.Unit, homePageVM.Filter.OrderItemUnits)));
            }

            homePageVM.Orders = result;
            return View("Index", homePageVM);
        }
        private async Task GetDataAsync(HomePageViewModel homePageVM)
        {
            var orders = await orderRepository.GetAllAsync();
            homePageVM.Orders = orders;
            homePageVM.Providers = (await providerRepository.GetAllAsync())
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                .OrderBy(x => x.Text);
            homePageVM.OrderNumbers = orders.Select(x => x.Number)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x })
                .OrderBy(x => x.Text);
            homePageVM.OrderItemNames = orders.SelectMany(x => x.OrderItems)
                .Select(x => x.Name)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x })
                .OrderBy(x => x.Text);
            homePageVM.OrderItemUnits = orders.SelectMany(x => x.OrderItems)
                .Select(x => x.Unit)
                .Distinct()
                .Select(x => new SelectListItem { Value = x, Text = x })
                .OrderBy(x => x.Text);
        }
    }
}