using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;

namespace Web_253501_Homets.Services.CategoryService
{
    public class MemoryCategoryService: ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
            {
                new Category {Id=1, Name="Закуски", NormalizedName="appetizers"},
                new Category {Id=2, Name="Пицца", NormalizedName="pizza"},
                new Category {Id=3, Name="Паста", NormalizedName="pasta"},
                new Category {Id=4, Name="Гарниры", NormalizedName="side-dishes"},
                new Category {Id=5, Name="Морепродукты", NormalizedName="seafood"},
                new Category {Id=6, Name="Вегетарианские блюда", NormalizedName="vegetarian-dishes"},
                new Category {Id=7, Name="Гриль", NormalizedName="grill"},
                new Category {Id=8, Name="Соусы", NormalizedName="sauces"},
            };
            var result = ResponseData<List<Category>>.Success(categories);

            return Task.FromResult(result);
        }
    }
}
