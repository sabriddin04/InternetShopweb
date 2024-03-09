namespace WebApi.Controllers;
using Domain.Models;
using Infrastructure.DataContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

[Controller]
[Route("[Controller]")]
public class OrderDetailsController: ControllerBase
{
    private readonly OrderDetailsService orderDetailsService;

    public OrderDetailsController()
    {
        orderDetailsService = new OrderDetailsService();
    }

    [HttpPost("add-orderdetails")]

    public async  Task<string> AddOrderDetails(OrdersDetailForAdd orderDetails)
    {
        return await orderDetailsService.AddOrderDetails(orderDetails);
    }


    [HttpGet("GetAllOrderDetails")]

    public async Task<List<OrderDetails>> GetOrderDetails()
    {
        return  await orderDetailsService.GetOrderDetails();
    }


    [HttpDelete("DeleteOrderDetails")]

    public async Task DeleteOrderDetails(int id)
    {
       await orderDetailsService.DeleteOrderDetails(id);
    }

    [HttpPut("UpdateOrderDetails")]

    public async Task UpdateOrderDetails(OrderDetails orderDetails)
    {
        await orderDetailsService.UpdateOrderDetails(orderDetails);
    }


    }
