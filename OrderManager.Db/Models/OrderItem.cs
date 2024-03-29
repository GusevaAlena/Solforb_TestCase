﻿using Microsoft.EntityFrameworkCore;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string Name { get; set; }

    [Precision(18, 3)]
    public decimal Quantity { get; set; }
    public string Unit { get; set; }
}
