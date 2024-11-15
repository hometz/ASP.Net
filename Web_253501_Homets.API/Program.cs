using Web_253501_Homets.API.Data;
using Microsoft.EntityFrameworkCore;
using Web_253501_Homets.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Добавьте DbContext с конфигурацией строки подключения
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

// Добавляем другие сервисы
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Инициализация базы данных
await DbInitializer.SeedData(app);


// Конфигурация приложения
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
