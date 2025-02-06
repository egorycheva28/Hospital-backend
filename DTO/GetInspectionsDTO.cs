using System;

public class GetInspectionsDTO
{
	public bool Grouped { get; set; }
	public List<string> ICDRoots { get; set; }
	public int Page {  get; set; }
	public int Size { get; set; }
}
