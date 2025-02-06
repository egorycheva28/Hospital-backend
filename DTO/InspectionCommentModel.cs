using System;
using System.ComponentModel.DataAnnotations;

public class InspectionCommentModel
{
    [Required]
	public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public Guid ParentId { get; set; }
    public string Content { get; set; }
    public Doctor Author { get; set; }
    public DateTime? ModifyTime { get; set; }
}
