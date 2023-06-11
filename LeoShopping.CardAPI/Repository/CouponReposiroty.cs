using AutoMapper;
using LeoShopping.CartAPI.Repository.Interfaces;
using LeoShopping.CartAPI.Data.Dtos;
using LeoShopping.CartAPI.Model.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;

namespace LeoShopping.CartAPI.Repository
{
    public class CouponReposiroty : ICouponReposiroty
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/v1/coupon";

        public CouponReposiroty(HttpClient client)
        {
            _client = client;
        }

        public async Task<CouponDTO> GetCouponByCouponCode(string couponCode, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{couponCode}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode != HttpStatusCode.OK) return new CouponDTO();

            return JsonSerializer.Deserialize<CouponDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
        }
    }
}
