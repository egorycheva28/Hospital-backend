using System;
using System.ComponentModel.DataAnnotations;
public class Consultation
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public Guid? InspectionId { get; set; }
    public Speciality? Specialty { get; set; }
    public List<Comment>? Comments { get; set; }
}
