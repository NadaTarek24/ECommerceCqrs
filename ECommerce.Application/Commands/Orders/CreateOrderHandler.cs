using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ECommerce.Persistence;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace ECommerce.Application.Commands.Orders;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateOrderHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OrderDto> Handle(CreateOrderCommand req, CancellationToken ct)
    {
        if (!req.Products.Any())
            throw new ValidationException("Order must contain at least one product.");

        var customer = await _db.Customers.FindAsync(new object[] { req.CustomerId }, ct)
                       ?? throw new KeyNotFoundException("Customer not found.");

        var productIds = req.Products.Select(p => p.ProductId).ToList();
        var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToListAsync(ct);

        double total = 0;
        var order = new Order
        {
            CustomerId = req.CustomerId,
            Status = "Pending",
            OrderDate = DateTime.UtcNow
        };

        foreach (var p in req.Products)
        {
            var prod = products.FirstOrDefault(pr => pr.Id == p.ProductId)
                       ?? throw new KeyNotFoundException($"Product {p.ProductId} not found.");

            // ✅ Do NOT reduce stock here anymore.
            // Just calculate total and record quantity.
            total += prod.Price * p.Quantity;

            order.OrderItems.Add(new OrderItem { ProductId = prod.Id, Quantity = p.Quantity });
        }

        order.TotalPrice = total;
        _db.Orders.Add(order);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<OrderDto>(order);
    }
}
