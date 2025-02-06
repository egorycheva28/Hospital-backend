using System;
using System.ComponentModel.DataAnnotations;

public class Icd10DTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public string? Code { get; set; }
    public string? Name { get; set; }
}
