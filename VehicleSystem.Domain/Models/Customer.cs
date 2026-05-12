namespace Vehicle_System.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<PartRequest> PartRequests { get; set; } = new List<PartRequest>();
        public List<ServiceReview> ServiceReviews { get; set; } = new List<ServiceReview>();
    }
}
