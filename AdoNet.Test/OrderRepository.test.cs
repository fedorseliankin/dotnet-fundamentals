using AdoNet.models;
using AdoNet.repositories;
using Moq;
using System.Data.SqlClient;
[TestClass]
public class OrderRepositoryTests
{
    private Mock<IADOHelper> mockAdoHelper;
    private OrderRepository orderRepository;

    [TestInitialize]
    public void Setup()
    {
        mockAdoHelper = new Mock<IADOHelper>();
        orderRepository = new OrderRepository(mockAdoHelper.Object);
    }

    [TestMethod]
    public void Add_ValidOrder_ExecutesCorrectly()
    {
        //Arrange
        var order = new Order
        {
            Status = OrderStatus.InProgress,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            ProductID = 1
        };

        string expectedQuery = "INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductID) VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductID)";

        // Set up the Mock.
        mockAdoHelper.Setup(m => m.ExecuteCommand(It.IsAny<string>(), It.IsAny<List<SqlParameter>>()))
            .Callback<string, List<SqlParameter>>((query, parameters) =>
            {
                Assert.AreEqual(expectedQuery, query);

                Assert.IsTrue(parameters.Any(param => param.ParameterName == "@Status" && param.Value.ToString().Equals(order.Status.ToString())));
                Assert.IsTrue(parameters.Any(param => param.ParameterName == "@CreatedDate" && param.Value.ToString().Equals(order.CreatedDate.ToString())));
                Assert.IsTrue(parameters.Any(param => param.ParameterName == "@UpdatedDate" && param.Value.ToString().Equals(order.UpdatedDate.ToString())));
                Assert.IsTrue(parameters.Any(param => param.ParameterName == "@ProductID" && param.Value.ToString().Equals(order.ProductID.ToString())));
            });

        //Act
        orderRepository.Add(order);

        //Assert
        mockAdoHelper.Verify();
    }

    [TestMethod]
    public void Get_ValidId_CorrectMethodAndParameters()
    {
        int testId = 1;
        string expectedQuery = "SELECT * FROM Order WHERE Id = @Id";

        // Setup and expectations
        var order = new Order
        {
            Id = 1,
            Status = OrderStatus.InProgress,
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            ProductID = 1
        };

        mockAdoHelper.Setup(m => m.ExecuteSingleReader(
            It.Is<string>(query => query == expectedQuery),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersGet(parameters, testId)),
            It.IsAny<Func<SqlDataReader, Order>>())
        ).Returns(order);

        // Act
        var result = orderRepository.Get(testId);

