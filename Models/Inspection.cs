using System;
using System.ComponentModel.DataAnnotations;

public class Inspection
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
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
    public List<Consultation>? Consultation { get; set; }
    public void Validate()
    {
        if (Date > DateTime.Now)
        {
            throw new InvalidOperationException("Дата осмотра не должна быть позже сегодняшнего дня.");
        }
        if (Date > NextVisitDate)
        {
            throw new InvalidOperationException("Дата следующего осмотра должна быть позже даты данного осмотра.");
        }
        if(!Diagnoses.Any(d=>d.Type==TypeDiagnosis.Main))
        {
            throw new InvalidOperationException("Осмотр обязательно должен иметь один диагноз с типом диагноза “Основной”.");
        }
        switch(Conclusions)
        {
            case Conclusion.Disease:
                if (NextVisitDate == null)
                {
                    throw new InvalidOperationException("При выборе заключения “Болезнь”, необходимо указать дату и время следующего визита.");
                }
                break;
            case Conclusion.Recovery:
                break;
            case Conclusion.Death:
                if (DeathDate == null)
                {
                    throw new InvalidOperationException("При выборе заключения “Смерть”, необходимо указать дату и время смерти.");
                }
                break;
            default:
                throw new InvalidOperationException("Такое заключение нельзя.");
        }
       
    }
}
