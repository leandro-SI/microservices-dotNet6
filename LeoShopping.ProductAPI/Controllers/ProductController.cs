using LeoShopping.ProductAPI.Data.Dtos;
using LeoShopping.ProductAPI.Model;
using LeoShopping.ProductAPI.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeoShopping.ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;


        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository ?? throw new ArgumentNullException(nameof(productRepository)) ;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> FindAll()
        {
            var products = await _repository.FindAll();

            return Ok(products);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> FindById(long id)
        {
            var product = await _repository.FindById(id);

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDTO)
        {

            if (productDTO == null) return BadRequest();

            var product = await _repository.Create(productDTO);

            return product;
        }

        [HttpPut]
        public async Task<ActionResult<ProductDTO>> Update(ProductDTO productDTO)
        {
            if (productDTO == null) return BadRequest();

            var product = await _repository.Update(productDTO);

            return product;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(long id)
        {
            var status = await _repository.DeleteById(id);

            if (!status) return BadRequest();

            return Ok(status);
        }



    }
}
