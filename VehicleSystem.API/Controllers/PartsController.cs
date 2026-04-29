using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    [ApiController]
    [Route("api/parts")]
    //[Authorize(Roles = "Admin")]
    public class PartsController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartsController(IPartService partService)
        {
            _partService = partService;
        }

        // GET api/parts
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _partService.GetAllPartsAsync();
            return Ok(response);
        }

        // GET api/parts/low-stock 
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock()
        {
            var response = await _partService.GetLowStockPartsAsync();
            return Ok(response);
        }

        // GET api/parts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _partService.GetPartByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        // POST api/parts
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PartRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _partService.CreatePartAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }

        // PUT api/parts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PartRequestDto dto)
        {
            var response = await _partService.UpdatePartAsync(id, dto);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        // DELETE api/parts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _partService.DeletePartAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }
    }
}