namespace VehicleSystem.Domain.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}