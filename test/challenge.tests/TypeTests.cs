using CarService;
using System;
using Xunit;


public class TypeTests
{

    public delegate string WriteMessage(string message);

    int counter = 0;

    [Fact]
    public void WriteMessageDelegateCanPointToMethod()
    {

        WriteMessage del = ReturnMessage;

        // del = ReturnMessage; podpięcie metody pod delegat

        del += ReturnMessage;
        del += ReturnMessage2;

        var result = del("Hello!");

        Assert.Equal(3, counter);

    }

    string ReturnMessage(string message)
    {
        counter++;
        return message;
    }

    string ReturnMessage2(string message)
    {
        counter++;
        return message.ToUpper();
    }


    [Fact]
    public void GetProductReturnDifferentsObjects()
    {
        //arrange
        var product1 = GetProduct(17, "FuelFilter", "FP300", 300.99, 10);
        var product2 = GetProduct(18, "FuelFilter", "FP310", 320.99, 14);

        //act

        // assert
        Assert.Equal("FuelFilter", product1.ProductName);
        Assert.Equal("FP310", product2.Type);
    }

    [Fact]
    public void CanSetProductFromReference()
    {
        //arrange
        var product1 = GetProduct(17, "FuelFilter", "FP300", 300.99, 10);

        //act
        this.SetTypeofProduct(product1, "FP400");

        // assert
        Assert.Equal("FP400", product1.Type);
    }

    [Fact]
    public void TwoVarsCanReferenceSameObject()
    {
        //arrange
        var product1 = GetProduct(17, "FuelFilter", "FP300", 300.99, 10);
        var product2 = GetProduct(18, "FuelFilter", "FP310", 320.99, 14);

        //act

        // assert
        Assert.NotSame(product1.Price, product2.Price);
        Assert.False(Object.ReferenceEquals(product1.Price, product2.Price));
    }

    [Fact]
    public void CSharpCanPassByRef()
    {
        //arrange
        var product1 = GetProduct(17, "FuelFilter", "FP300", 300.99, 10);

        //act
        GetProductSetAtributes(out product1, 19, "AirFilter", "PP790", 350.49, 13);

        // assert
        Assert.Equal(350.49, product1.Price);
    }


    [Fact]
    public void Test1()
    {
        //arrange
        var x = GetInt();

        //act
        SetInt(ref x);

        // assert
        Assert.Equal(x, 42);
    }

    [Fact]
    public void StringBehaveLikeValueType()
    {
        //arrange
        var x = "Grzegorz is the best";

        //act
        var upper = this.MakeUpperCase(x);
        // assert
        Assert.Equal(x, "Grzegorz is the best");
        Assert.Equal(upper, "GRZEGORZ IS THE BEST");
    }

    private string MakeUpperCase(string parametr)
    {
        return parametr.ToUpper();
    }

    private void SetInt(ref int x)
    {
        x = 42;
    }

    private int GetInt()
    {
        return 3;
    }

    private void GetProductSetAtributes(out SavedProduct item1, int no, string product, string type, double price, int amount)
    {
        item1 = new SavedProduct(no, product, type, price, amount);

    }

    private SavedProduct GetProduct(int no, string product, string type, double price, int amount)
    {
        return new SavedProduct(no, product, type, price, amount);
    }

    private void SetTypeofProduct(SavedProduct item1, string type)
    {
        item1.Type = type;
    }
}
