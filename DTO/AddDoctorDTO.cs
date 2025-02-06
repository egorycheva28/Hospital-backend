using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class AddDoctorDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Name { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    [MinLength(1)]
    public string Email { get; set; }
    public DateTime Birthday { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gen { get; set; }
    [Phone]
    [RegularExpression(@"^\+7\s*\(\d{3}\)\s*\d{3}\-\d{2}\-\d{2}$")]
    public string PhoneNumber { get; set; }
    [Required]
    public Guid Speciality { get; set; }
}
