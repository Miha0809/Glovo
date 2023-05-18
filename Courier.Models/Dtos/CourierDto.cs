using Company.Models.Dtos;

namespace Courier.Models.Dtos;

public class CourierDto
{
    public required string Name { get; set; }
    public virtual OrderDto? Order { get; set; }
}