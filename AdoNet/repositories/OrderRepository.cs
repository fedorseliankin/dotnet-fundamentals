using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using AdoNet.models;
namespace AdoNet.repositories
{
    public class OrderRepository
    {
        private IADOHelper _helper;
        public OrderRepository(IADOHelper helper)
        {
            this._helper = helper;
        }
        public void Add(Order order)
        {
            string query = "INSERT INTO [Order] (Status, CreatedDate, UpdatedDate, ProductID) VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductID)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Status", order.Status.ToString()),
                new SqlParameter("@CreatedDate", order.CreatedDate),
                new SqlParameter("@UpdatedDate", order.UpdatedDate),
                new SqlParameter("@ProductID", order.ProductID)
            };
            _helper.ExecuteCommand(query, parameters);
        }

        public Order Get(int id)
        {
            string query = "SELECT * FROM Order WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return _helper.ExecuteSingleReader(query, parameters, (rdr) =>
            {
                return new Order
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), rdr["Status"].ToString()),
                    CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]),
                    UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]),
                    ProductID = Convert.ToInt32(rdr["ProductID"]),
                };
            });
        }

        public void Update(Order order)
        {
            string query = "UPDATE Order SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductID = @ProductID WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", order.Id),
                new SqlParameter("@Status", order.Status.ToString()),
                new SqlParameter("@CreatedDate", order.CreatedDate),
                new SqlParameter("@UpdatedDate", order.UpdatedDate),
                new SqlParameter("@ProductID", order.ProductID)
            };

            _helper.ExecuteCommand(query, parameters);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Order WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };
            _helper.ExecuteCommand(query, parameters);
        }

        public List<Order> GetOrders(int? month = null, int? year = null, OrderStatus? status = null, int? productId = null)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Month", (object)month ?? DBNull.Value),
                new SqlParameter("@Year", (object)year ?? DBNull.Value),
                new SqlParameter("@Status", status?.ToString() ?? (object)DBNull.Value),
                new SqlParameter("@ProductId", (object)productId ?? DBNull.Value),
            };

            return _helper.ExecuteReader("[FetchOrders]", parameters, rdr =>
            {
                return new Order
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), rdr["Status"].ToString()),
                    CreatedDate = Convert.ToDateTime(rdr["CreatedDate"]),
                    UpdatedDate = Convert.ToDateTime(rdr["UpdatedDate"]),
                    ProductID = Convert.ToInt32(rdr["ProductID"]),
                };
            });
        }

        public void DeleteOrders(int? month = null, int? year = null, OrderStatus? status = null, int? productID = null)
        {
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Month", (object)month ?? DBNull.Value),
                new SqlParameter("@Year", (object)year ?? DBNull.Value),
                new SqlParameter("@Status", status?.ToString() ?? (object)DBNull.Value),
                new SqlParameter("@ProductID", (object)productID ?? DBNull.Value),
            };

            _helper.ExecuteTransaction("DeleteOrders", parameters);
        }
    }
}
