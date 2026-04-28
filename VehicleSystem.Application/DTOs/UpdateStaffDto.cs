namespace VehicleSystem.Application.DTOs
{
    public class UpdateStaffDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Role { get; set; } = "Staff";
        public bool IsActive { get; set; } = true;
    }
}