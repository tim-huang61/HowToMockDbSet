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
            _northwindContext = Substitute.For<NorthwindContext>();
            var mockDbSet = ToMockDbSet(new List<Customer>
            {
                new Customer {CustomerID = 1},
                new Customer {CustomerID = 2},
                new Customer {CustomerID = 3},
            });
            _northwindContext.Customers = mockDbSet;
        }

        private DbSet<TEntity> ToMockDbSet<TEntity>(IEnumerable<TEntity> data) where TEntity : class
        {
            var mockDbSet = Substitute.For<DbSet<TEntity>, IQueryable<TEntity>>();
            var dataQuery = data.AsQueryable();
            var dbQuery   = (IQueryable<TEntity>) mockDbSet;
            dbQuery.Expression.Returns(dataQuery.Expression);
            dbQuery.Provider.Returns(dataQuery.Provider);
            dbQuery.ElementType.Returns(dbQuery.ElementType);
            dbQuery.GetEnumerator().Returns(new TestEnumerator<TEntity>(dataQuery.GetEnumerator()));

            return mockDbSet;
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