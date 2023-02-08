using System;

namespace CarService
{
    public class Statistics
    {
        public double SellLow { get; set; }
        public double SellHigh { get; set; }
        public double Left { get; set; }
        public double SellsSum { get; set; }
        public double Count { get; set; }

        public Statistics()
        {
            Count = 0;
            SellsSum = 0;
            SellHigh = double.MinValue;
            SellLow = double.MaxValue;
            
        }

        public double SellAverage
        {
            get
            {
                return SellsSum / Count;
            }
        }

        public char Letter
        {
            get
            {
                switch (SellAverage)
                {
                    case >= 90:
                        return 'A';

                    case >= 80:
                        return 'B';

                    case >= 60:
                        return 'B';

                    default:
                        return 'D';

                }
            }
        }

        public void AddSells(double sells)
        {
            SellsSum += sells;
            Count += 1;
            SellHigh = Math.Max(sells, SellHigh);
            SellLow = Math.Min(sells, SellLow);
        }

    }
}