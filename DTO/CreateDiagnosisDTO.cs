using System;
using System.ComponentModel.DataAnnotations;

public class CreateDiagnosisDTO
{
	[Required]
	public Guid DiagnosisId { get; set; }
	[MaxLength(5000)]
	public string Description { get; set; }
    [Required]
    public TypeDiagnosis Type { get; set; }
}
