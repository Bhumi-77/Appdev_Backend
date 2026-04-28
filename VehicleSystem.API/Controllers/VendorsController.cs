using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    // Attribute Routing
    [ApiController]
    [Route("api/vendors")]
    //[Authorize(Roles = "Admin")]  // JWT Authorization
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        // Dependency Injection
        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        // GET api/vendors
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _vendorService.GetAllVendorsAsync();
            return Ok(response);
        }

        // GET api/vendors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _vendorService.GetVendorByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        // POST api/vendors
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] VendorRequestDto dto)
        {
            // Model Validation
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _vendorService.CreateVendorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        // PUT api/vendors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] VendorRequestDto dto)
        {
            var response = await _vendorService.UpdateVendorAsync(id, dto);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        // DELETE api/vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _vendorService.DeleteVendorAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}