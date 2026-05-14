namespace Vehicle_System.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceType { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled

        public Customer Customer { get; set; }
    }
}
