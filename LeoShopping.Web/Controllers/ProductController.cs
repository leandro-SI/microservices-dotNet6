using LeoShopping.Web.Models;
using LeoShopping.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace LeoShopping.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> ProductIndex()
        {

            var products = await _productService.FindAllProducts();

            return View(products);
        }

        public IActionResult ProductCreate()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductModel model)
        {

            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProduct(model);

                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            
            return View(model);
        }
    }
}
