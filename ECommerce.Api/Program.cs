using ECommerce.Persistence;
using Microsoft.EntityFrameworkCore;
using MediatR;
using FluentValidation;
using FluentValidation.AspNetCore;
using AutoMapper;
using ECommerce.Application.Mapping;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Controllers + FluentValidation
builder.Services.AddControllers();
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(ApplicationProfile).Assembly);

// 2️⃣ Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 3️⃣ Database
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 4️⃣ CQRS (MediatR)
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(ApplicationProfile).Assembly));

// 5️⃣ AutoMapper
builder.Services.AddAutoMapper(typeof(ApplicationProfile).Assembly); // ✅ Should now work

// 6️⃣ Build & Run
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();