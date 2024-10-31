using Web_253501_Homets.Services.CategoryService;
using Web_253501_Homets.Services.ProductService;

namespace Web_253501_Homets.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
            builder.Services.AddScoped<IProductService, MemoryProductService>();
        }
    }
}
