using System;
using System.ComponentModel.DataAnnotations;

public class CopyInspection
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public DateTime CreateTime { get; set; }

    public DateTime Date { get; set; }

    public string? Anamnesis { get; set; }

    public string? Complaints { get; set; }

    public string? Treatment { get; set; }
    
    public Conclusion Conclusions { get; set; }

    public DateTime? NextVisitDate { get; set; }

    public DateTime? DeathDate { get; set; }

    public Guid? BaseInspectionId { get; set; }

    public Guid? PreviousInspectionId { get; set; }

    public Patient Patient { get; set; }

    public Doctor Doctor { get; set; }

    public List<Diagnosis>? Diagnoses { get; set; }

    public List<InspectionConsultationModel>? Consultation { get; set; }
}