        // Assert 
        Assert.IsNotNull(result);
        Assert.AreEqual(order.Id, result.Id);
    }

    private bool ValidateParametersGet(List<SqlParameter> parameters, int expectedId)
    {
        var idParam = parameters.FirstOrDefault(x => x.ParameterName == "@Id");

        if (idParam == null || Convert.ToInt32(idParam.Value) != expectedId)
            return false;

        return true;
    }

    [TestMethod]
    public void Update_ValidOrder_ExecutesCorrectly()
    {
        // Arrange
        Order order = new Order
        {
            Id = 1,
            Status = OrderStatus.Done,
            CreatedDate = DateTime.Now.AddDays(-5),
            UpdatedDate = DateTime.Now,
            ProductID = 10
        };

        string expectedQuery = "UPDATE Order SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductID = @ProductID WHERE Id = @Id";

        // Act
        orderRepository.Update(order);

        // Assert
        mockAdoHelper.Verify(m => m.ExecuteCommand(
            It.Is<string>(s => s == expectedQuery),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersUpdate(parameters, order))),
            Times.Once);
    }

    private bool ValidateParametersUpdate(List<SqlParameter> parameters, Order expectedOrder)
    {
        var idParam = parameters.FirstOrDefault(x => x.ParameterName == "@Id");
        var statusParam = parameters.FirstOrDefault(x => x.ParameterName == "@Status");

        if (idParam == null || Convert.ToInt32(idParam.Value) != expectedOrder.Id)
            return false;
        if (statusParam == null || statusParam.Value.ToString() != expectedOrder.Status.ToString())
            return false;

        return true;
    }

    [TestMethod]
    public void Delete_ValidId_ExecutesCorrectly()
    {
        // Arrange 
        int idToDelete = 1;
        string expectedQuery = "DELETE FROM Order WHERE Id = @Id";

        // Act
        orderRepository.Delete(idToDelete);

        // Assert
        mockAdoHelper.Verify(m => m.ExecuteCommand(
            It.Is<string>(s => s == expectedQuery),
            It.Is<List<SqlParameter>>(p => p.Count == 1 && (int)p[0].Value == idToDelete)),
        Times.Once);
    }

    [TestMethod]
    public void GetOrders_ValidFilters_ReturnsMatchingOrders()
    {
        // Arrange
        var expectedOrders = new List<Order>
        {
            new Order { Id = 1, Status = OrderStatus.Done, CreatedDate = new DateTime(2022, 1, 1), UpdatedDate = DateTime.Now, ProductID = 1 },
            new Order { Id = 2, Status = OrderStatus.Done, CreatedDate = new DateTime(2022, 1, 2), UpdatedDate = DateTime.Now, ProductID = 2 }
        };

        mockAdoHelper.Setup(m => m.ExecuteReader(
            It.Is<string>(sproc => sproc == "[FetchOrders]"),
            It.Is<List<SqlParameter>>(parameters => ValidateParametersFetchOrders(parameters, 1, 2022, OrderStatus.Done, 1)),
            It.IsAny<Func<SqlDataReader, Order>>())
        ).Returns(expectedOrders);

        // Act
        var result = orderRepository.GetOrders(1, 2022, OrderStatus.Done, 1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedOrders.Count, result.Count);
    }

    private bool ValidateParametersFetchOrders(List<SqlParameter> parameters, int? month, int? year, OrderStatus? status, int? productId)
    {
        var monthParam = parameters.FirstOrDefault(x => x.ParameterName == "@Month");
        var yearParam = parameters.FirstOrDefault(x => x.ParameterName == "@Year");
        var statusParam = parameters.FirstOrDefault(x => x.ParameterName == "@Status");
        var productIdParam = parameters.FirstOrDefault(x => x.ParameterName == "@ProductID");

        if (monthParam != null && monthParam.Value != DBNull.Value && Convert.ToInt32(monthParam.Value) != month)
            return false;
        if (yearParam != null && yearParam.Value != DBNull.Value && Convert.ToInt32(yearParam.Value) != year)
            return false;
        if (statusParam != null && statusParam.Value != DBNull.Value && statusParam.Value.ToString() != status.ToString())
            return false;
        if (productIdParam != null && productIdParam.Value != DBNull.Value && Convert.ToInt32(productIdParam.Value) != productId)
            return false;

        return true;
    }

    [TestMethod]
    public void DeleteOrders_ValidFilters_CallsExecuteTransactionWithCorrectParams()
    {
        // Arrange
        int expectedMonth = 1;
        int expectedYear = 2022;
        OrderStatus expectedStatus = OrderStatus.Done;
        int expectedProductId = 1;

        // Act
        orderRepository.DeleteOrders(expectedMonth, expectedYear, expectedStatus, expectedProductId);

        // Assert
        mockAdoHelper.Verify(m => m.ExecuteTransaction(
            It.Is<string>(s => s == "DeleteOrders"),
            It.Is<List<SqlParameter>>(p => ValidateParametersDeleeteOrders(p, expectedMonth, expectedYear, expectedStatus, expectedProductId))),
        Times.Once);
    }

    private bool ValidateParametersDeleeteOrders(List<SqlParameter> parameters, int? month, int? year, OrderStatus? status, int? productId)
    {
        var monthParam = parameters.FirstOrDefault(x => x.ParameterName == "@Month");
        var yearParam = parameters.FirstOrDefault(x => x.ParameterName == "@Year");
        var statusParam = parameters.FirstOrDefault(x => x.ParameterName == "@Status");
        var productIdParam = parameters.FirstOrDefault(x => x.ParameterName == "@ProductId");

        if (monthParam != null && monthParam.Value != DBNull.Value && Convert.ToInt32(monthParam.Value) != month)
            return false;
        if (yearParam != null && yearParam.Value != DBNull.Value && Convert.ToInt32(yearParam.Value) != year)
            return false;
        if (statusParam != null && statusParam.Value != DBNull.Value && statusParam.Value.ToString() != status.ToString())
            return false;
        if (productIdParam != null && productIdParam.Value != DBNull.Value && Convert.ToInt32(productIdParam.Value) != productId)
            return false;

        return true;
    }
}