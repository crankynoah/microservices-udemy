using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
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
            List<ProductDto> list = new();
            var resoponse = await _productService.GetAllProcductsAsync<ResponseDto>();
            if (resoponse != null && resoponse.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(resoponse.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var resoponse = await _productService.CreateProductAsync<ResponseDto>(model);
                if (resoponse != null && resoponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ProductEdit(int productId)
        {
            var resoponse = await _productService.GetProcductByIdAsync<ResponseDto>(productId);
            if (resoponse != null && resoponse.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resoponse.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var resoponse = await _productService.UpdateProductAsync<ResponseDto>(model);
                if (resoponse != null && resoponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> ProductDelete(int productId)
        {
            var resoponse = await _productService.GetProcductByIdAsync<ResponseDto>(productId);
            if (resoponse != null && resoponse.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(resoponse.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                var resoponse = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId);
                if (resoponse != null && resoponse.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}
