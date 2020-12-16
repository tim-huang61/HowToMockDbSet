using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NSubstitute;
using Persistence.Services;
using NUnit.Framework;
using Persistence.Models;
using Persistence.Test.Mocks;

namespace Persistence.Test
{
    public class DbSetTest
    {
        private NorthwindContext _northwindContext;
        private List<Customer>   _data;

        [SetUp]
        public void Setup()
        {
            _data = new List<Customer>
            {
                new() {CustomerID = 1, Name = "Pixel", IsDeleted = true},
                new() {CustomerID = 2, Name = "Wendy"},
                new() {CustomerID = 3, Name = "Tim"},
            };
        }

        [Test]
        public void Test_FindByID_NSub_To_Mock_DbSet()
        {
            // ToListAsync
            _northwindContext           = Substitute.For<NorthwindContext>();
            _northwindContext.Customers = _data.ToMockDbSet();
            var customerService = new CustomerService(_northwindContext);

            var customer = customerService.FindByID(3);

            customer.CustomerID.Should().Be(3);
        }

        [Test]
        public void Test_FindByID_Moq_To_Mock_DbSet()
        {
            // ToListAsync
            _northwindContext           = new Mock<NorthwindContext>().Object;
            _northwindContext.Customers = _data.ToMockDbSetByMoq();
            var customerService = new CustomerService(_northwindContext);
            var customer        = customerService.FindByID(3);

            customer.CustomerID.Should().Be(3);
        }

        [Test]
        public async Task Test_FindByIDAsync_NSub_To_Mock_DbSet()
        {
            // FirstOrDefaultAsync
            _northwindContext           = Substitute.For<NorthwindContext>();
            _northwindContext.Customers = _data.ToMockDbSet();
            var customerService = new CustomerService(_northwindContext);

            var customer = await customerService.FindByIDAsync(3);

            customer.CustomerID.Should().Be(3);
        }

        [Test]
        public async Task Test_FindByIDAsync_Moq_To_Mock_DbSet()
        {
            // FirstOrDefaultAsync
            _northwindContext           = new Mock<NorthwindContext>().Object;
            _northwindContext.Customers = _data.ToMockDbSetByMoq();
            var customerService = new CustomerService(_northwindContext);

            var customer = await customerService.FindByIDAsync(3);

            customer.CustomerID.Should().Be(3);
        }

        [Test]
        public async Task Test_FindAll_NSub_To_Mock_DbSet()
        {
            // Where, ToListAsync
            _northwindContext           = Substitute.For<NorthwindContext>();
            _northwindContext.Customers = _data.ToMockDbSet();
            var customerService = new CustomerService(_northwindContext);

            var customers = await customerService.FindAllAsync();

            customers.Count().Should().Be(2);
        }

        [Test]
        public async Task Test_FindAll_OrderByNameAsync_NSub_To_Mock_DbSet()
        {
            // Where, OrderBy, ToListAsync
            _northwindContext           = Substitute.For<NorthwindContext>();
            _northwindContext.Customers = _data.ToMockDbSet();
            var customerService = new CustomerService(_northwindContext);
            var customers       = await customerService.FindAll_OrderByNameAsync();

            customers.Should().BeEquivalentTo(new List<Customer>
            {
                new() {CustomerID = 3, Name = "Tim"},
                new() {CustomerID = 2, Name = "Wendy"},
            }, options => options.WithStrictOrdering());
        }
    }
}