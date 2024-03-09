using Infrastructure.DataContext;
using Domain.Models;
using Dapper;
using Microsoft.VisualBasic;
namespace Infrastructure.Services;

public class CustomerService
{
    private readonly DapperContext context;

    public CustomerService()
    {
        context = new DapperContext();

    }

    public async Task<List<Customers>> GetCustomers()
    {
        var sql="select * from Customers";
        var rezult = await context.Connection().QueryAsync<Customers>(sql);
        return  rezult.ToList();
    }

    public async Task AddCustomers(Customers customer)
    {
        var sql="insert into Customers (CustomerName,Email,Address) values(@CustomerName,@Email,@Address)";

        await context.Connection().ExecuteAsync(sql, customer);
    }

    public async Task DeleteCustomers(int id)
    {
        var sql="delete from Customers where CustomerId=@id";
       await context.Connection().ExecuteAsync(sql,new{Id=id});
    }

    public async Task UpdateCustomers(Customers customer)
    {
        var sql="Update Customers set CustomerName=@CustomerName,Email=@Email,Address=@Address where CustomerId=@CustomerId ";

       await context.Connection().ExecuteAsync(sql,customer);
    }

    public async Task<List<Customers>> GetCustomersWhichOrderInIterval(DateTime dateTime1,DateTime dateTime2)
    {
        var sql=@"select * from Customers
         join orders on orders.CustomerId=customers.CustomerId
         where (OrderDate>@dateTime1 or OrderDate=@dateTime1) and (OrderDate<@dateTime2 or OrderDate=@dateTime2) ";

       var rezult= await  context.Connection().QueryAsync<Customers>(sql,new{DateTime1=dateTime1,DateTime2=dateTime2});
        return rezult.ToList();
    }


    public async Task<List<CustomerAverage>>GetCustomersWithEvarage(){

        var sql = @"select CustomerName,avg(TotalAmount)as Average
             from Customers
             join orders on Customers.CustomerId=orders.CustomerId
             group by CustomerName";

         var rezult= await  context.Connection().QueryAsync<CustomerAverage>(sql);
         return rezult.ToList();  
    }




}
