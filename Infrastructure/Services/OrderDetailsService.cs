using Dapper;
using Domain;
using Domain.Models;
using Infrastructure.DataContext;

namespace Infrastructure.Services;

public class OrderDetailsService
{

   private readonly DapperContext context;

   public OrderDetailsService()
   {
    context=new DapperContext();
   }
   
   public  async Task<string>  AddOrderDetails(OrdersDetailForAdd orderDetails)
   {
      var ProductId= orderDetails.ProductId;
   
      var sql= "select StockQuantity from Products where ProductId=@ProductId";

      var StockQuantity=  await context.Connection().ExecuteScalarAsync<int>(sql,new{ProductId=ProductId});


      if(orderDetails.Quantity>StockQuantity) return $"NE KHVATAET ETOGO PRODUCTA ";

        StockQuantity-=orderDetails.Quantity;

        var sql1="update Products set StockQuantity=@StockQuantity where ProductId=@ProductId";

       await context.Connection().ExecuteAsync(sql1,new{StockQuantity=StockQuantity,ProductId=ProductId});

        
       var sql2=@"insert into OrderDetails(OrderId,ProductId,Quantity,UnitPrice)
                         values(@OrderId,@ProductId,@Quantity,@UnitPrice) ";

      await context.Connection().ExecuteAsync(sql2,orderDetails);
      
       return "USPESHNO DOBAVLEN ZAKAZ";

   }


   public async Task DeleteOrderDetails(int id)
   {
      var sql="delete from OrderDetails where OrderDetailsId=@id ";

      await context.Connection().ExecuteAsync(sql,new{OrderDetailsId=id});

   }

    public async Task UpdateOrderDetails(OrderDetails orderDetails)
    {
      var sql=@"Update OrderDetails set OrderId=@OrderId,
      ProductId=@ProductId,Quantity=@Quantity,
      UnitPrice=@UnitPrice where OrderDetailsId=@OrderDetailsId;";
      await context.Connection().ExecuteAsync(sql,orderDetails);
   }

   public async Task<List<OrderDetails>> GetOrderDetails()
   {
        var sql="select * from OrderDetails";

        var rezult = await context.Connection().QueryAsync<OrderDetails>(sql);
            return  rezult.ToList();
   }   

   
   




}
