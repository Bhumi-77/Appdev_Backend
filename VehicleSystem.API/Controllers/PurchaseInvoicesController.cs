using Microsoft.AspNetCore.Mvc;
using VehicleSystem.Application.DTOs;
using VehicleSystem.Application.Interfaces.Services;

namespace VehicleSystem.API.Controllers
{
    [ApiController]
    [Route("api/purchaseinvoices")]
    public class PurchaseInvoicesController : ControllerBase
    {
        private readonly IPurchaseInvoiceService _service;

        public PurchaseInvoicesController(IPurchaseInvoiceService service)
        {
            _service = service;
        }

        // GET api/purchaseinvoices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllAsync();
            return Ok(response);
        }

        // GET api/purchaseinvoices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetByIdAsync(id);
            if (!response.Success) return NotFound(response);
            return Ok(response);
        }

        // POST api/purchaseinvoices
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PurchaseInvoiceRequestDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _service.CreateAsync(dto);
            if (!response.Success) return BadRequest(response);
            return CreatedAtAction(nameof(GetById), new { id = response.Data?.Id }, response);
        }
    }
}