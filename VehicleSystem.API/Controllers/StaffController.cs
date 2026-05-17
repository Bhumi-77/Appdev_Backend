using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _userService;

        public StaffController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var staff = await _userService.GetAllStaffAsync();
            return Ok(staff);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var staff = await _userService.GetStaffByIdAsync(id);

            if (staff == null)
                return NotFound("Staff not found.");

            return Ok(staff);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff(CreateStaffDto dto)
        {
            var staff = await _userService.CreateStaffAsync(dto);
            return Ok(staff);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, UpdateStaffDto dto)
        {
            var result = await _userService.UpdateStaffAsync(id, dto);

            if (!result)
                return NotFound("Staff not found.");

            return Ok("Staff updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeactivateStaff(int id)
        {
            var result = await _userService.DeactivateStaffAsync(id);

            if (!result)
                return NotFound("Staff not found.");

            return Ok("Staff deactivated successfully.");
        }
    }
}