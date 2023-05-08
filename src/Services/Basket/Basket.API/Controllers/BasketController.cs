using System.Runtime.InteropServices.Marshalling;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketController: ControllerBase
{
    private readonly IBasketRepository _repository;

    public BasketController(IBasketRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{username}", Name = "GetBasket")]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
    {
        var basket = await _repository.GetBasket(username);
        return Ok(basket ?? new ShoppingCart(username));
    }
    
    [HttpPost]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        return Ok(await _repository.UpdateBasket(basket));
    }
    
    [HttpDelete("{userName}", Name = "DeleteBasket")]
    public async Task<ActionResult<ShoppingCart>> DeleteBasket(string userName)
    {
        await _repository.DeleteBasket(userName);
        return Ok();
    }
    
    
}