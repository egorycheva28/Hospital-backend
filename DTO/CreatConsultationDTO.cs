using System;
using System.ComponentModel.DataAnnotations;

public class CreatConsultationDTO
{
	[Required]
	public Guid SpecialityId { get; set; }
    [Required]
    public CreatCommentDTO Comment { get; set; }
}
