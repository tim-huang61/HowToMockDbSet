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