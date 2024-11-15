using Web_253501_Homets.API.Data;
using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Web_253501_Homets.API.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return ResponseData<List<Category>>.Success(categories);
        }
    }
}
