using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ECommerce.Application.DTOs;
using ECommerce.Persistence;

namespace ECommerce.Application.Queries.Customers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, List<CustomerDto>>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public GetCustomersHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<List<CustomerDto>> Handle(GetCustomersQuery req, CancellationToken ct)
    {
        var customers = await _db.Customers.ToListAsync(ct);
        return _mapper.Map<List<CustomerDto>>(customers);
    }
}
