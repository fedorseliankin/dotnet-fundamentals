using AdoNet.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Fund.services
{
    public class OrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> AddOrder(Order order)
        {
            _context.Order.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> UpdateOrder(Order order)
        {
            _context.Order.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _context.Order.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task DeleteOrder(int id)
        {
            var order = await GetOrder(id);
            if (order != null)
            {
                _context.Order.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Order>> GetOrders(int? month, int? year, string status, int? productID)
        {
            var sql = "EXEC [FetchOrders] @Month, @Year, @Status, @ProductID";
            return (await _context.Order.FromSqlRaw(
                sql,
                new SqlParameter("@Month", month ?? (object)DBNull.Value),
                new SqlParameter("@Year", year ?? (object)DBNull.Value),
                new SqlParameter("@Status", status ?? (object)DBNull.Value),
                new SqlParameter("@ProductID", productID ?? (object)DBNull.Value)
            ).ToListAsync());
        }
        public async Task DeleteOrders(int? month, int? year, string status, int? productId)
        {
            var orders = await GetOrders(month, year, status, productId);
            _context.Order.RemoveRange(orders);
            await _context.SaveChangesAsync();
        }
    }
}
