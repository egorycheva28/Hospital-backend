using System;

public class GetPatientsListDTO
{
    public string? Name { get; set; }
    public List<Conclusion>? Conclusions { get; set; }=new List<Conclusion>();
    public Sorting? Sort { get; set; }
    public bool? ScheduledVisits { get; set; }
    public bool? OnlyMine { get; set; }
    public int Page {  get; set; }
    public int Size { get; set; }
}