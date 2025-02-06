using System;
using System.ComponentModel.DataAnnotations;

public class GetDictionaryListDTO
{
    public string? Name { get; set; }
    public int Page { get; set; }
    public int Size { get; set; }
}
