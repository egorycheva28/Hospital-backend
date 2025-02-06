using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

public class CreatePatientDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    [Required]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Gen { get; set; }
}
