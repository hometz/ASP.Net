using Microsoft.AspNetCore.Mvc;
using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Services.CategoryService;

namespace Web_253501_Homets.Services.ProductService
{
    public class MemoryProductService
    {
        List<Dish> _dishes;
        List<Category> _categories;

        public MemoryProductService(ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            SetupData();
        }

        private void SetupData()
        {
            _dishes = new List<Dish>
            {
                new Dish {Id = 2, Name="Брускетта с томатами", Description="Хрустящий хлеб с сочными томатами и базиликом", Calories = 150, Image = "#", Category = _categories.Find(c => c.NormalizedName == "appetizers")},
                new Dish {Id = 3, Name="Маргарита", Description="Классическая пицца с томатным соусом и моцареллой", Calories = 300, Image = "#", Category = _categories.Find(c => c.NormalizedName == "pizza")},
                new Dish {Id = 4, Name="Спагетти Карбонара", Description="Паста с беконом, яйцом и сыром пармезан", Calories = 400, Image = "#", Category = _categories.Find(c => c.NormalizedName == "pasta")},
                new Dish {Id = 5, Name="Картофельное пюре", Description="Нежное картофельное пюре с маслом", Calories = 200, Image = "#", Category = _categories.Find(c => c.NormalizedName == "side-dishes")},
                new Dish {Id = 6, Name="Креветки на гриле", Description="Сочные креветки, приготовленные на гриле", Calories = 250, Image = "#", Category = _categories.Find(c => c.NormalizedName == "seafood")},
                new Dish {Id = 7, Name="Салат Цезарь", Description="Салат с романо, пармезаном и соусом Цезарь", Calories = 180, Image = "#", Category = _categories.Find(c => c.NormalizedName == "vegetarian-dishes")},
                new Dish {Id = 8, Name="Стейк на гриле", Description="Сочный стейк, приготовленный на гриле", Calories = 500, Image = "#", Category = _categories.Find(c => c.NormalizedName == "grill")},
                new Dish {Id = 9, Name="Соус Песто", Description="Ароматный соус из базилика, чеснока и оливкового масла", Calories = 100, Image = "#", Category = _categories.Find(c => c.NormalizedName == "sauces")},

            };
        }
    }
}
