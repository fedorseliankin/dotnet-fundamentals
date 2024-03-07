using AdoNet.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Fund.services
{
    public class ProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task DeleteProduct(int id)
        {
            var product = await GetProduct(id);
            if (product != null)
            {
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Product.ToListAsync();
        }
    }
}
