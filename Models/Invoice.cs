namespace Vehicle_System.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }

        public Customer Customer { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
