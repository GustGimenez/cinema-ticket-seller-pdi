using System.Security.Claims;
using cinema_ticket_seller_pdi.DTOs;
using cinema_ticket_seller_pdi.Models;
using cinema_ticket_seller_pdi.Schemas;
using cinema_ticket_seller_pdi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cinema_ticket_seller_pdi.Controllers
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
    }
}
