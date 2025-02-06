using System;
using System.ComponentModel.DataAnnotations;

public class Token
{
	[Required]
	public Guid Id { get; set; }
	[Required]
	[MinLength(1)]
	public string Tokens { get; set; }
}
