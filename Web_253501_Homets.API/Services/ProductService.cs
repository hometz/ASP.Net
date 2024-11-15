using Web_253501_Homets.API.Data;
using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Web_253501_Homets.API.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public ProductService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var itemsPerPage = _configuration.GetValue<int>("ItemsPerPage");

            var query = _context.Dishes.Include(d => d.Category).AsQueryable();

            if (!string.IsNullOrEmpty(categoryNormalizedName))
            {
                query = query.Where(d => d.Category.NormalizedName == categoryNormalizedName);
            }

            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / itemsPerPage);

            var products = await query
                .Skip((pageNo - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            var listModel = new ListModel<Dish>
            {
                Items = products,
                CurrentPage = pageNo,
                TotalPages = totalPages
            };

            return ResponseData<ListModel<Dish>>.Success(listModel);
        }

        public async Task<ResponseData<Dish>> GetProductByIdAsync(int id)
        {
            var product = await _context.Dishes
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (product == null)
            {
                return ResponseData<Dish>.Error("Продукт не найден");
            }

            return ResponseData<Dish>.Success(product);
        }
    }
}
