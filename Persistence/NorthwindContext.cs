using System;
using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
    }
}