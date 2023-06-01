using LeoShopping.Web.Models;
using LeoShopping.Web.Services.IServices;
using LeoShopping.Web.Utils;
using System.Net.Http.Headers;

namespace LeoShopping.Web.Services
{
    public class ProductService : IProductService
    {

        private readonly HttpClient _client;
        public const string BasePath = "/api/v1/product";

        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<ProductViewModel>> FindAllProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync(BasePath);
            return await response.ReadContentAs<List<ProductViewModel>>();
        }

        public async Task<ProductViewModel> FindProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"{BasePath}/{id}");
            return await response.ReadContentAs<ProductViewModel>();
        }

        public async Task<ProductViewModel> CreateProduct(ProductViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PostAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductViewModel>();
            }

            throw new Exception("Algo deu errado ao chamar a API");
            
        }

        public async Task<ProductViewModel> UpdateProduct(ProductViewModel model, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.PutAsJson(BasePath, model);

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<ProductViewModel>();
            }

            throw new Exception("Algo deu errado ao chamar a API");
        }

        public async Task<bool> DeleteProductById(long id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.DeleteAsync($"{BasePath}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return await response.ReadContentAs<bool>();
            }

            throw new Exception($"Could not delete {id}");
            
        }

    }
}
