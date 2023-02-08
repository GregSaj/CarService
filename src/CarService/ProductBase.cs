namespace CarService
{

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
}