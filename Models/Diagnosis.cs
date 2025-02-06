using System;
using System.ComponentModel.DataAnnotations;

public class Diagnosis
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    [MinLength(1)]
    public string Code { get; set; }
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    public string? Description { get; set; }
    [Required]
    public TypeDiagnosis Type { get; set; }
}
