namespace MineField.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void Test_ProcessField()
    {
        // Arrange
        char[,] input = new char[,]
        {
                { '*', '.', '.', '.' },
                { '.', '.', '*', '.' },
                { '.', '.', '.', '.' }
        };

        int[,] expectedField = new int[,]
        {
                { 0, 2, 1, 1 },
                { 1, 2, 0, 1 },
                { 0, 1, 1, 1 }
        };

        // Act
        int[,] output = MineField.ProcessField(input);

        // Assert
        CollectionAssert.AreEqual(expectedField, output);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "incorrect input")]
    public void Test_ProcessField_ThrowsException_ForIncorrectInput()
    {
        // Arrange
        char[,] input = new char[,]
        {
                { '.', '.', '*', '.', 'a' }
        };

        // Act
        MineField.ProcessField(input);
    }
}
