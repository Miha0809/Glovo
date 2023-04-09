using System.ComponentModel.DataAnnotations;

namespace Companies.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [DataType(DataType.Time)]
    public DateTime Date { get; set; }
    
    public required Client.Models.Client Client { get; set; }
    public required List<Product> Products { get; set; }
}