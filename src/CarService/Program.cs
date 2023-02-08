using System;
using System.Collections.Generic;
using System.IO;

namespace CarService
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("           ||| Program CarService |||                    ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"This app alows you to manage sells in your car workshop \nand save it on a file or keep it on your computer memory.");
            Console.ResetColor();
            Console.WriteLine();

            //Products - objects from class SavedProduct
            SavedProduct product1 = new SavedProduct(1, "OilFiltr", "OP520", 59.99, 150);
            SavedProduct product2 = new SavedProduct(2, "OilFiltr", "OP525", 69.99, 90);
            SavedProduct product3 = new SavedProduct(3, "AirFiltr", "PP100", 39.99, 50);
            SavedProduct product4 = new SavedProduct(4, "AirFiltr", "OP120", 29.99, 60);
            SavedProduct product5 = new SavedProduct(5, "CabinFiltr", "OC1000", 100.00, 20);
            SavedProduct product6 = new SavedProduct(6, "CabinFiltr", "OC1002", 120.00, 18);
            SavedProduct product7 = new SavedProduct(7, "Oil", "Syntetic", 90.00, 1000);
            SavedProduct product8 = new SavedProduct(8, "Oil", "Semi-Syntetic", 79.00, 1000);

            //Products - objects from class InMemoryProduct
            InMemoryProduct product1x = new InMemoryProduct(1, "OilFiltr", "OP520", 59.99, 150);
            InMemoryProduct product2x = new InMemoryProduct(2, "OilFiltr", "OP525", 69.99, 90);
            InMemoryProduct product3x = new InMemoryProduct(3, "AirFiltr", "PP100", 39.99, 50);
            InMemoryProduct product4x = new InMemoryProduct(4, "AirFiltr", "OP120", 29.99, 60);
            InMemoryProduct product5x = new InMemoryProduct(5, "CabinFiltr", "OC1000", 100.00, 20);
            InMemoryProduct product6x = new InMemoryProduct(6, "CabinFiltr", "OC1002", 120.00, 18);
            InMemoryProduct product7x = new InMemoryProduct(7, "Oil", "Syntetic", 90.00, 1000);
            InMemoryProduct product8x = new InMemoryProduct(8, "Oil", "Semi-Syntetic", 79.00, 1000);

            var listOfSavedProducts = new List<SavedProduct>() { product1, product2, product3, product4, product5, product6, product7, product8 };
            var listOfMemorizedProducts = new List<InMemoryProduct>() { product1x, product2x, product3x, product4x, product5x, product6x, product7x, product8x };

            foreach (var item in listOfSavedProducts)
            {
                item.SellsAdded += OnSellsAdded;
                item.LowSellsAdded += LowSellsAdded;
            }

            foreach (var item in listOfMemorizedProducts)
            {
                item.SellsAdded += OnSellsAdded;
                item.LowSellsAdded += LowSellsAdded;
            }


            static void OnSellsAdded(object sender, EventArgs args)
            {
                Console.WriteLine($"New sells added.");
            }

            static void LowSellsAdded(object sender, EventArgs args)
            {
                Console.WriteLine($"Come on, you can sell more than that!");
            }

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Enter propper key to proceed: ");
                Console.ResetColor();
                Console.WriteLine("A) See list of products.\nB) Enter sales and save it to the file .txt.\nC) Enter sales and keep it in memory.\nQ) Quit program.  ");

                var choose = Console.ReadLine();

                if (choose == "q" || choose == "Q")
                {
                    Console.WriteLine("Thanks for using CarService!");
                    break;
                }

                switch (choose)
                {
                    case "A" or "a":

                        DisplayListOfProducts(listOfMemorizedProducts);

                        break;

                    case "B" or "b":

                        CheckIfFileExistAndAskIfDeleteItProduct1(product1);
                        CheckIfFileExistAndAskIfDeleteItProduct2(product2);
                        CheckIfFileExistAndAskIfDeleteItProduct3(product3);
                        CheckIfFileExistAndAskIfDeleteItProduct4(product4);
                        CheckIfFileExistAndAskIfDeleteItProduct5(product5);
                        CheckIfFileExistAndAskIfDeleteItProduct6(product6);
                        CheckIfFileExistAndAskIfDeleteItProduct7(product7);
                        CheckIfFileExistAndAskIfDeleteItProduct7(product8);

                        while (true)
                        {
                            DisplayListOfProducts(listOfSavedProducts);

                            ChooseIdOrQToExit();

                            var choose1 = Console.ReadLine();

                            if (choose1 == "q" || choose1 == "Q")
                            {

                                break;
                            }

                            switch (choose1)
                            {
                                case "1":

                                    EnterSales(product1);

                                    ShowEndStats(product1);

                                    break;

                                case "2":

                                    EnterSales(product2);

                                    ShowEndStats(product2);

                                    break;

                                case "3":

                                    EnterSales(product3);

                                    ShowEndStats(product3);

                                    break;

                                case "4":

                                    EnterSales(product4);

                                    ShowEndStats(product4);

                                    break;

                                case "5":

                                    EnterSales(product5);

                                    ShowEndStats(product5);

                                    break;

                                case "6":

                                    EnterSales(product6);

                                    ShowEndStats(product6);

                                    break;

                                case "7":

                                    EnterSales(product7);

                                    ShowEndStats(product7);

                                    break;

                                case "8":

                                    EnterSales(product8);

                                    ShowEndStats(product8);

                                    break;

                                default:

                                    Console.WriteLine("Wrong key. Try one more time.");

                                    break;
                            }
                        }

                        break;

                    case "C" or "c":

                        while (true)
                        {
                            DisplayListOfProducts(listOfMemorizedProducts);

                            ChooseIdOrQToExit();

                            var choose2 = Console.ReadLine();

                            if (choose2 == "q" || choose2 == "Q")
                            {
                                break;
                            }

                            switch (choose2)
                            {
                                case "1":

                                    EnterSales(product1x);

                                    ShowEndStats(product1x);

                                    break;

                                case "2":

                                    EnterSales(product2x);

                                    ShowEndStats(product2x);

                                    break;

                                case "3":

                                    EnterSales(product3x);

                                    ShowEndStats(product3x);

                                    break;

                                case "4":

                                    EnterSales(product4x);

                                    ShowEndStats(product4x);

                                    break;

                                case "5":

                                    EnterSales(product5x);

                                    ShowEndStats(product5x);

                                    break;

                                case "6":

                                    EnterSales(product6x);

                                    ShowEndStats(product6x);

                                    break;

                                case "7":

                                    EnterSales(product7x);

                                    ShowEndStats(product7x);

                                    break;

                                case "8":

                                    EnterSales(product8x);

                                    ShowEndStats(product8x);

                                    break;

                                default:

                                    Console.WriteLine("Wrong key. Try one more time.");

                                    break;
                            }
                        }

                        break;

                    default:

                        Console.WriteLine("Wrong key. Try one more time.");

                        break;
                }
            }
        }

        private static void ChooseIdOrQToExit()
        {
            Console.WriteLine("Choose your product by Id: or q to exit.");
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct1(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 150;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator)
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;
                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct2(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 90;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct3(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 90;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct4(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 60;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct5(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 20;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct6(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 18;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void CheckIfFileExistAndAskIfDeleteItProduct7(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");
                        product1.Amount = 1000;

                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
        private static void DisplayListOfProducts(List<InMemoryProduct> listOfMemorizedProducts)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("List of products:");
            Console.ResetColor();

            foreach (var item in listOfMemorizedProducts)
            {
                item.ShowProduct();
            }

            Console.WriteLine();
        }
        private static void DisplayListOfProducts(List<SavedProduct> ListofProducts) 
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("List of products:");
            Console.ResetColor();

            foreach (var item in ListofProducts)
            {
                item.ShowProduct();
            }

            Console.WriteLine();
        }
        private static void EnterSales(IProduct product1)
        {
            while (true)
            {
                Console.WriteLine($"Enter sales for product {product1.Id}. You can press A for adding 10, A+ adds 15, B adds 20 and B+ adds 25. Q to quit.");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    break;
                }
                try
                {
                    var sells = input;
                    product1.AddSells(sells);

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        private static void ShowEndStats(IProduct product1x)
        {
            var stats = product1x.GetStatistics();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Here are sales statistics of {product1x.Type}:");
            Console.ResetColor();
            Console.WriteLine($"Sum: {stats.SellsSum}");
            Console.WriteLine($"High: {stats.SellHigh}");
            Console.WriteLine($"Low: {stats.SellLow}");
            Console.WriteLine($"Average: {stats.SellAverage:F2}");
            Console.WriteLine($"Left: {stats.Left}");
            Console.WriteLine($"Letter: {stats.Letter}");
            Console.WriteLine();
        }
        private static void CheckIfFileExistAndAskIfDeleteIt(SavedProduct product1)
        {
            if (File.Exists($"{product1.fullFileName}"))
            {
                Console.WriteLine($"Theres is file {product1.fullFileName} in folder. Do you want to delete it? Y/N");
                var answer1 = Console.ReadLine();
                while (true)
                {
                    if (answer1 == "Y" || answer1 == "y")
                    {
                        File.Delete($"{product1.fullFileName}");


                        break;
                    }
                    else if (answer1 == "N" || answer1 == "n")
                    {
                        double sumFromFile = 0;

                        using (var reader = File.OpenText($"{product1.fullFileName}"))
                        {
                            var line = reader.ReadLine();
                            while (line != null)
                            {
                                var number = double.Parse(line);
                                sumFromFile += number;
                                line = reader.ReadLine();
                            }

                            if (!product1.fileCalculator) //jezeli nie ma pliku to nie wchodzi tutaj, nastepnie przy wyborze jest tworzony plik i dodawana jest sells do niego, polecenie zmniejszenia amount w klasie sellsprodcut dziala i potem ponownie uruchumiajac program B wchodzi do wiersza 389
                            {
                                product1.Amount = product1.Amount - sumFromFile;
                                product1.fileCalculator = true;
                            }

                            break;

                        }

                    }
                    else
                    {
                        Console.WriteLine($"Invalid format.");
                    }
                }
            }
        }
    }
}