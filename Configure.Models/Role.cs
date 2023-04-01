using System.ComponentModel.DataAnnotations;

namespace Configure.Models;

public class Role
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
}
