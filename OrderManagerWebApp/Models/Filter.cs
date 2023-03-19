using OrderManager.Db.Interfaces;
using System.Collections;

namespace OrderManagerWebApp.Models
{
    public class Filter
    {
        public IEnumerable<string> Providers { get; set; }
        public IEnumerable<string> OrderNumbers { get; set; }
        public IEnumerable<string> OrderItemNames { get; set; }
        public IEnumerable<string> OrderItemUnits { get; set; }
    }
}
