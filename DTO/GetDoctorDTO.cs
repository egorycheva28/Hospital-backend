using System;
using System.ComponentModel.DataAnnotations;

public class GetDoctorDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    public DateTime? Birthday { get; set; }
    [Required]
    public Gender Gen { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    [Phone]
    [RegularExpression(@"^\+7\s*\(\d{3}\)\s*\d{3}\-\d{2}\-\d{2}$")]
    public string? PhoneNumber { get; set; }
}
