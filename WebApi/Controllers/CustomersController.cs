using Domain.Models;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[controller]")]
public class CustomersController : ControllerBase
{
    private readonly CustomerService customerService;

    public CustomersController()
    {
        customerService = new CustomerService();
    }

    [HttpGet("get-customers")]

    public async Task <List<Customers>> GetCustomers()
    {
        return await customerService.GetCustomers();
    }

    [HttpPost("add-customers")]

    public async Task AddCustomers(Customers customer)
    {
       await customerService.AddCustomers(customer);
    }

    [HttpDelete("delete-customers")]

    public async Task DeleteCustomers(int id)
    {
       await customerService.DeleteCustomers(id);
    }

    [HttpPut("update-customers")]

    public async Task UpdateCustomers(Customers customer)
    {
       await customerService.UpdateCustomers(customer);
    }

    [HttpGet("GetCustomersWhichOrderInIterval")]

    public async Task<List<Customers>> GetCustomersWhichOrderInIterval(DateTime dateTime1, DateTime dateTime2)
    {
        return await customerService.GetCustomersWhichOrderInIterval(dateTime1, dateTime2);
    }

    [HttpGet("GetCustomersWithAverage")]

    public async Task< List<CustomerAverage>> GetCustomersWithEvarage()
    {

        return await customerService.GetCustomersWithEvarage();

    }
    
}