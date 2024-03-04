using System;
using System.Collections.Generic;
using System.Linq;
using Task1.DoNotChange;

namespace Task1
{
    public static class LinqTask
    {
        public static IEnumerable<Customer> Linq1(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null)
            {
                throw new ArgumentNullException(nameof(customers));
            }

            return customers.Where(x => x.Orders.Sum(y => y.Total) > limit);

        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
                throw new ArgumentNullException(nameof(customers));
            if (suppliers is null)
                throw new ArgumentNullException(nameof(suppliers));

            return customers.Select(x => (x, suppliers.Where(y => y.Country == x.Country && y.City == x.City)));


        }

        public static IEnumerable<(Customer customer, IEnumerable<Supplier> suppliers)> Linq2UsingGroup(
            IEnumerable<Customer> customers,
            IEnumerable<Supplier> suppliers
        )
        {
            if (customers is null)
                throw new ArgumentNullException(nameof(customers));
            if (suppliers is null)
                throw new ArgumentNullException(nameof(suppliers));

            var supplierGroup = suppliers
                .GroupBy(x => new
                {
                    x.Country,
                    x.City
                });

            return customers
                .Select(x => (x, supplierGroup.Where(y => y.Key.Country == x.Country && y.Key.City == x.City).SelectMany(y => y)));

        }

        public static IEnumerable<Customer> Linq3(IEnumerable<Customer> customers, decimal limit)
        {
            if (customers is null)
                throw new ArgumentNullException();

            return customers
                .Where(x => x.Orders.Any())
                .Where(x => x.Orders.Sum(y => y.Total) > limit);

        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq4(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
                throw new ArgumentNullException();

            return customers
                .Where(x => x.Orders.Any())
                .Select(x => (x, x.Orders.Select(y => y.OrderDate).Min()));

        }

        public static IEnumerable<(Customer customer, DateTime dateOfEntry)> Linq5(
            IEnumerable<Customer> customers
        )
        {
            var result = Linq4(customers);

            return result
                .OrderBy(x => x.dateOfEntry.Year)
                .ThenBy(x => x.dateOfEntry.Month)
                .ThenByDescending(x => x.customer.Orders.Sum(y => y.Total))
                .ThenBy(x => x.customer.CompanyName);

        }

        public static IEnumerable<Customer> Linq6(IEnumerable<Customer> customers)
        {
            if (customers is null)
                throw new ArgumentNullException();

            return customers.Where(x => !x.PostalCode.All(char.IsDigit) || string.IsNullOrEmpty(x.Region) || !x.Phone.Contains("("));

        }

        public static IEnumerable<Linq7CategoryGroup> Linq7(IEnumerable<Product> products)
        {
            /* example of Linq7result

             category - Beverages
	            UnitsInStock - 39
		            price - 18.0000
		            price - 19.0000
	            UnitsInStock - 17
		            price - 18.0000
		            price - 19.0000
             */

            var group = products.GroupBy(x => new { x.Category, x.UnitsInStock });

            return group.Select(x => new Linq7CategoryGroup()
            {
                Category = x.Key.Category,
                UnitsInStockGroup = x.Select(y => new Linq7UnitsInStockGroup
                {
                    UnitsInStock = y.UnitsInStock,
                    Prices = x.Select(z => z.UnitPrice)
                })
            });

        }

        public static IEnumerable<(decimal category, IEnumerable<Product> products)> Linq8(
            IEnumerable<Product> products,
            decimal cheap,
            decimal middle,
            decimal expensive
        )
        {
            if (products is null)
                throw new ArgumentNullException();

            var ch = products.Where(x => x.UnitPrice <= cheap);
            var mi = products.Where(x => x.UnitPrice > cheap && x.UnitPrice <= middle);
            var ex = products.Where(x => x.UnitPrice > middle && x.UnitPrice >= expensive);

            return new List<(decimal category, IEnumerable<Product> products)>
            {
                (cheap, ch),
                (middle,mi),
                (expensive, ex),
            };

        }

        public static IEnumerable<(string city, int averageIncome, int averageIntensity)> Linq9(
            IEnumerable<Customer> customers
        )
        {
            if (customers is null)
                throw new ArgumentNullException();

            return customers.GroupBy(x => x.City)
                .Select(x => (x.Key, (int)Math.Ceiling(x.Sum(x => x.Orders.Sum(x => x.Total)) / x.Count()), x.Sum(x => x.Orders.Count()) / x.Count()));

        }

        public static string Linq10(IEnumerable<Supplier> suppliers)
        {
            if (suppliers is null)
                throw new ArgumentNullException();

            return suppliers
                .GroupBy(x => x.Country)
                .OrderBy(x => x.Key.Length)
                .ThenBy(x => x.Key)
                .Select(x => x.Key)
                .Aggregate((x, y) => x + y);
        }

    }
}