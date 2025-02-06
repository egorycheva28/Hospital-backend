using System;
using System.ComponentModel.DataAnnotations;

public class EditCommentDTO
{
    [Required]
    [StringLength(1000, MinimumLength = 1)]
    public string Content { get; set; }
}
