using System.ComponentModel.DataAnnotations;

namespace Companies.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Range(0, uint.MaxValue)]
    public required uint Price { get; set; }
    
    [Range(0, 200)]
    public required uint Weight { get; set; }
    
    [StringLength(50)]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [StringLength(short.MaxValue)]
    [DataType(DataType.MultilineText)]
    public required string Description { get; set; }
    
    public virtual Company? Company { get; set; }
    public virtual Category Category { get; set; }
}