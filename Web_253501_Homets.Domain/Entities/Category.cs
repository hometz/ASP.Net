using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_253501_Homets.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NormalizedName { get; set; }

        public ICollection<Dish> Dishes { get; set; } = new List<Dish>();
    }
}
