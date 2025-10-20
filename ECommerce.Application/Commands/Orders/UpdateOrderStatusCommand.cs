using MediatR;

namespace ECommerce.Application.Commands.Orders;

public record UpdateOrderStatusCommand(int OrderId, string NewStatus) : IRequest<string>;
