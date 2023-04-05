﻿using System.ComponentModel.DataAnnotations;
using Configure.Models;
using Configure.Models.interfaces;

namespace Companies.Models;

public class Company : IUser
{
    [Key]
    public int Id { get; set; }

    [StringLength(20)]
    [DataType(DataType.Text)]
    public required string Name { get; set; }

    [StringLength(256)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public virtual required Email Email { get; set; }
    public virtual required Role Role { get; set; }
    public virtual List<Product> Products { get; set; }
}