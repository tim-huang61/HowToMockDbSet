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
            throw new System.NotImplementedException();
        }
    }
}