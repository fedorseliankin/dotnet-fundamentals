using AdoNet.models;
using AdoNet.repositories;
using AdoNet.services;
ADOHelper aDOHelper = new ADOHelper("data source=DESKTOP-DK1A54F;initial catalog=MyDatabase;trusted_connection=true");
var or = new OrderRepository(aDOHelper);
var pr = new ProductRepository(aDOHelper);
var order = new Order()
    {
        CreatedDate = DateTime.Now,
        ProductID = 1,
        Status = 0,
        UpdatedDate = DateTime.Now,
};
or.Add(order);
var product = pr.Get(1);

Console.WriteLine(product.ToString());
