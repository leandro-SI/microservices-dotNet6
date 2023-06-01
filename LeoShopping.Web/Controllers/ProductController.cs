using LeoShopping.Web.Models;
using LeoShopping.Web.Services.IServices;
using LeoShopping.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public async Task<IActionResult> ProductIndex()
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var products = await _productService.FindAllProducts(token);

            return View(products);
        }

        public IActionResult ProductCreate()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductCreate(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.CreateProduct(model, token);

                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            
            return View(model);
        }

        public async Task<IActionResult> ProductUpdate(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token   );

            if (product != null)
            {
                return View(product);
            }

            return NotFound();

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ProductUpdate(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                var token = await HttpContext.GetTokenAsync("access_token");

                var response = await _productService.UpdateProduct(model, token);

                if (response != null)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }

            }            

            return View(model);

        }

        [Authorize]
        public async Task<IActionResult> ProductDelete(long id)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var product = await _productService.FindProductById(id, token);

            if (product != null)
            {
                return View(product);
            }

            return NotFound();

        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> ProductDelete(ProductViewModel model)
        {
            var token = await HttpContext.GetTokenAsync("access_token");

            var response = await _productService.DeleteProductById(model.Id, token);

            if (response)
            {
                return RedirectToAction(nameof(ProductIndex));
            }

            return View(model);

        }


    }
}
