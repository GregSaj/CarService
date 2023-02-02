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

            result.Left = this.Amount - ReturnSumFromFile();
                        
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

                            if (ReturnSumFromFile() + 10 < this.Amount)
                            {
                                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                                using (var writer2 = File.AppendText($"audit.txt"))
                                {
                                    writer.WriteLine(10);
                                    writer2.WriteLine($"{Id} {Type,-15} - {10,3}        {DateTime.UtcNow,-20}");
                                    if (SellsAdded != null)
                                    {
                                        SellsAdded(this, new EventArgs());
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "A+":

                            if (ReturnSumFromFile() + 15 < this.Amount)
                            {
                                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                                using (var writer2 = File.AppendText($"audit.txt"))
                                {
                                    writer.WriteLine(15);
                                    writer2.WriteLine($"{Id} {Type,-15} - {15,3}        {DateTime.UtcNow,-20}");
                                    if (SellsAdded != null)
                                    {
                                        SellsAdded(this, new EventArgs());
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "B":

                            if (ReturnSumFromFile() + 20 < this.Amount)
                            {
                                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                                using (var writer2 = File.AppendText($"audit.txt"))
                                {
                                    writer.WriteLine(20);
                                    writer2.WriteLine($"{Id} {Type,-15} - {20,3}        {DateTime.UtcNow,-20}");
                                    if (SellsAdded != null)
                                    {
                                        SellsAdded(this, new EventArgs());
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("You cannot sell more than amount.");
                            }
                            break;

                        case "B+":

                            if (ReturnSumFromFile() + 25 < this.Amount)
                            {
                                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                                using (var writer2 = File.AppendText($"audit.txt"))
                                {
                                    writer.WriteLine(25);
                                    writer2.WriteLine($"{Id} {Type,-15} - {25,3}        {DateTime.UtcNow,-20}");
                                    if (SellsAdded != null)
                                    {
                                        SellsAdded(this, new EventArgs());
                                    }
                                }
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
                    if (LowSellsAdded != null && stock < 10 && ReturnSumFromFile() + stock < this.Amount)
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
                        }
                    }
                    else if (stock > this.Amount)
                    {
                        Console.WriteLine($"You cannot sell at once more then {this.Amount}!");
                    }
                    else if (ReturnSumFromFile() + stock > this.Amount)
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

