﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_253501_Homets.API.Data;
using Web_253501_Homets.Domain.Entities;

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
        public async Task<ActionResult<IEnumerable<Dish>>> GetDishes()
        {
            return await _context.Dishes.ToListAsync();
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
}
