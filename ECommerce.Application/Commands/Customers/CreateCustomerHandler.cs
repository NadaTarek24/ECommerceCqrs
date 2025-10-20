using MediatR;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ECommerce.Domain.Entities;
using ECommerce.Application.DTOs;
using ECommerce.Persistence;
using System.Threading.Tasks;
using System.Threading;

namespace ECommerce.Application.Commands.Customers;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateCustomerHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(CreateCustomerCommand req, CancellationToken ct)
    {
        if (await _db.Customers.AnyAsync(c => c.Email == req.Email, ct))
            throw new ValidationException("Email already exists.");

        var customer = new Customer
        {
            Name = req.Name,
            Email = req.Email,
            Phone = req.Phone
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync(ct);

        return _mapper.Map<CustomerDto>(customer);
    }
}
