using System;
using System.ComponentModel.DataAnnotations;

public class Speciality
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
}
