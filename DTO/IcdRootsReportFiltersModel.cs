using System;
using System.ComponentModel.DataAnnotations;

public class IcdRootsReportFiltersModel
{
    [Required]
    public DateTime Start { get; set; }
    [Required]
    public DateTime End { get; set; }
    public List<string>? IcdRoots { get; set; }
}
