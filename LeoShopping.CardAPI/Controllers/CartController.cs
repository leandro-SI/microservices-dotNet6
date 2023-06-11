using LeoShopping.CartAPI.Data.Dtos;
using LeoShopping.CartAPI.Messages;
using LeoShopping.CartAPI.Model;
using LeoShopping.CartAPI.RabbitMQSender;
using LeoShopping.CartAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoShopping.CartAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICouponReposiroty _couponRepository;
        private readonly IRabbitMQMessageSenser _rabbitMQMessageSenser;

        public CartController(ICartRepository repository, IRabbitMQMessageSenser rabbitMQMessageSenser, ICouponReposiroty couponRepository)
        {
            _cartRepository = repository ?? throw new ArgumentNullException(nameof(repository));
            _rabbitMQMessageSenser = rabbitMQMessageSenser;
            _couponRepository = couponRepository;
        }


        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartDTO>> FindById(string id)
        {
            var cart = await _cartRepository.FindCartbyUserId(id);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartDTO>> AddCart(CartDTO cartDTO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDTO);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartDTO>> UpdateCart(CartDTO cartDTO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartDTO);

            if (cart == null) return NotFound();

            return Ok(cart);
        }

        [HttpDelete("remove-cart/{id}")]
        public async Task<ActionResult<CartDTO>> RemoveCart(int id)
        {
            var response = await _cartRepository.RemoveFromCart(id);

            if (!response) return BadRequest();

            return Ok(response);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartDTO>> ApplyCoupon(CartDTO cartDTO)
        {
            var status = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);

            if (!status) return NotFound();

            return Ok(status);
        }        
        
        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartDTO>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);

            if (!status) return NotFound();

            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderDTO>> Checkout(CheckoutHeaderDTO dto)
        {
            //string token = Request.Headers["Authorization"];
            var token = await HttpContext.GetTokenAsync("access_token");

            if (dto?.UserId == null) return BadRequest();

            var cart = await _cartRepository.FindCartbyUserId(dto.UserId);

            if (cart == null) return NotFound();

            if (!string.IsNullOrEmpty(dto.CouponCode))
            {
                CouponDTO coupon = await _couponRepository.GetCouponByCouponCode(dto.CouponCode, token);

                if (dto.DiscountTotal != coupon.DiscountAmount)
                {
                    return StatusCode(412);
                }
            }

            dto.CartDetails = cart.CartDetails;
            dto.Time = DateTime.Now;

            // TASK RabbitMQ logic comes here!!!

            _rabbitMQMessageSenser.SendMessage(dto, "checkoutqueue");

            return Ok(dto);
        }


    }
}
