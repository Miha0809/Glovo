﻿using System.ComponentModel.DataAnnotations;

namespace Company.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
}