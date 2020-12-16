namespace Persistence.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }
    }
}