using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CarService
{
    public delegate void SellsAddedDelegate(object sender, EventArgs args);


    public abstract class ProductBase : ProductSource, IProduct
    {
        public ProductBase(int id, string product, string type, double price, int amount) : base(id, product, type, price, amount)
        {

        }

        public abstract event SellsAddedDelegate SellsAdded;
        public abstract event SellsAddedDelegate LowSellsAdded;

        public abstract void AddSells(string sells);

        public abstract Statistics GetStatistics();

    }

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

        // public override void AddSells(string sells)
        // {
        //     using (var writer = File.AppendText($"{Id}_{Type}.txt"))
        //     {
        //         writer.WriteLine(sells);
        //         if (SellsAdded != null)
        //         {
        //             SellsAdded(this, new EventArgs());
        //         }
        //     }
        //     // writer.Dispose();
        // }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            result.Count = 0;
            result.SellsSum = 0;
            result.SellAverage = 0;
            result.SellHigh = double.MinValue;
            result.SellLow = double.MaxValue;

            using (var reader = File.OpenText($"{Id}_{Type}_{fileName}"))
            {
                var line = reader.ReadLine();
                while (line != null)
                {
                    var number = double.Parse(line);
                    result.Count++;
                    result.SellLow = Math.Min(result.SellLow, number);
                    result.SellHigh = Math.Max(result.SellHigh, number);
                    result.SellsSum = result.SellsSum + number;
                    line = reader.ReadLine();
                }

                result.SellAverage = result.SellsSum / result.Count;
                result.Left = this.Amount - result.SellsSum;
                this.SellsSum = result.SellsSum;


            }

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

                            if (ReturnSumFromFile() + 10 < this.Amount) //trzeba zrobic warunek if file exists wtedy...
                            {
                                using (var writer = File.AppendText($"{Id}_{Type}_{fileName}"))
                                using (var writer2 = File.AppendText($"audit.txt"))
                                {
                                    writer.WriteLine(10);
                                    writer2.WriteLine($"{Id} {Type} - {10}        {DateTime.UtcNow}");
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
                                    writer2.WriteLine($"{Id} {Type} - {15}        {DateTime.UtcNow}");
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
                                    writer2.WriteLine($"{Id} {Type} - {20}        {DateTime.UtcNow}");
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
                                    writer2.WriteLine($"{Id} {Type} - {25}        {DateTime.UtcNow}");
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
                            writer2.WriteLine($"{Id} {Type} - {stock}        {DateTime.UtcNow}");
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
                            writer2.WriteLine($"{Id} {Type} - {stock}        {DateTime.UtcNow}");
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

        // public override Statistics GetStatistics()
        // {
        //     var result = new Statistics();
        //     result.SellsSum = 0;
        //     result.SellAverage = 0;
        //     result.SellHigh = double.MinValue;
        //     result.SellLow = double.MaxValue;




        //     for (int i = 0; i < listaSell.Count; i++)
        //     {
        //         result.SellLow = Math.Min(result.SellLow, listaSell[i]);
        //         result.SellHigh = Math.Max(result.SellHigh, listaSell[i]);
        //         result.SellsSum = result.SellsSum + listaSell[i];
        //     }

        //     result.SellAverage = result.SellsSum / listaSell.Count;
        //     result.Left = this.Amount - result.SellsSum;
        //     this.SellsSum = result.SellsSum;

        //     switch (result.SellAverage)
        //     {
        //         case var d when d >= 90:
        //             result.Letter = 'A';
        //             break;

        //         case var d when d >= 80:
        //             result.Letter = 'B';
        //             break;

        //         case var d when d >= 60:
        //             result.Letter = 'C';
        //             break;

        //         default:
        //             result.Letter = 'D';
        //             break;
        //     }
        //     return result;
        // }

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

