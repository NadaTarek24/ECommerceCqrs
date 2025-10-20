using MediatR;
using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace ECommerce.Application.Commands.Orders;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatusCommand, string>
{
    private readonly ApplicationDbContext _db;
    public UpdateOrderStatusHandler(ApplicationDbContext db) => _db = db;

    public async Task<string> Handle(UpdateOrderStatusCommand req, CancellationToken ct)
    {
        var order = await _db.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.Id == req.OrderId, ct);

        if (order is null)
            throw new KeyNotFoundException("Order not found.");

        // ✅ Update status
        order.Status = req.NewStatus;

        // ✅ If delivered, reduce stock now
        if (req.NewStatus.Equals("Delivered", StringComparison.OrdinalIgnoreCase))
        {
            foreach (var item in order.OrderItems)
            {
                if (item.Product == null)
                    throw new Exception($"Product {item.ProductId} not found.");

                if (item.Product.Stock < item.Quantity)
                    throw new Exception($"Insufficient stock for {item.Product.Name}.");

                item.Product.Stock -= item.Quantity;
            }
        }

        await _db.SaveChangesAsync(ct);
        return $"Order {order.Id} status updated to {order.Status}";
    }
}
