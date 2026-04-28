using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IUserService _service;

        public StaffController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStaff()
        {
            var result = await _service.GetAllStaffAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(int id)
        {
            var result = await _service.GetStaffByIdAsync(id);

            if (result == null)
            {
                return NotFound("Staff not found.");
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStaff([FromBody] CreateStaffDto dto)
        {
            try
            {
                var result = await _service.CreateStaffAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStaff(int id, [FromBody] UpdateStaffDto dto)
        {
            var result = await _service.UpdateStaffAsync(id, dto);

            if (result == null)
            {
                return NotFound("Staff not found.");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var result = await _service.DeleteStaffAsync(id);

            if (!result)
            {
                return NotFound("Staff not found.");
            }

            return Ok("Staff deleted successfully.");
        }
    }
}