﻿using System;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
    public Provider()
    {
        Orders = new List<Order>();
    }
}