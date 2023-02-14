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
       
        public override void AddSells(string sells)
        {
            try
            {
                if (sells == "A" || sells == "A+" || sells == "B" || sells == "B+")
                {
                    switch (sells)
                    {
                        case "A":
                            InnerMethodAddSells(10);
                            break;

                        case "A+":

                            InnerMethodAddSells(15);
                            break;

                        case "B":

                            InnerMethodAddSells(20);
                            break;

                        case "B+":

                            InnerMethodAddSells(25);
                            break;

                        default:
                            Console.WriteLine("Method assume only from A, A+, B or B+.");
                            break;
                    }
                }
                else if (double.TryParse(sells, out double stock))
                {
                    if (stock < 0)
                    {
                        Console.WriteLine("Sells cannot be less then 0.");
                        return;
                    }
                    if (LowSellsAdded != null && stock < 10 && stock < this.Amount)
                    {
                        LowSellsAdded(this, new EventArgs());
                        listaSell.Add(stock);
                        this.Amount = this.Amount - stock;
                        Console.WriteLine($"Sell {sells} was added to product.");
                    }
                    else if (stock > this.Amount)
                    {
                        throw new ArgumentException($"You cannot sell at once more then {this.Amount}!");
                    }                   
                    else
                    {
                        listaSell.Add(stock);
                        this.Amount = this.Amount - stock;
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

        private void InnerMethodAddSells(int A)
        {            
            if (A < this.Amount)
            {
                listaSell.Add(A);
                this.Amount = this.Amount - A;
                Console.WriteLine($"Sell {A} was added to product.");
            }
            else
            {
                Console.WriteLine("You cannot sell more than amount.");
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            for (int i = 0; i < listaSell.Count; i++)
            {
                result.AddSells(listaSell[i]);
            }

            result.Left = this.Amount;

            return result;
        }
    }
}





