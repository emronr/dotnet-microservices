using System.Net;
using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketsController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    
    
    [HttpGet("{username}", Name = "GetBasket")]
    [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> GetBasket(string username)
    {
        var basket = await _basketRepository.GetBasket(username);
        return Ok(basket ?? new Entities.ShoppingCart(username));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
    {
        return Ok(await _basketRepository.UpdateBasket(basket));
    }

    [HttpDelete("{username}", Name = "DeleteBasket")]
    public async Task<IActionResult> DeleteBasket(string username)
    {
        await _basketRepository.DeleteBasket(username);
        return Ok();
    }
}