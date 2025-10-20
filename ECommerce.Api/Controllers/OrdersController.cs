using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerce.Application.Commands.Orders;
using ECommerce.Application.Queries.Orders;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrdersController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderCommand cmd)
    {
        var result = await _mediator.Send(cmd);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(id));
        return result == null ? NotFound(new { message = "Order not found" }) : Ok(result);
    }

    [HttpPost("UpdateOrderStatus/{id}")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
    {
        var result = await _mediator.Send(new UpdateOrderStatusCommand(id, newStatus));
        return Ok(new { message = result });
    }
}
