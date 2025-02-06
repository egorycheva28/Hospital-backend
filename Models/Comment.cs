using System;
using System.ComponentModel.DataAnnotations;
public class Comment
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    [Required]
    [MinLength(1)]
    public string Content { get; set; }
    [Required]
    public Guid AuthorId { get; set; }
    [Required]
    [MinLength(1)]
    public string Author { get; set; }
    public Guid? ParentId { get; set; }
    [Required]
    public Guid ConsultationId { get; set; }
}
