using System;
using System.ComponentModel.DataAnnotations;

public class InspectionConsultationModel
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public Guid? InspectionId { get; set; }
    public Speciality Speciality { get; set; }
    public InspectionCommentModel RootComment { get; set; }

    public int CommentsNumber { get; set; }
}
