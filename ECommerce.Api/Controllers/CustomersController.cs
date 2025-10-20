using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerce.Application.Commands.Customers;
using ECommerce.Application.Queries.Customers;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetCustomersQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customers = await _mediator.Send(new GetCustomersQuery());
        var customer = customers.FirstOrDefault(c => c.Id == id);
        if (customer is null) return NotFound(new { message = "Customer not found" });
        return Ok(customer);
    }
}
