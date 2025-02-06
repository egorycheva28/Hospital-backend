using System;
using System.ComponentModel.DataAnnotations;

public class CreatCommentDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; }
	public Guid ParentId { get; set; }
}
