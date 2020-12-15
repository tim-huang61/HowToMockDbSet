using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Customer> FindByIDAsync(int id)
        {
            var customers = await _northwindContext.Customers.ToListAsync();

            return await _northwindContext.Customers.FirstOrDefaultAsync(c => c.CustomerID == id);
        }
    }
}