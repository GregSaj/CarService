using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarService
{

    public class InMemoryProduct : ProductBase
    {        
        public List<double> listaSell = new List<double>();
        public override event SellsAddedDelegate SellsAdded;
        public override event SellsAddedDelegate LowSellsAdded;

        public InMemoryProduct(int id, string product, string type, double price, int amount) : base(id, product, type, price, amount)
        {
            
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

        public void ChangeProductAndCheckIfDigit(string newproduct)
        {
            bool checkiftrue = true;

            foreach (var item in newproduct)
            {
                if (char.IsDigit(item))
                {
                    checkiftrue = false;
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.Write($"Can't change. {newproduct} has got digits.");
                    Console.ResetColor();
                    break;
                }
            }

            if (checkiftrue)
            {
                this.ProductName = newproduct;
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write($"Product has been changed to {newproduct}.");
                Console.ResetColor();
            }
        }
       
        public override void AddSells(string sells)
        {
            try            
            {
                if (sells == "A" || sells == "A+" || sells == "B" || sells == "B+")
                {
                    switch (sells)
                    {
                        case "A":
                            if (listaSell.Sum() + 10 < this.Amount)
                            {                                
                                listaSell.Add(10.00);
                                sells = "10";
                                Console.WriteLine($"Sell {sells} was added to product.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "A+":
                            if (listaSell.Sum() + 15 < this.Amount)
                            {

                                listaSell.Add(15.00);
                                sells = "15";
                                Console.WriteLine($"Sell {sells} was added to product.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "B":
                            if (listaSell.Sum() + 20 < this.Amount)
                            {

                                listaSell.Add(20.00);
                                sells = "20";
                                Console.WriteLine($"Sell {sells} was added to product.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "B+":
                            if (listaSell.Sum() + 25 < this.Amount)
                            {

                                listaSell.Add(25.00);
                                sells = "25";
                                Console.WriteLine($"Sell {sells} was added to product.");
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        default:
                            Console.WriteLine("Method assume only from A, A+, B or B+.");
                            break;
                    }
                }
                else if (double.TryParse(sells, out double stock))
                {
                    if (LowSellsAdded != null && stock < 10 && listaSell.Sum()+stock<this.Amount )
                    {
                        LowSellsAdded(this, new EventArgs());
                        listaSell.Add(stock);
                        Console.WriteLine($"Sell {sells} was added to product.");
                    }
                    else if (stock > this.Amount)
                    {
                        throw new ArgumentException($"You cannot sell at once more then {this.Amount}!");
                    }
                    else if (listaSell.Sum()+stock>this.Amount)
                    {
                        Console.WriteLine($"You cannot sell more then {this.Amount}!");                        
                    }
                    else
                    {
                        listaSell.Add(stock);
                        Console.WriteLine($"Sell {sells} was added to product.");
                    }
                }
                else
                {
                    Console.WriteLine($"{sells} is wrong format!");
                }
            }
            catch (FormatException ex)
            {                 
                Console.WriteLine(ex.Message);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);                
            }
        }
     
        public override Statistics GetStatistics()
        {
            var result = new Statistics();           

            for (int i = 0; i < listaSell.Count; i++)
            {
                result.AddSells(listaSell[i]);              
            }
            
            result.Left = this.Amount - result.SellsSum;
                       
            return result;
        }
    }
}





