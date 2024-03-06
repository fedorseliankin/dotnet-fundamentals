using AdoNet.models;
using AdoNet.repositories;
using AdoNet.services;
using Moq;
using System.Data.SqlClient;
[TestClass]
public class ProductRepositoryTests
{
    private Mock<IADOHelper> mockAdoHelper;
    private ProductRepository productRepository;

    [TestInitialize]
    public void Setup()
    {
        mockAdoHelper = new Mock<IADOHelper>();
        productRepository = new ProductRepository(mockAdoHelper.Object);
    }

    [TestMethod]
    public void Add_ValidProduct_ExecutesCorrectly()
    {
        //Arrange
        var product = new Product
        {
            Name = "Product1",
            Description = "Description1",
            Weight = 1.0M,
            Height = 2.0M,
            Width = 3.0M,
            Length = 4.0M
        };

        string expectedQuery = "INSERT INTO Product (Name, Description, Weight, Height, Width, Length) VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

        //Act
        productRepository.Add(product);

        //Assert
        mockAdoHelper.Verify(m => m.ExecuteCommand(
            It.Is<string>(query => query == expectedQuery),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersAdd(parameters, product))
        ), Times.Once);
    }

    private bool ValidateParametersAdd(List<SqlParameter> parameters, Product expectedProduct)
    {
        var nameParam = parameters.FirstOrDefault(x => x.ParameterName == "@Name");
        var heightParam = parameters.FirstOrDefault(x => x.ParameterName == "@Height");
        // include all other parameters 

        if (nameParam == null || nameParam.Value.ToString() != expectedProduct.Name)
            return false;
        if (heightParam == null || Convert.ToDecimal(heightParam.Value) != expectedProduct.Height)
            return false;

        return true;
    }

    [TestMethod]
    public void Get_ValidId_CorrectMethodAndParameters()
    {
        int testId = 1;
        string expectedQuery = "SELECT * FROM Product WHERE Id = @Id";

        // Setup and expectations
        var product = new Product
        {
            Id = 1,
            Name = "Test Product",
            Description = "Test Description",
            Weight = 1.0M,
            Height = 2.0M,
            Width = 3.0M,
            Length = 4.0M
        };

        mockAdoHelper.Setup(m => m.ExecuteSingleReader(
            It.Is<string>(query => query == expectedQuery),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersGet(parameters, testId)),
            It.IsAny<Func<SqlDataReader, Product>>())
        ).Returns(product);

        // Act
        var result = productRepository.Get(testId);

        // Assert 
        Assert.IsNotNull(result);
        Assert.AreEqual(product.Id, result.Id);
    }

    private bool ValidateParametersGet(List<SqlParameter> parameters, int expectedId)
    {
        var idParam = parameters.FirstOrDefault(x => x.ParameterName == "@Id");

        if (idParam == null)
            return false;
        if (idParam.Value == null || Convert.ToInt32(idParam.Value) != expectedId)
            return false;

        return true;
    }

    [TestMethod]
    public void Update_ValidProduct_ExecutesCorrectly()
    {
        // Arrange
        var product = new Product
        {
            Id = 1,
            Name = "UpdatedProduct",
            Description = "UpdatedDescription",
            Weight = 2.0M,
            Height = 3.0M,
            Width = 4.0M,
            Length = 5.0M
        };

        string expectedQuery = "UPDATE Product SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id";

        // Act
        productRepository.Update(product);

        // Assert
        mockAdoHelper.Verify(m => m.ExecuteCommand(
            It.Is<string>(query => query == expectedQuery),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersUpdate(parameters, product))),
        Times.Once);
    }

    private bool ValidateParametersUpdate(List<SqlParameter> parameters, Product expectedProduct)
    {
        var idParam = parameters.FirstOrDefault(x => x.ParameterName == "@Id");
        var nameParam = parameters.FirstOrDefault(x => x.ParameterName == "@Name");

        if (idParam == null || Convert.ToInt32(idParam.Value) != expectedProduct.Id)
            return false;
        if (nameParam == null || nameParam.Value.ToString() != expectedProduct.Name)
            return false;

        return true;
    }

    [TestMethod]
    public void GetAll_InvokesDataReaderWithCorrectQueryAndReturnsProducts()
    {
        // Arrange
        string query = "SELECT * FROM Products";

        var mockReader = new Mock<IDataReaderWrapper>();
        mockReader.SetupSequence(r => r.Read())
            .Returns(true)
            .Returns(true)
            .Returns(false);


        mockAdoHelper.Setup(m => m.ExecuteReaderWithConnection(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
            .Returns(mockReader.Object);

        mockReader.Setup(r => r.GetInt32(0)).Returns(1);
        mockReader.Setup(r => r.GetString(1)).Returns("Product 1");
        mockReader.Setup(r => r.GetString(2)).Returns("Product 1 Description");

        mockAdoHelper.Setup(m => m.ExecuteReaderWithConnection(query, It.IsAny<List<SqlParameter>>())).Returns(mockReader.Object);

        // Act
        var result = productRepository.GetAll();

        // Assert
        Assert.AreEqual(2, result.Count());
        Assert.AreEqual("Product 1", result.First().Name);
    }

    [TestMethod]
    public void Delete_ValidId_ExecutesCorrectly()
    {
        // Arrange 
        int idToDelete = 1;
        string expectedQuery = "DELETE FROM Product WHERE Id = @Id";

        // Act
        productRepository.Delete(idToDelete);

        // Assert
        mockAdoHelper.Verify(m => m.ExecuteCommand(
            It.Is<string>(s => s == expectedQuery),
            It.Is<List<SqlParameter>>(p => p.Count == 1 && (int)p[0].Value == idToDelete)),
        Times.Once);
    }
}
