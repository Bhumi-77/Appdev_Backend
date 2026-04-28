using System.Collections.Generic;

namespace VehicleSystem.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}