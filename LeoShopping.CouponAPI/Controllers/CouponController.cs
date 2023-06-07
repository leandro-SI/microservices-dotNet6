using LeoShopping.CouponAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoShopping.CouponAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {

        private readonly ICouponReposiroty _couponReposiroty;

        public CouponController(ICouponReposiroty couponReposiroty)
        {
            _couponReposiroty = couponReposiroty;
        }

        [Authorize]
        [HttpGet("{couponCode}")]
        public async Task<IActionResult> GetCouponByCouponCode(string couponCode)
        {
            var coupon = await _couponReposiroty.GetCouponByCouponCode(couponCode);

            if (coupon == null) return NotFound();

            return Ok(coupon);

        }

    }
}
