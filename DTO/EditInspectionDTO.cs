using System;
using System.ComponentModel.DataAnnotations;

public class EditInspectionDTO
{
    [MaxLength(5000)]
    public string Anamnesis { get; set; }
    [Required]
    [StringLength(5000, MinimumLength = 1)]
    public string Complaints { get; set; }
    [Required]
    [StringLength(5000, MinimumLength = 1)]
    public string Treatment { get; set; }
    public DateTime? NextVisitDate { get; set; }
    public DateTime? DeathDate { get; set; }
    [Required]
    public Conclusion Conclusion { get; set; }
    //[Required]
    [MinLength(1)]
    public List<CreateDiagnosisDTO>? Diagnoses { get; set; }
}
