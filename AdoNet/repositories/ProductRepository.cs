using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using AdoNet.models;
using AdoNet.repositories;

namespace AdoNet.services
{
    public class ProductRepository
    {

        private IADOHelper _helper;

        public ProductRepository(IADOHelper helper)
        {
            _helper = helper;
        }

        public void Add(Product product)
        {
            string query = "INSERT INTO Product (Name, Description, Weight, Height, Width, Length) VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Description", product.Description),
                new SqlParameter("@Weight", product.Weight),
                new SqlParameter("@Height", product.Height),
                new SqlParameter("@Width", product.Width),
                new SqlParameter("@Length", product.Length)
            };

            _helper.ExecuteCommand(query, parameters);
        }

        public Product Get(int id)
        {
            string query = "SELECT * FROM Product WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            return _helper.ExecuteSingleReader(query, parameters, (rdr) =>
            {
                return new Product
                {
                    Id = Convert.ToInt32(rdr["Id"]),
                    Name = rdr["Name"].ToString(),
                    Description = rdr["Description"].ToString(),
                    Weight = Convert.ToDecimal(rdr["Weight"]),
                    Height = Convert.ToDecimal(rdr["Height"]),
                    Width = Convert.ToDecimal(rdr["Width"]),
                    Length = Convert.ToDecimal(rdr["Length"])
                };
            });
        }

        public void Update(Product product)
        {
            string query = "UPDATE Product SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", product.Id),
                new SqlParameter("@Name", product.Name),
                new SqlParameter("@Description", product.Description),
                new SqlParameter("@Weight", product.Weight),
                new SqlParameter("@Height", product.Height),
                new SqlParameter("@Width", product.Width),
                new SqlParameter("@Length", product.Length)
            };

            _helper.ExecuteCommand(query, parameters);
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Product WHERE Id = @Id";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", id)
            };

            _helper.ExecuteCommand(query, parameters);
        }

        public IEnumerable<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            var parameters = new List<SqlParameter>();

            using (var reader = _helper.ExecuteReaderWithConnection("SELECT * FROM Product", parameters))
            {
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        Height = reader.GetDecimal(3),
                        Width = reader.GetDecimal(4),
                        Weight = reader.GetDecimal(5),
                        Length = reader.GetDecimal(6),
                    });
                }
            }

            return products;
        }
    }
}
