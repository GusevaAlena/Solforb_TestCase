using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Concurrent;

namespace OrderManagerWebApp.Models
{
    public class OrdersViewModel
    {
        public DateTime Today { get; set; } = DateTime.Now;
        public DateTime MonthAgo { get; set; } = DateTime.Now.AddMonths(-1);
        public List<Order> Orders { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }
        public OrdersViewModel()
        {
            Orders = new List<Order>();
        }
    }
}
