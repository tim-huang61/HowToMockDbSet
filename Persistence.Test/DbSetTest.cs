using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Persistence.Services;
using NUnit.Framework;
using Persistence.Models;

namespace Persistence.Test
{
    public class DbSetTest
    {
        private NorthwindContext _northwindContext;

        [SetUp]
        public void Setup()
        {
            _northwindContext           = Substitute.For<NorthwindContext>();
            _northwindContext.Customers = Substitute.For<DbSet<Customer>>();
            var customers = new List<Customer>
            {
                new Customer {CustomerID = 1},
                new Customer {CustomerID = 2},
                new Customer {CustomerID = 3},
            }.AsQueryable();

            var queryable = _northwindContext.Customers.AsQueryable();
            queryable.Expression.Returns(customers.Expression);
            queryable.Provider.Returns(customers.Provider);
            queryable.ElementType.Returns(queryable.ElementType);
            queryable.GetEnumerator().Returns(new TestEnumerator<Customer>(customers.GetEnumerator()));
        }

        [Test]
        public void NSub_To_Mock_DbSet()
        {
            var customerService = new CustomerService(_northwindContext);
            var customer        = customerService.FindByID(3);
            customer.CustomerID.Should().Be(3);
        }
    }
}