using AdoNet.models;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ORM_Fund;
using ORM_Fund.services;

var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
optionsBuilder.UseSqlServer("");

AppDbContext _context = new AppDbContext(optionsBuilder.Options);
ProductService productService = new ProductService(_context);
var newProduct = new Product
{
    Name = "New Product",
    Description = "Product Description",
    Weight = 50.5m,
    Height = 25.5m,
    Width = 22.5m,
    Length = 12.5m
};
await productService.AddProduct(newProduct);