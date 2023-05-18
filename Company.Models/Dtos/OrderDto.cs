namespace Company.Models.Dtos;

public sealed class OrderDto
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsConfirm { get; set; }

    public required List<ProductDto> Products { get; set; }
    public Client.Models.Dtos.ClientDto? Client { get; set; }
}