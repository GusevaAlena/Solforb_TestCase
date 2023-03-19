public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public Provider()
    {
        Orders = new List<Order>();
    }
}
