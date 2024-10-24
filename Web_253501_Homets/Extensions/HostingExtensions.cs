using Web_253501_Homets.Services.CategoryService;

namespace Web_253501_Homets.Extensions
{
    public static class HostingExtensions
    {
        public static void RegisterCustomServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, MemoryCategoryService>();
        }
    }
}
