using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_253501_Homets.API.Data;
using Web_253501_Homets.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace Web_253501_Homets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dishes1Controller : ControllerBase
    {
        private readonly AppDbContext _context;

        public Dishes1Controller(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Dishes1
        [HttpGet]
        public async Task<ActionResult<PagedResponse<Dish>>> GetDishes(string? category = null, int page = 1, int pageSize = 10)
        {
            // Стартуем запрос с получения всех блюд
            IQueryable<Dish> query = _context.Dishes.Include(d => d.Category);

            // Фильтрация по категории
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(d => d.Category.NormalizedName == category);
            }

            // Получение общего количества блюд
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Получение списка блюд для текущей страницы
            var dishes = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Если нет данных, возвращаем ошибку
            if (!dishes.Any())
            {
                return NotFound(new { Message = "Нет доступных блюд для указанной категории." });
            }

            // Создаем объект с данными и метаинформацией о пагинации
            var pagedResponse = new PagedResponse<Dish>
            {
                Items = dishes,
                TotalPages = totalPages,
                CurrentPage = page,
                TotalItems = totalItems
            };

            // Возвращаем успешный результат с данными
            return Ok(pagedResponse);
        }

        // GET: api/Dishes1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dish>> GetDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);

            if (dish == null)
            {
                return NotFound();
            }

            return dish;
        }

        // PUT: api/Dishes1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDish(int id, Dish dish)
        {
            if (id != dish.Id)
            {
                return BadRequest();
            }

            _context.Entry(dish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dishes1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dish>> PostDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDish", new { id = dish.Id }, dish);
        }

        // DELETE: api/Dishes1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDish(int id)
        {
            var dish = await _context.Dishes.FindAsync(id);
            if (dish == null)
            {
                return NotFound();
            }

            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }
    }

    // Класс для ответа с пагинацией
    public class PagedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
    }
}
