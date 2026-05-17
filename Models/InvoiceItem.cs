namespace Vehicle_System.Models
{
    public class InvoiceItem
    {
        public int Id { get; set; }   // ✅ PRIMARY KEY (IMPORTANT)

        public int InvoiceId { get; set; }
        public int PartId { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
