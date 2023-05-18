using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public class Order
{
    [Key]
    public int Id { get; set; }
    
    [DataType(DataType.Time)]
    public DateTime Date { get; set; } = DateTime.Now;

    public bool IsConfirm { get; set; }

    public virtual required List<Product> Products { get; set; }
    public virtual Client.Models.Client? Client { get; set; }
}