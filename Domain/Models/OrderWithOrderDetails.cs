namespace Domain.Models;

public class OrderWithOrderDetails
{
   public int OrderId { get; set; }

   public List<OrderDetails> OrderDetails { get; set; }

}
