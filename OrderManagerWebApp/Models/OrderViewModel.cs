using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace OrderManagerWebApp.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Укажите номер заказа")]
        public string Number { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        [Required(ErrorMessage ="Выберите поставщика из списка")]
        public Provider Provider { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public IEnumerable<SelectListItem> Providers { get; set; }
    }
}
