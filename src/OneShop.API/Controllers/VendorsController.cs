using System;
using Microsoft.AspNetCore.Mvc;
using OneShop.Application.Interfaces;
using OneShop.Domain.Entities;

namespace OneShop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _service;
        private readonly ILogger<VendorsController> _logger;

        public VendorsController(IVendorService service, ILogger<VendorsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        // GET: api/vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendors()
        {
            var _vendors = await _service.GetAllVendorsAsync();
            return Ok(_vendors);
        }

        // GET: api/vendor/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(long id)
        {
            var vendor = await _service.GetVendorByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return Ok(vendor);
        }

        // POST: api/vendor
        [HttpPost]
        public async Task<ActionResult<Vendor>> CreateVendor(Vendor vendor)
        {
            await _service.AddVendorAsync(vendor);
            return CreatedAtAction(nameof(GetVendor), new { id = vendor.Id }, vendor);
        }

        // PUT: api/vendor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVendor(long id, Vendor updatedVendor)
        {

            var vendor = await _service.GetVendorByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            updatedVendor.Id = vendor.Id;
            await _service.UpdateVendorAsync(updatedVendor);
            return NoContent();

        }

        // DELETE: api/vendor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(long id)
        {
            var vendor = await _service.GetVendorByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            await _service.DeleteVendorAsync(vendor);
            return NoContent();
        }
    }
}
