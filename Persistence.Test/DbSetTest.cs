using FluentAssertions;
using NSubstitute;
using Persistence.Services;
using NUnit.Framework;

namespace Persistence.Test
{
    public class DbSetTest
    {
        private NorthwindContext _northwindContext;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void NSub_To_Mock_DbSet()
        {
            _northwindContext = Substitute.For<NorthwindContext>();
            var customerService  = new CustomerService(_northwindContext);
            var customer         = customerService.FindByID(3);
            customer.CustomerID.Should().Be(3);
        }
    }
}