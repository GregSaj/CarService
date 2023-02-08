using CarService;
namespace challenge.tests;

public class UnitTest1
{
    [Fact]
    public void TestGetStatistics()
    {
        //arrange
        var prod = new InMemoryProduct(15, "AirFilter", "PP670", 56.99, 90);
        prod.AddSells("70");
        prod.AddSells("10");

        // act
        var result = prod.GetStatistics();

        // assert
        Assert.Equal(40, Math.Round(result.SellAverage, 2));
        Assert.Equal(70, result.SellHigh);
        Assert.Equal(10, result.SellLow);
    }
}