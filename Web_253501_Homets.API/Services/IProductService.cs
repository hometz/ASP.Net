using Web_253501_Homets.Domain.Entities;
using Web_253501_Homets.Domain.Models;

namespace Web_253501_Homets.API.Services
{
    public interface IProductService
    {
        Task<ResponseData<ListModel<Dish>>> GetProductListAsync(string? categoryNormalizedName, int pageNo = 1);
        Task<ResponseData<Dish>> GetProductByIdAsync(int id);
        //Task UpdateProductAsync(int id, Dish product, IFormFile? formFile);
        //Task DeleteProductAsync(int id);
        //Task<ResponseData<Dish>> CreateProductAsync(Dish product, IFormFile? formFile);
    }
}
