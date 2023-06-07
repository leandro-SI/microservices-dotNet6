using LeoShopping.Web.Models;
using LeoShopping.Web.Services.IServices;
using LeoShopping.Web.Utils;
using System.Net;
using System.Net.Http.Headers;

namespace LeoShopping.Web.Services
{
    public class CouponService : ICouponService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/v1/coupon";

        public CouponService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponViewModel> GetCoupon(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{couponCode}");

            if (response.StatusCode != HttpStatusCode.OK)
            {
                return new CouponViewModel();
            }

            return await response.ReadContentAs<CouponViewModel>();
        }
    }
}
