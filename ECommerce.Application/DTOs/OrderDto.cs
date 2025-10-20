namespace ECommerce.Application.DTOs;

public class OrderDto
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = default!;
    public string Status { get; set; } = default!;
    public int NumberOfProducts { get; set; }
    public double TotalPrice { get; set; }
}
