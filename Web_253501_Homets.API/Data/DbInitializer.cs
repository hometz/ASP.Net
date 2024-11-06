using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web_253501_Homets.Domain.Entities;

namespace Web_253501_Homets.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await context.Database.MigrateAsync();

            if (!context.Categories.Any())
            {
                var _categories = new List<Category>
                {
                    new Category { Id = 1, Name = "Закуски", NormalizedName = "appetizers" },
                    new Category { Id = 2, Name = "Пицца", NormalizedName = "pizza" },
                    new Category { Id = 3, Name = "Паста", NormalizedName = "pasta" },
                    new Category { Id = 4, Name = "Гарниры", NormalizedName = "side-dishes" },
                    new Category { Id = 5, Name = "Морепродукты", NormalizedName = "seafood" },
                    new Category { Id = 6, Name = "Вегетарианские блюда", NormalizedName = "vegetarian-dishes" },
                    new Category { Id = 7, Name = "Гриль", NormalizedName = "grill" },
                    new Category { Id = 8, Name = "Соусы", NormalizedName = "sauces" },
                };

                context.Categories.AddRange(_categories);
                await context.SaveChangesAsync();
            }

            if (!context.Dishes.Any())
            {
                var categories = await context.Categories.ToListAsync();

                var dishes = new List<Dish>
                {
                    new Dish { Id = 1, Name = "Брускетта с томатами", Description = "Хрустящий хлеб с сочными томатами и базиликом", Calories = 150, Image = "Images/Tomato.jpg", CategoryId = categories.First(c => c.NormalizedName == "appetizers").Id },
                    new Dish { Id = 2, Name = "Маргарита", Description = "Классическая пицца с томатным соусом и моцареллой", Calories = 300, Image = "Images/Margarita.png", CategoryId = categories.First(c => c.NormalizedName == "pizza").Id },
                    new Dish { Id = 3, Name = "Спагетти Карбонара", Description = "Паста с беконом, яйцом и сыром пармезан", Calories = 400, Image = "Images/Karbonara.jpg", CategoryId = categories.First(c => c.NormalizedName == "pasta").Id },
                    new Dish { Id = 4, Name = "Картофельное пюре", Description = "Нежное картофельное пюре с маслом", Calories = 200, Image = "Images/Pure.jpg", CategoryId = categories.First(c => c.NormalizedName == "side-dishes").Id },
                    new Dish { Id = 5, Name = "Креветки на гриле", Description = "Сочные креветки, приготовленные на гриле", Calories = 250, Image = "Images/Krevetki.jpg", CategoryId = categories.First(c => c.NormalizedName == "seafood").Id },
                    new Dish { Id = 6, Name = "Салат Цезарь", Description = "Салат с романо, пармезаном и соусом Цезарь", Calories = 180, Image = "Images/Cezar.jpeg", CategoryId = categories.First(c => c.NormalizedName == "vegetarian-dishes").Id },
                    new Dish { Id = 7, Name = "Стейк на гриле", Description = "Сочный стейк, приготовленный на гриле", Calories = 500, Image = "Images/Steik.jpg", CategoryId = categories.First(c => c.NormalizedName == "grill").Id },
                    new Dish { Id = 8, Name = "Соус Песто", Description = "Ароматный соус из базилика, чеснока и оливкового масла", Calories = 100, Image = "Images/Pesto.jpg", CategoryId = categories.First(c => c.NormalizedName == "sauces").Id },
                };

                await context.Dishes.AddRangeAsync(dishes);
                await context.SaveChangesAsync();
            }
        }
    }
}
