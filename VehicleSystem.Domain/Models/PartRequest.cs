namespace Vehicle_System.Models
{
    public class PartRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string PartName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; } = "Requested"; // Requested, Fulfilled, Rejected

        public Customer Customer { get; set; }
    }
}
