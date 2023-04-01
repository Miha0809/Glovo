using System.ComponentModel.DataAnnotations;

namespace Configure.Models;

public class Email
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [DataType(DataType.EmailAddress)]
    public required string Name { get; set; }
}