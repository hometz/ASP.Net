using Microsoft.AspNetCore.Mvc;

using Web_253501_Homets.API.Services;

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
            // Получение списка категорий из базы данных
            var responseCategories = await _categoryService.GetCategoryListAsync();
            var categories = responseCategories.Data;

            // Получаем название текущей категории для отображения
            var currentCategory = categories?.FirstOrDefault(c => c.NormalizedName == category)?.Name ?? "Все";
            ViewData["categories"] = categories;
            ViewData["currentCategory"] = currentCategory;
            ViewData["currentCategoryNormalizedName"] = category;

            // Получаем список товаров для выбранной категории с пагинацией
            var responseProducts = await _productService.GetProductListAsync(category, pageNo);
            var model = responseProducts.Data;

            return View(model);
        }
    }
}
