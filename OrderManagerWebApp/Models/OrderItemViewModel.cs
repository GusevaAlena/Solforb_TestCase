using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace OrderManagerWebApp.Models
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Required(ErrorMessage = "Введите наименование позиции")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Укажите количество")]
        public decimal Quantity { get; set; }
        [Required(ErrorMessage = "Укажите единицу измерения")]
        public string Unit { get; set; }
    }
}
