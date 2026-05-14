namespace Vehicle_System.Models
{
    public class ServiceReview
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; } // 1 to 5
        public DateTime ReviewDate { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }
    }
}
