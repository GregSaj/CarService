using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarService
{
    public delegate void SellsAddedDelegate(object sender, EventArgs args);

    public class SavedProduct : ProductBase
    {
        public const string fileName = "sales.txt";
        public string fullFileName;

        public bool fileCalculator = false;

        public SavedProduct(int id, string product, string type, double price, int amount) : base(id, product, type, price, amount)
        {
            fullFileName = $"{Id}_{Type}_{fileName}";
        }

        public override event SellsAddedDelegate SellsAdded;
        public override event SellsAddedDelegate LowSellsAdded;
        public override Statistics GetStatistics()
        {
            var result = new Statistics();

            using (var reader = File.OpenText($"{Id}_{Type}_{fileName}"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.AddSells(number);
                    line = reader.ReadLine();
                }

            }

            result.Left = this.Amount;

            return result;
        }

        public double ReturnSumFromFile()
        {
            double sumFromFile = 0;
            using (var reader = File.OpenText($"{Id}_{Type}_{fileName}"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    sumFromFile += number;
                    line = reader.ReadLine();
                }

                return SellsSum = sumFromFile;
            }
        }

        public override void AddSells(string sells)
        {

            if (!File.Exists($"{Id}_{Type}_{fileName}"))
            {
                var writer = File.AppendText($"{Id}_{Type}_{fileName}");
                writer.Dispose();
            }
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

                        using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                        using (var writer2 = File.AppendText($"audit.txt"))
                        {
                            writer.WriteLine(stock);
                            writer2.WriteLine($"{Id} {Type,-15} - {stock,3}        {DateTime.UtcNow,-20}");
                            if (SellsAdded != null)
                            {
                                SellsAdded(this, new EventArgs());
                            }
                            this.Amount = this.Amount - stock;
                            fileCalculator = true;
                        }
                    }                    
                    else if (stock > this.Amount)
                    {
                        Console.WriteLine($"You cannot sell more then {this.Amount}!");
                    }
                    else
                    {
                        using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                        using (var writer2 = File.AppendText($"audit.txt"))
                        {
                            writer.WriteLine(stock);
                            writer2.WriteLine($"{Id} {Type,-15} - {stock,3}        {DateTime.UtcNow,-20}");
                            if (SellsAdded != null)
                            {
                                SellsAdded(this, new EventArgs());
                            }
                            this.Amount = this.Amount - stock;
                            fileCalculator = true;
                        }
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
            if (A <= this.Amount)
            {
                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                using (var writer2 = File.AppendText($"audit.txt"))
                {
                    writer.WriteLine(A);
                    writer2.WriteLine($"{Id} {Type,-15} - {10,3}        {DateTime.UtcNow,-20}");
                    if (SellsAdded != null)
                    {
                        SellsAdded(this, new EventArgs());
                    }
                    Console.WriteLine($"Sell {A} was added to product.");
                    this.Amount = this.Amount - A;
                    fileCalculator = true;
                }
            }
            else
            {
                Console.WriteLine("You cannot sell more than amount.");
            }
        }
    }
}

