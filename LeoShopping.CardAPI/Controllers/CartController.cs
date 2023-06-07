using LeoShopping.CartAPI.Messages;
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
        public async Task<ActionResult<CartDTO>> FindById(string id)
        {
            var cart = await _repository.FindCartbyUserId(id);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _repository.SaveOrUpdateCart(cartDTO);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
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

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var status = await _repository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);

            if (!status) return NotFound();

            return Ok(status);
        }        
        
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
        {
            var status = await _repository.RemoveCoupon(userId);

            if (!status) return NotFound();

            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO dto)
        {
            var cart = await _repository.FindCartbyUserId(dto.UserId);

            if (cart == null) return NotFound();

            dto.CartDetails = cart.CartDetails;
            dto.Time = DateTime.Now;

            // TASK RabbitMQ logic comes here!!!

            return Ok(dto);
        }


    }
}
