﻿namespace ProductWebAPI.Models
{
    public class Product
    {
        public string Id { get; set; } = default!;
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
