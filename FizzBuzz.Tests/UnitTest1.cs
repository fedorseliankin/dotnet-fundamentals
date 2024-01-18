using FizzBuzz;
namespace FizzBuzz.Tests;

[TestClass]
public class FizzBuzzTests
{
    [TestMethod]
    public void GetFizzBuzz_InputIsDivisibleBy3And5_ReturnsFizzBuzz()
    {
        // Act
        var result = FizzBuzz.GetFizzBuzz(15);
        // Assert
        Assert.AreEqual("FizzBuzz", result);
    }

    [TestMethod]
    public void GetFizzBuzz_InputIsDivisibleBy3_ReturnsFizz()
    {
        // Arrange
        // Act
        var result = FizzBuzz.GetFizzBuzz(6);
        // Assert
        Assert.AreEqual("Fizz", result);
    }

    [TestMethod]
    public void GetFizzBuzz_InputIsDivisibleBy5_ReturnsBuzz()
    {
        // Arrange
        // Act
        var result = FizzBuzz.GetFizzBuzz(10);
        // Assert
        Assert.AreEqual("Buzz", result);
    }

    [TestMethod]
    public void GetFizzBuzz_InputIsNotDivisibleBy3Or5_ReturnsInputAsString()
    {
        // Arrange
        // Act
        var result = FizzBuzz.GetFizzBuzz(7);
        // Assert
        Assert.AreEqual("7", result);
    }
}