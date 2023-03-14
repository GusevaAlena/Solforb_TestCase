using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderManagerWebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public Provider Provider { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}
