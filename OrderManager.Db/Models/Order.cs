public class Order
{
    public int Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public Provider Provider { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public Order()
    {
        OrderItems = new List<OrderItem>();
    }
}
