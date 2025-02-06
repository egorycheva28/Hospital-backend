using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

public class Patient
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public DateTime CreateTime { get; set; } = DateTime.UtcNow;
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
	public DateTime? Birthday { get; set; }
    [Required]
    public Gender Gen { get; set; }
    public List<Inspection> Inspections { get; set; }
    public void Validate()
    {
        if (Birthday > DateTime.Now)
        {
            throw new InvalidOperationException("Дата рождения не должна быть позже сегодняшнего дня.");
        }
    }
}
