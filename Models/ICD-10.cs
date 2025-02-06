using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
public class ICD10
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public Guid? ParentId { get; set; }
}
