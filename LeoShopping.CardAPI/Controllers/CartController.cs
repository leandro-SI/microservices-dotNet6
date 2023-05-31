using LeoShopping.CartAPI.Model;
using LeoShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }


        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDTO>> FindById(string userId)
        {
            var cart = await _repository.FindCartbyUserId(userId);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart/{id}")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart/{id}")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCart(int id)
        {
            var response = await _repository.RemoveFromCart(id);

            if (!response) return BadRequest();

            return Ok(response);
        }


    }
}
