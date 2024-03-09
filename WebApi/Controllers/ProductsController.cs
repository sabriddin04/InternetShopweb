using Domain.Models;
using Infrastructure.DataContext;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Controller]
[Route("[Controller]")]
public class ProductsController : ControllerBase
{
    private readonly ProductService productService;

    public ProductsController()
    {
        productService = new ProductService();
    }

    [HttpPost("add-products")]

    public async Task AddProducts(Products product)
    {
       await  productService.AddProducts(product);
    }

    [HttpDelete("delete-products")]
    public async Task DeleteProducts(int id)
    {
       await productService.DeleteProducts(id);
    }

    [HttpGet("get-products")]

    public async Task< List<Products>> GetProducts()
    {
        return await productService.GetProducts();
    }

    [HttpPut("Update-products")]

    public async Task UpdateProducts(Products product)
    {
       await productService.UpdateProducts(product);
    }


    [HttpGet("Get-Products-Count-Which-to-Sell")]

    public async Task<List<ProductsCount>> GetProductsCounts()
    {
        return  await productService.GetProductsCounts();
    }

    [HttpGet("GetOrdersBySum")]
    public async Task< List<OrderBySum>> GetOrdersBySum(decimal totalAmount)
    {

      return  await productService.GetOrdersBySum(totalAmount);

    }
    [HttpGet("GetOrdersWithOrderDetails")]

    public async Task< List<OrderWithOrderDetails>> GetOrdersWithOrderDetails()
    {
        return await productService.GetOrdersWithOrderDetails();
    }


    }
