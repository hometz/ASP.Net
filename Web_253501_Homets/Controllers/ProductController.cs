using Microsoft.AspNetCore.Mvc;
using Web_253501_Homets.Services.CategoryService;
using Web_253501_Homets.Services.ProductService;

namespace Web_253501_Homets.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(string? category = null, int pageNo = 1)
        {
            var response = await _categoryService.GetCategoryListAsync();
            var categories = response.Data;

            var currentCategory = categories?.FirstOrDefault(c => c.NormalizedName == category)?.Name ?? "Все";
            ViewData["categories"] = categories;
            ViewData["currentCategory"] = currentCategory;
            ViewData["currentCategoryNormalizedName"] = category;

            var products = await _productService.GetProductListAsync(category, pageNo);
            var model = products.Data;

            return View(model);
        }
    }
}
