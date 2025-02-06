using System;
using System.ComponentModel.DataAnnotations;

public class InspectionShortDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public Diagnosis Diagnosis { get; set; }
}
