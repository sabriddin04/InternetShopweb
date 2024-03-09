using Dapper;
using Domain.Models;
using Infrastructure.DataContext;
using Npgsql;
namespace Infrastructure.Services;

public class ProductService
{
    private readonly DapperContext context;

    public ProductService()
    {
        context=new DapperContext();
    }

      public async Task<List<Products>> GetProducts()
    {
        var sql="select * from Products";
        var rezult = await context.Connection().QueryAsync<Products>(sql);
        return rezult.ToList();
    }

    public async Task AddProducts(Products product)
    {
        var sql="insert into Products (ProductName,Price,StockQuantity) values(@ProductName,@Price,@StockQuantity)";

        await context.Connection().ExecuteAsync(sql,product);
    }

    public async Task DeleteProducts(int id)
    {
        var sql="delete from Products where ProductId=@id";
       await context.Connection().ExecuteAsync(sql,new{Id=id});
    }

    public async Task UpdateProducts (Products product)
    {
        var sql="update Products set ProductName=@ProductName,Price=@Price,StockQuantity=@StockQuantity where ProductId=@ProductId";

      await  context.Connection().ExecuteAsync(sql,product);
    }
 
    public async Task<List<ProductsCount>> GetProductsCounts()
    {
        var sql=@"select ProductName, sum(Quantity) as CountOfOrder
                   from Products
                   join orderdetails as od on products.ProductId=od.ProductId
                   group by ProductName";
        var rezult= await context.Connection().QueryAsync<ProductsCount>(sql);
        return rezult.ToList();
    }

    public  async Task<List<OrderBySum>> GetOrdersBySum(decimal totalAmount)
    {
        var sql = @"select orderId,Orders.orderdate,CustomerName
               from orders
                  join Customers on Customers.CustomerId=orders.CustomerId
                   where TotalAmount>@totalAmount";

         var rezult=await context.Connection().QueryAsync<OrderBySum>(sql, new {TotalAmount=totalAmount});
         return rezult.ToList();
    }


    public async Task< List<OrderWithOrderDetails>> GetOrdersWithOrderDetails()
    {
        var list = @" select OrderId from Orders   ";

         var list1= await context.Connection().QueryAsync<int>(list);
               

        var sql = @"select * from Orders where OrderId=@OrderId;
                    select * from OrderDetails where OrderId=@OrderId ";

        List<OrderWithOrderDetails> orders = new List<OrderWithOrderDetails>();
        foreach (var orderId in list1.ToList())
        {
            using (var multiple = await context.Connection().QueryMultipleAsync(sql, new { OrderId = orderId }))
            {
                OrderWithOrderDetails orderWithOrderDetails = new OrderWithOrderDetails();

                orderWithOrderDetails.OrderId= await multiple.ReadFirstAsync<int>();

                var list3 =  await multiple.ReadAsync<OrderDetails>();

                orderWithOrderDetails.OrderDetails = list3.ToList();

                orders.Add(orderWithOrderDetails);
           
            }

        }
       return   orders;


    }


}
