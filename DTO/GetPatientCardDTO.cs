using System;

public class GetPatientCardDTO
{
    public Guid Id { get; set; }
    //public DateTime CreateTime { get; set; }
    public string Name { get; set; }
    public DateTime Birthday { get; set; }
    public Gender Gen { get; set; }
}
