using System;
using System.Collections.Generic;

public class Order
{
    public int Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    
    public Order()
    {
        OrderItems = new List<OrderItem>();
    }
}
