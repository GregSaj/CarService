using System;
using System.Collections.Generic;
using System.Linq;

namespace CarService
{
    public class ProductSource
    {

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        public double SellsSum { get; set; }

        public ProductSource(int id, string product, string type, double price, int amount)
        {
            this.Id = id;
            this.ProductName = product;
            this.Type = type;
            this.Price = price;
            this.Amount = amount;
        }
    }
}
