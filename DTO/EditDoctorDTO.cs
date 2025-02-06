using System;
using System.ComponentModel.DataAnnotations;

public class EditDoctorDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Name { get; set; }
    [Required]
    [MinLength(1)]
    [EmailAddress]
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    [Required]
    public Gender Gen { get; set; }
    [Phone]
    [RegularExpression(@"^\+7\s*\(\d{3}\)\s*\d{3}\-\d{2}\-\d{2}$")]
    public string PhoneNumber { get; set; }
}
