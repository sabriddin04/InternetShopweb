namespace Domain.Models;

public class Orders
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public DateTime OrderDate { get; set; }

    public int TotalAmount { get; set; }
}
