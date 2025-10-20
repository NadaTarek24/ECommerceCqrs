using MediatR;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Commands.Customers;

public record CreateCustomerCommand(string Name, string Email, string? Phone) : IRequest<CustomerDto>;
