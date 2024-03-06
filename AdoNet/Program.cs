using AdoNet.models;
using AdoNet.repositories;
using AdoNet.services;
ADOHelper myADOHelper = new ADOHelper("example");
var or = new OrderRepository(myADOHelper);
var pr = new ProductRepository(myADOHelper);
var order = new Order()
    {
        CreatedDate = DateTime.Now,
        ProductID = 1,
        Status = 0,
        UpdatedDate = DateTime.Now,
};
or.Add(order);
var product = pr.GetAll();

Console.WriteLine(product.ToString());
