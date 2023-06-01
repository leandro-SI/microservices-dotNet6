﻿using LeoShopping.Web.Models;
using LeoShopping.Web.Services.IServices;
using LeoShopping.Web.Utils;
using System.Net.Http.Headers;
using System.Reflection;

namespace LeoShopping.Web.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client;
        public const string BasePath = "/api/v1/cart";

        public CartService(HttpClient client)
        {
            _client = client;
        }

        public async Task<CartViewModel> AddItemToCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJson($"{BasePath}/add-cart", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartViewModel>();
            }

            throw new Exception("Algo deu errado ao chamar a API");
        }

        public Task<bool> ApplyCoupon(CartViewModel cart, string couponCode, string token)
        {
            throw new NotImplementedException();
        }

        public Task<CartViewModel> Checkout(CartHeaderViewModel cartHeader, string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ClearCart(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<CartViewModel> FindCartByUserId(string userId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/find-cart/{userId}");
            return await response.ReadContentAs<CartViewModel>();
        }

        public Task<bool> RemoveCoupon(string userId, string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveFromCart(long cartId, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{BasePath}/remove-cart/{cartId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }

            throw new Exception($"Could not delete {cartId}");
        }

        public async Task<CartViewModel> UpdateCart(CartViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJson($"{BasePath}/update-cart", model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<CartViewModel>();
            }

            throw new Exception("Algo deu errado ao chamar a API");
        }
    }
}
