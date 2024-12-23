﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_253501_Homets.Domain.Models
{
    public class ListModel<T>
    {
        public List<T> Items { get; set; } = new();

        public int CurrentPage { get; set; } = 1;
    
        public int TotalPages { get; set; } = 1;

        public int PageSize { get; set; } = 3;
        
        public int TotalCount { get; set; }
    }

}
