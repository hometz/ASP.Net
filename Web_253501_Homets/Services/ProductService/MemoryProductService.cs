using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;
using Web_253501_Homets.Services.CategoryService;

namespace Web_253501_Homets.Services.ProductService
{
    public class MemoryProductService : IProductService
    {
        private readonly int _pageSize;
        List<Dish> _dishes;
        List<Category> _categories;


        public MemoryProductService([FromServices] IConfiguration config,
                                    ICategoryService categoryService)
        {
            _pageSize = config.GetValue<int>("ItemsPerPage");
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }

        private void SetupData()
        {
            _dishes = new List<Dish>
            {
                new Dish {Id = 1, Name="Брускетта с томатами", Description="Хрустящий хлеб с сочными томатами и базиликом", Calories = 150, Image = "images/Tomato.jpg", Category = _categories.Find(c => c.NormalizedName == "appetizers")},
                new Dish {Id = 2, Name="Маргарита", Description="Классическая пицца с томатным соусом и моцареллой", Calories = 300, Image = "images/Margarita.png", Category = _categories.Find(c => c.NormalizedName == "pizza")},
                new Dish {Id = 3, Name="Спагетти Карбонара", Description="Паста с беконом, яйцом и сыром пармезан", Calories = 400, Image = "images/Karbonara.jpg", Category = _categories.Find(c => c.NormalizedName == "pasta")},
                new Dish {Id = 4, Name="Картофельное пюре", Description="Нежное картофельное пюре с маслом", Calories = 200, Image = "images/Pure.jpg", Category = _categories.Find(c => c.NormalizedName == "side-dishes")},
                new Dish {Id = 5, Name="Креветки на гриле", Description="Сочные креветки, приготовленные на гриле", Calories = 250, Image = "images/Krevetki.jpg", Category = _categories.Find(c => c.NormalizedName == "seafood")},
                new Dish {Id = 6, Name="Салат Цезарь", Description="Салат с романо, пармезаном и соусом Цезарь", Calories = 180, Image = "images/Cezar.jpeg", Category = _categories.Find(c => c.NormalizedName == "vegetarian-dishes")},
                new Dish {Id = 7, Name="Стейк на гриле", Description="Сочный стейк, приготовленный на гриле", Calories = 500, Image = "images/Steik.jpg", Category = _categories.Find(c => c.NormalizedName == "grill")},
                new Dish {Id = 8, Name="Соус Песто", Description="Ароматный соус из базилика, чеснока и оливкового масла", Calories = 100, Image = "images/Pesto.jpg", Category = _categories.Find(c => c.NormalizedName == "sauces")},

            };
        }


        public Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {

            var filteredFruits = string.IsNullOrEmpty(categoryNormalizedName)
            ? _dishes
            : _dishes.Where(f => f.Category.NormalizedName.Equals(categoryNormalizedName, StringComparison.OrdinalIgnoreCase)).ToList();


            var pagedFruits = filteredFruits
                .Skip((pageNo - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            var totalItems = filteredFruits.Count;
            var listModel = new ListModel<Dish>
            {
                Items = pagedFruits,
                TotalCount = totalItems,
                CurrentPage = pageNo,
                PageSize = _pageSize,
                TotalPages = (int)Math.Ceiling((double)totalItems / _pageSize)
            };

            return Task.FromResult(new ResponseData<ListModel<Dish>> { Data = listModel, Successfull = true });
        }


        public Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            var product = _dishes.FirstOrDefault(d => d.Id == id);
            if (product == null)
            {
                return Task.FromResult(ResponseData<Dish>.Error("Продукт не найден"));
            }

            return Task.FromResult(ResponseData<Dish>.Success(product));
        }


        public Task UpdateProductAsync(int id, Dish product, IFormFile? formFile)
        {
            var existingProduct = _dishes.FirstOrDefault(d => d.Id == id);
            if (existingProduct == null)
            {
                throw new Exception("Продукт не найден");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Calories = product.Calories;
            existingProduct.Category = product.Category;

            if (formFile != null)
            {
                existingProduct.Image = $"Images/{formFile.FileName}";
            }

            return Task.CompletedTask;
        }

        public Task DeleteProductAsync(int id)
        {
            var product = _dishes.FirstOrDefault(d => d.Id == id);
            if (product != null)
            {
                _dishes.Remove(product);
            }

            return Task.CompletedTask;
        }

        public Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile)
        {
            product.Id = _dishes.Max(d => d.Id) + 1;

            if (formFile != null)
            {
                product.Image = $"Images/{formFile.FileName}";
            }

            _dishes.Add(product);

            return Task.FromResult(ResponseData<Dish>.Success(product));
        }
    }
}
