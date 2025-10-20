using MediatR;
using System.Collections.Generic;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Commands.Orders;

public record OrderProductDto(int ProductId, int Quantity);
public record CreateOrderCommand(int CustomerId, List<OrderProductDto> Products) : IRequest<OrderDto>;
