using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarService
{
    public interface IProduct
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Type { get; set; }
        public double Price { get; set; }
        public double Amount { get; set; }
        void AddSells(string sells);
        Statistics GetStatistics();
        public event SellsAddedDelegate SellsAdded;
        public event SellsAddedDelegate LowSellsAdded;
    }
}