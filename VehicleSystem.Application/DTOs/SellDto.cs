namespace Vehicle_System.DTOs
{
    public class SellDto
    {
        public int CustomerId { get; set; }
        public List<SellItemDto> Items { get; set; }
    }
}
