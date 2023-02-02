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
            SellHigh = double.MaxValue;
            SellLow = double.MinValue;

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
                    case var d when d >= 90:
                        return 'A';

                    case var d when d >= 80:
                        return 'B';


                    case var d when d >= 60:
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
            SellHigh = Math.Max(sells, SellLow );
            SellLow = Math.Min(sells, SellHigh);
        }
    }
}