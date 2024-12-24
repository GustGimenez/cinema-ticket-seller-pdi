using System.Security.Claims;
using Application.DTOs;
using Application.Models;
using Application.Schemas;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CustomerDTO>> FindById(long id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userRole != Role.Administrator.ToString() && userId != id.ToString())
            {
                return Forbid();
            }

            return await _customerService.FindById(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create([FromBody] CreateCustomerSchema schema)
        {
            var cratedCustomer = await _customerService.Create(schema);

            return Created("", cratedCustomer);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Update(long id, [FromBody] UpdateCustomerSchema schema)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userRole != Role.Administrator.ToString() && userId != id.ToString())
            {
                return Forbid();
            }

            await _customerService.Update(id, schema);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Deactivate(long id)
        {
            var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var userId = User.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            if (userRole != Role.Administrator.ToString() && userId != id.ToString())
            {
                return Forbid();
            }

            await _customerService.Deactivate(id);

            return Ok();
        }
    }
}
