namespace HarryPotter.Tests;
[TestClass]
public class UnitTest1
{
    BookShop bookShop = new BookShop();

    [TestMethod]
    public void TestCalculatePrice_SingleBookNoDiscount()
    {
        var basket = new Dictionary<int, int>
        {
            { 1, 1 }
        };

        double totalPrice = bookShop.CalculatePrice(basket);

        Assert.AreEqual(8, totalPrice);
    }

    [TestMethod]
    public void TestCalculatePrice_TwoDifferentBooksDiscount()
    {
        var basket = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 1 }
        };

        double totalPrice = bookShop.CalculatePrice(basket);

        Assert.AreEqual(15.2, totalPrice);
    }

    [TestMethod]
    public void TestCalculatePrice_AllFiveDifferentBooksDiscount()
    {
        var basket = new Dictionary<int, int>
        {
            { 1, 1 },
            { 2, 1 },
            { 3, 1 },
            { 4, 1 },
            { 5, 1 }
        };

        double totalPrice = bookShop.CalculatePrice(basket);

        Assert.AreEqual(30, totalPrice);
    }

    [TestMethod]
    public void TestCalculatePrice_ExampleScenario()
    {
        var basket = new Dictionary<int, int>
        {
            { 1, 2 },
            { 2, 2 },
            { 3, 2 },
            { 4, 1 },
            { 5, 1 }
        };

        double totalPrice = bookShop.CalculatePrice(basket);

        Assert.AreEqual(51.6, totalPrice);
    }
}