using System.Collections.Generic;

namespace ECommerce.Application.DTOs;
public class CreateOrderDto
{
    public int CustomerId { get; set; }
    public List<int> ProductIds { get; set; } = new();
}