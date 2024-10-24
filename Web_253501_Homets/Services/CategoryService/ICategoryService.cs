using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;

namespace Web_253501_Homets.Services.CategoryService
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
