using MediatR;
using ECommerce.Application.DTOs;
using System.Collections.Generic;

namespace ECommerce.Application.Queries.Customers;

public record GetCustomersQuery() : IRequest<List<CustomerDto>>;
