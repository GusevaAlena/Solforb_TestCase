using Microsoft.EntityFrameworkCore;
using System;

public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [Precision(18,3)]
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}
