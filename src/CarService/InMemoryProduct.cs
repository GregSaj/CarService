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

        // public override void AddSells(double sells)
        // {
        //     if (sells > 0 && sells < this.Amount)
        //     {
        //         this.listaSell.Add(sells);
        //         if (SellsAdded != null)
        //         {
        //             SellsAdded(this, new EventArgs());
        //         }
        //         if (LowSellsAdded != null && sells <= 10)
        //         {
        //             LowSellsAdded(this, new EventArgs());
        //         }
        //         this.SellsSum = listaSell.Sum();
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Invalid argument: {nameof(sells)}");
        //     }
        //}


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
        // public void AddSell(string sells)
        // {
        //     if (double.TryParse(sells, out double stock))
        //     {
        //         listaSell.Add(stock);
        //         Console.WriteLine($"Sell {sells} was added to product.");
        //     }
        //     else
        //     {
        //         Console.WriteLine($"{sells} is wrong format");
        //     }
        // }

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
                        // throw new ArgumentException($"Invalid argument: {nameof(sells)}. Only grades from 1 to 6 are allowed!"); comment: to nie zadziała, dlaczego?
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
        // public void AddSellsByTensPlusFive(string sells)
        // {
        //     var sellswithplus = sells switch
        //     {
        //         "10+" => 15,
        //         "20+" => 25,
        //         "30+" => 35,
        //         "40+" => 45,
        //         "50+" => 55,
        //         "60+" => 65,
        //         _ => double.Parse(sells)
        //     };
        //     if (sellswithplus > 0 && sellswithplus < this.Amount)
        //     {
        //         this.listaSell.Add(sellswithplus);
        //         this.SellsSum = listaSell.Sum();
        //     }
        //     else
        //     {
        //         throw new ArgumentException($"Invalid argument: {nameof(sells)}");
        //     }
        // }

        // public void AddSell(char sells)
        // {
        //     switch (sells)
        //     {
        //         case 'A':

        //             this.AddSell(100);
        //             this.SellsSum = listaSell.Sum();
        //             break;

        //         case 'B':

        //             this.AddSell(80);
        //             this.SellsSum = listaSell.Sum();
        //             break;

        //         case 'C':

        //             this.AddSell(60);
        //             this.SellsSum = listaSell.Sum();
        //             break;

        //         default:

        //             this.AddSell(0);
        //             this.SellsSum = listaSell.Sum();
        //             break;
        //     }
        // }


        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            result.SellsSum = 0;  
            result.Count = 0;          
            result.SellHigh = double.MinValue;
            result.SellLow = double.MaxValue;

            for (int i = 0; i < listaSell.Count; i++)
            {
                result.SellLow = Math.Min(result.SellLow, listaSell[i]);
                result.SellHigh = Math.Max(result.SellHigh, listaSell[i]);
                result.SellsSum = result.SellsSum + listaSell[i];
                result.Count++;
            }
            
            result.Left = this.Amount - result.SellsSum;
            this.SellsSum = result.SellsSum;
           
            return result;
        }
    }
}





