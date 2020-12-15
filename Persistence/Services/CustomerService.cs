using System.Linq;
using Persistence.Models;

namespace Persistence.Services
{
    public class CustomerService
    {
        private readonly NorthwindContext _northwindContext;

        public CustomerService(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public Customer FindByID(int id)
        {
            return _northwindContext.Customers.FirstOrDefault(c => c.CustomerID == id);
        }
    }
}