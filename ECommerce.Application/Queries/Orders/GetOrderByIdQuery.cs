using MediatR;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Queries.Orders;

public record GetOrderByIdQuery(int Id) : IRequest<OrderDto>;
