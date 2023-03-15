using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Concurrent;

namespace OrderManagerWebApp.Models
{
    public class HomePageViewModel
    {
        public DateTime Today { get; set; } = DateTime.Now;
        public DateTime MonthAgo { get; set; } = DateTime.Now.AddMonths(-1);
        public List<Order> Orders { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }
        public IEnumerable<SelectListItem> OrderNumbers { get; set; }
        public IEnumerable<SelectListItem> OrderItemNames { get; set; }
        public IEnumerable<SelectListItem> OrderItemUnits { get; set; }

    }
}
