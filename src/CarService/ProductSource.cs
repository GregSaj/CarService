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
       

        public ProductSource(int id, string product, string type, double price, int amount)
        {
            this.Id = id;
            this.ProductName = product;
            this.Type = type;
            this.Price = price;
            this.Amount = amount;
        }
        public void ShowProduct()
        {
            if (ProductName == "Oil")
            {
                Console.WriteLine($"Id: {Id,-3} Product: {ProductName,-13} Type: {Type,-20} Price: {Price,6:F2} [zł netto/l.]     Amount: {Amount,7:F2}");
            }
            else
            {
                Console.WriteLine($"Id: {Id,-3} Product: {ProductName,-13} Type: {Type,-20} Price: {Price,6:F2} [zł netto/szt.]   Amount: {Amount,7:F2} ");
            }
        }
    }
}
