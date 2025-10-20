using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Persistence;
using ECommerce.Application.DTOs;

namespace ECommerce.Application.Queries.Orders;

public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto?>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetOrderByIdHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<OrderDto?> Handle(GetOrderByIdQuery req, CancellationToken ct)
    {
        var order = await _db.Orders
            .Include(o => o.Customer)
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == req.Id, ct);

        return order == null ? null : _mapper.Map<OrderDto>(order);
    }
}
