
namespace VehicleSystem.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } = string.Empty;

        public List<Vehicle> Vehicles { get; set; }
        public List<Invoice> Invoices { get; set; }
    }
}